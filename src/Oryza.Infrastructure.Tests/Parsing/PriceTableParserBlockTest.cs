using System.IO;
using Oryza.Infrastructure.Parsing;
using Oryza.TestBase;
using Oryza.TestBase.Composition;
using Xunit;

namespace Oryza.Infrastructure.Tests.Parsing
{
    public class PriceTableParserBlockTest : Test
    {
        [Fact]
        public void Parse_InputHtml_ReturnsHtmlPriceTable()
        {
            // arrange
            var parserBlock = _serviceProvider.GetService<PriceTableParserBlock>();
            var html = File.ReadAllText("oryza_web_page.txt");

            // act
            var priceTable = parserBlock.Handle(html);

            // assert
            Assert.Contains("view-rice-price", priceTable);
        }
    }
}