using System;
using LegoBuildingBlock;
using Oryza.ServiceInterfaces;
using Oryza.Utility;

namespace Oryza.Infrastructure.Parsing
{
    public class PriceTableParserBlock : IBlock<string, string>
    {
        private readonly IConfiguration _configuration;

        public PriceTableParserBlock(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Input { get; set; }

        public Func<string, string> Work
        {
            get
            {
                return html => html.ToHtmlDocument()
                                   .DocumentNode
                                   .SelectSingleNode(_configuration.PriceTableXPath)
                                   .OuterHtml;
            }
        }
    }
}