using System;
using System.IO;
using System.Linq;
using Oryza.ServiceInterfaces;
using Oryza.TestBase;
using Xunit;

namespace Oryza.Extract.Tests
{
    public class PriceTableExtractorTest : Test
    {
        [Fact]
        public void ExtractDate_PriceTable_ReturnsCorrectDate()
        {
            // arrange
            var extractor = _serviceProvider.GetService<IDateExtractor>();
            var priceTable = File.ReadAllText("oryza_price_table.txt");

            // act
            var publishedDate = extractor.ExtractDate(priceTable);

            // assert
            Assert.Equal(new DateTime(2015, 3, 30), publishedDate);
        }

        [Fact]
        public void ExtractPriceUnit_PriceTable_ReturnsCorrectPriceUnit()
        {
            // arrange
            var extractor = _serviceProvider.GetService<IPriceUnitExtractor>();
            var priceTable = File.ReadAllText("oryza_price_table.txt");

            // act
            var priceUnit = extractor.ExtractPriceUnit(priceTable);

            // assert
            Assert.Equal("USD per ton", priceUnit);
        }

        [Fact]
        public void ExtractCategories_PriceTable_ReturnsCorrectCategories()
        {
            // arrange
            var extractor = _serviceProvider.GetService<ICategoriesExtractor>();
            var priceTable = File.ReadAllText("oryza_price_table.txt");

            // act
            var categories = extractor.ExtractCategories(priceTable);

            // assert
            Assert.Equal(6, categories.Count());
            Assert.Equal("Long grain white rice - high quality", categories.ElementAt(0).Name);
            Assert.Equal("Thailand 100% B grade", categories.ElementAt(0).Entries.First().Name);
            Assert.Equal(395M, categories.ElementAt(0).Entries.First().LowPrice);
            Assert.Equal(405M, categories.ElementAt(0).Entries.First().HighPrice);
        }
    }
}