using System;
using System.IO;
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
    }
}