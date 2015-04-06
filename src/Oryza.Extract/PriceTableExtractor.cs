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
    public class PriceTableExtractor : IPriceTableExtractor, IDateExtractor, ICategoriesExtractor, ICategoryNameConverter, ICategoryNameMatcher, IEntryNameConverter, IEntryNameMatcher, IPriceUnitExtractor
    {
        private readonly IConfiguration _configuration;

        public PriceTableExtractor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Snapshot ExtractPriceTable(string priceTable)
        {
            //            using (var session = _documentStore.OpenSession())
            //            {
            //                var existingCategoryTypes = session.Query<CategoryType>().ToList();
            //                var existingEntryTypes = session.Query<EntryType>().ToList();
            //
            //                foreach (var category in snapshot.Categories)
            //                {
            //                    CategoryType matchCategoryType;
            //
            //                    if (TryMatchCategoryName(category.Name, existingCategoryTypes, this, out matchCategoryType))
            //                    {
            //                        if (!matchCategoryType.NameVariants.Contains(category.Name))
            //                        {
            //                            matchCategoryType.NameVariants.Add(category.Name);
            //                        }
            //                    }
            //                    else
            //                    {
            //                        existingCategoryTypes.Add(new CategoryType
            //                                                  {
            //                                                      Name = ConvertCategoryName(category.Name),
            //                                                      NameVariants = new List<string> {category.Name}
            //                                                  });
            //                    }
            //
            //                    foreach (var entry in category.Entries)
            //                    {
            //                        EntryType matchEntryType;
            //
            //                        if (TryMatchEntryName(entry.Name, existingEntryTypes, this, out matchEntryType))
            //                        {
            //                            if (!matchEntryType.NameVariants.Contains(entry.Name))
            //                            {
            //                                matchEntryType.NameVariants.Add(entry.Name);
            //                            }
            //                        }
            //                        else
            //                        {
            //                            existingEntryTypes.Add(new EntryType
            //                                                   {
            //                                                       Name = ConvertEntryName(entry.Name),
            //                                                       NameVariants = new List<string> {entry.Name}
            //                                                   });
            //                        }
            //                    }
            //                }
            //
            //                session.BatchStore(existingCategoryTypes);
            //                session.BatchStore(existingEntryTypes);
            //
            //                session.SaveChanges();
            //            }

            return new Snapshot
                   {
                       PriceUnit = ExtractPriceUnit(priceTable),
                       PublishDate = ExtractDate(priceTable),
                       Categories = ExtractCategories(priceTable).ToList()
                   };
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

            // TODO: improve
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
            return categoryTable.SelectSingleNode(_configuration.CategoryNameXPath).InnerText.Trim();
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

        public string ConvertCategoryName(string categoryName)
        {
            return ConvertNameToType(categoryName);
        }

        public bool TryMatchCategoryName(string categoryName, IEnumerable<CategoryType> existingCategoryTypes, ICategoryNameConverter categoryNameConverter, out CategoryType match)
        {
            match = null;

            var newCategoryTypeName = categoryNameConverter.ConvertCategoryName(categoryName);

            foreach (var categoryType in existingCategoryTypes)
            {
                if (categoryType.NameVariants.Contains(categoryName))
                {
                    match = categoryType;
                    return true;
                }

                if (categoryType.Name == newCategoryTypeName)
                {
                    match = categoryType;
                    return true;
                }
            }

            return false;
        }

        public string ConvertEntryName(string entryName)
        {
            return ConvertNameToType(entryName);
        }

        private string ConvertNameToType(string name)
        {
            var segments = name.Split(_configuration.EntryNameSeparators.ToArray())
                               .SelectMany(Capitalize)
                               .SelectMany(TranslateSpecialChar)
                               .ToArray();

            return new string(segments);
        }

        private IEnumerable<char> Capitalize(string word)
        {
            if (word.IsNullOrEmpty())
            {
                yield break;
            }

            yield return char.ToUpper(word[0]);

            // TODO: improve
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

        public bool TryMatchEntryName(string entryName, IEnumerable<EntryType> existingEntryTypes, IEntryNameConverter entryNameConverter, out EntryType match)
        {
            match = null;

            var newEntryTypeName = entryNameConverter.ConvertEntryName(entryName);

            foreach (var entryType in existingEntryTypes)
            {
                if (entryType.NameVariants.Any(x => x == entryName))
                {
                    match = entryType;
                    return true;
                }

                if (entryType.Name == newEntryTypeName)
                {
                    match = entryType;
                    return true;
                }
            }

            return false;
        }
    }
}