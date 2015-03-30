using System;
using System.IO;
using Oryza.ServiceInterfaces;
using Oryza.TestBase;
using Xunit;

namespace Oryza.Extract.Tests
{
    public class ExtractorTest
    {
        private readonly IServiceProvider _serviceProvider;

        public ExtractorTest()
        {
            _serviceProvider = new TestDoublesContainerBuilder().Build();
        }

        [Fact]
        public void Extract_InputHtml_ReturnsExtractedTable()
        {
            // arrange
            var extractor = _serviceProvider.GetService<IExtractor>();
            var configuration = _serviceProvider.GetService<IConfiguration>();
            var html = File.ReadAllText("oryza_web_page.txt");

            // act
            var priceTable = extractor.Extract(html);

            // assert
            Assert.Contains(configuration.PriceTableCssSelector, priceTable);
        }
    }
}