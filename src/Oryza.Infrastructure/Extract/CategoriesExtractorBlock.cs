using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using LegoBuildingBlock;
using Oryza.Entities;
using Oryza.ServiceInterfaces;
using Oryza.Utility;

namespace Oryza.Infrastructure.Extract
{
    public class CategoriesExtractorBlock : IBlock<Snapshot, Snapshot>
    {
        private readonly IConfiguration _configuration;

        public CategoriesExtractorBlock(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Snapshot Input { get; set; }

        public Func<Snapshot, Snapshot> Work
        {
            get
            {
                return snapshot =>
                       {
                           snapshot.Categories = Input.PriceTableData
                                                      .ToHtmlDocument()
                                                      .DocumentNode
                                                      .SelectNodes(_configuration.CategoriesXPath)
                                                      .Select(ExtractCategory)
                                                      .ToList();

                           return snapshot;
                       };
            }
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
    }
}