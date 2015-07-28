using System;
using System.Globalization;
using System.Text.RegularExpressions;
using LegoBuildingBlock;
using Oryza.Entities;
using Oryza.ServiceInterfaces;
using Oryza.Utility;

namespace Oryza.Infrastructure.Extract
{
    public class DateExtractorBlock : IBlock<Snapshot, Snapshot>
    {
        private readonly IConfiguration _configuration;

        public DateExtractorBlock(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Func<Snapshot, Snapshot> Handle
        {
            get
            {
                return snapshot =>
                       {
                           var dateString = snapshot.PriceTableData
                                                    .ToHtmlDocument()
                                                    .DocumentNode
                                                    .SelectSingleNode(_configuration.PublishDateXPath)
                                                    .InnerText;

                           dateString = Regex.Replace(dateString, "(st|nd|rd|th),", ",");

                           snapshot.PublishDate = DateTime.ParseExact(dateString, _configuration.PublishDateFormat, CultureInfo.InvariantCulture);

                           return snapshot;
                       };
            }
        }
    }
}