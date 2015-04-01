using System;
using System.IO;
using System.Linq;
using Oryza.ServiceInterfaces;
using Oryza.TestBase;
using Xunit;

namespace Oryza.Extract.Tests
{
    public class PriceTableExtractorTest
    {
        private readonly IServiceProvider _serviceProvider;

        public PriceTableExtractorTest()
        {
            _serviceProvider = new TestDoublesContainerBuilder().Build();
        }

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
            var extractor = _serviceProvider.GetService<ICategoryExtractor>();
            var priceTable = File.ReadAllText("oryza_price_table.txt");

            // act
            var categories = extractor.ExtractCategories(priceTable);

            // assert
            Assert.True(categories.Count() == 6);
        }
    }
}