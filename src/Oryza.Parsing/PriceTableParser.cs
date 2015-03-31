using HtmlAgilityPack;
using Oryza.ServiceInterfaces;

namespace Oryza.Parsing
{
    public class PriceTableParser : IPriceTableParser
    {
        private readonly IConfiguration _configuration;

        public PriceTableParser(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Accepts HTML document and returns extracted price table.
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public string Parse(string html)
        {
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(html);

            return htmlDocument.DocumentNode
                               .SelectSingleNode(_configuration.PriceTableXPath)
                               .OuterHtml;
        }
    }
}