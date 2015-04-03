using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Oryza.Entities;
using Oryza.ServiceInterfaces;
using Oryza.Utility;

namespace Oryza.Extract
{
    public class PriceTableExtractor : IPriceTableExtractor, IDateExtractor, ICategoriesExtractor, IPriceUnitExtractor, IEntryTypeNameConverter, IEntryNameMatcher
    {
        private readonly IConfiguration _configuration;

        public PriceTableExtractor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DateTime ExtractDate(string priceTable)
        {
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(priceTable);

            var dateString = htmlDocument.DocumentNode
                                         .SelectSingleNode(_configuration.PublishDateXPath)
                                         .InnerText;

            dateString = Regex.Replace(dateString, "(st|nd|rd|th),", ",");

            return DateTime.ParseExact(dateString, _configuration.PublishDateFormat, CultureInfo.InvariantCulture);
        }

        public string ExtractPriceUnit(string priceTable)
        {
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(priceTable);

            var footer = htmlDocument.DocumentNode
                                     .SelectSingleNode(_configuration.PriceUnitXPath)
                                     .InnerText;

            return _configuration.DefaultPriceUnits
                                 .First(x => Regex.IsMatch(footer, x, RegexOptions.IgnoreCase));
        }

        public IEnumerable<Category> ExtractCategories(string priceTable)
        {
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(priceTable);

            var extractCategories = htmlDocument.DocumentNode
                                                .SelectNodes(_configuration.CategoriesXPath)
                                                .Select(ExtractCategory);
            return extractCategories;
        }

        private Category ExtractCategory(HtmlNode categoryTable)
        {
            return new Category
                   {
                       Name = ExtractCategoryName(categoryTable),
                       Entries = ExtractCategoryEntries(categoryTable),
                   };
        }

        private string ExtractCategoryName(HtmlNode categoryTable)
        {
            return categoryTable.SelectSingleNode(_configuration.CategoryNameXPath).InnerText;
        }

        private ICollection<Entry> ExtractCategoryEntries(HtmlNode categoryTable)
        {
            return ExtractCategoryEntriesImpl(categoryTable).ToList();
        }

        private IEnumerable<Entry> ExtractCategoryEntriesImpl(HtmlNode categoryTable)
        {
            return categoryTable.SelectNodes(_configuration.CategoryEntriesXPath)
                                .Select(ExtractCategoryEntry);
        }

        private Entry ExtractCategoryEntry(HtmlNode entryNode)
        {
            var name = entryNode.SelectSingleNode(_configuration.CategoryEntryNameXPath).InnerText.Trim();

            var priceRange = entryNode.SelectSingleNode(_configuration.CategoryEntryPriceRangeXPath).InnerText.Trim();

            var isPriceAvailable = priceRange != _configuration.PriceUnavailableText;

            decimal lowPrice = 0;
            decimal highPrice = 0;

            if (isPriceAvailable)
            {
                var priceTexts = priceRange.Split(new[] {_configuration.PriceRangeDelimiter}, StringSplitOptions.None);

                lowPrice = decimal.Parse(priceTexts.First());
                highPrice = decimal.Parse(priceTexts.Last());
            }

            return new Entry
                   {
                       Name = name,
                       IsPriceAvailable = isPriceAvailable,
                       HighPrice = highPrice,
                       LowPrice = lowPrice
                   };
        }

        public string ConvertEntryName(string entryName)
        {
            var fragments = entryName.Split(_configuration.EntryNameSeparators.ToArray())
                                     .SelectMany(Capitalize)
                                     .SelectMany(TranslateSpecialChar)
                                     .ToArray();

            return new string(fragments);
        }

        private IEnumerable<char> Capitalize(string word)
        {
            if (word.IsNullOrEmpty())
            {
                yield break;
            }

            yield return char.ToUpper(word[0]);

            foreach (var c in word.Substring(1))
            {
                yield return c;
            }
        }

        private IEnumerable<char> TranslateSpecialChar(char c)
        {
            if (!_configuration.SpecialCharToWordMap.ContainsKey(c))
            {
                yield return c;
                yield break;
            }

            foreach (var translatedChar in _configuration.SpecialCharToWordMap[c])
            {
                yield return translatedChar;
            }
        }

        public bool MatchEntryName(string entryName, ICollection<EntryType> existingEntryTypes, IEntryTypeNameConverter entryTypeNameConverter, out EntryType match)
        {
            match = null;

            foreach (var entryType in existingEntryTypes)
            {
                if (entryType.NameVariants.Any(x => x == entryName))
                {
                    match = entryType;
                    return true;
                }
            }

            foreach (var entryType in existingEntryTypes)
            {
                if (entryType.Name == entryTypeNameConverter.ConvertEntryName(entryName))
                {
                    match = entryType;
                    return true;
                }
            }

            return false;
        }
    }
}