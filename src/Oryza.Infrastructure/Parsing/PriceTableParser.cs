using Oryza.ServiceInterfaces;
using Oryza.Utility;

namespace Oryza.Infrastructure.Parsing
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
            return html.ToHtmlDocument()
                       .DocumentNode
                       .SelectSingleNode(_configuration.PriceTableXPath)
                       .OuterHtml;
        }
    }
}