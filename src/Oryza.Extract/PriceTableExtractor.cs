using System;
using System.Globalization;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Oryza.ServiceInterfaces;

namespace Oryza.Extract
{
    public class PriceTableExtractor : IPriceTableExtractor, IDateExtractor, ICategoryExtractor, IPriceUnitExtractor
    {
        private readonly IConfiguration _configuration;

        public PriceTableExtractor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DateTime ExtractDate(string priceTable)
        {
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(priceTable);

            var dateString = htmlDocument.DocumentNode
                                         .SelectSingleNode(_configuration.PublishDateXPath)
                                         .InnerText;

            dateString = Regex.Replace(dateString, "(st|nd|rd|th),", ",");

            return DateTime.ParseExact(dateString, _configuration.PublishDateFormat, CultureInfo.InvariantCulture);
        }
    }
}