using System;
using System.Linq;
using System.Text.RegularExpressions;
using LegoBuildingBlock;
using Oryza.Entities;
using Oryza.ServiceInterfaces;
using Oryza.Utility;

namespace Oryza.Infrastructure.Extract
{
    public class PriceUnitExtractorBlock : IBlock<Snapshot, Snapshot>
    {
        private readonly IConfiguration _configuration;

        public PriceUnitExtractorBlock(IConfiguration configuration)
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
                           var footer = snapshot.PriceTableData
                                                .ToHtmlDocument()
                                                .DocumentNode
                                                .SelectSingleNode(_configuration.PriceUnitXPath)
                                                .InnerText;

                           snapshot.PriceUnit = _configuration.DefaultPriceUnits
                                                              .First(x => Regex.IsMatch(footer, x, RegexOptions.IgnoreCase));

                           return snapshot;
                       };
            }
        }
    }
}