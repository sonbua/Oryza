using HtmlAgilityPack;
using Oryza.ServiceInterfaces;

namespace Oryza.Extract
{
    public class Extractor : IExtractor
    {
        private readonly IConfiguration _configuration;

        public Extractor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Accepts HTML document and returns extracted price table.
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public string Extract(string html)
        {
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(html);

            return htmlDocument.DocumentNode
                               .SelectSingleNode(string.Format("//div[contains(@class, '{0}')]", _configuration.PriceTableCssSelector))
                               .OuterHtml;
        }
    }
}