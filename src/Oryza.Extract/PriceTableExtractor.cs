using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Oryza.Entities;
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

        public string ExtractPriceUnit(string priceTable)
        {
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(priceTable);

            var footer = htmlDocument.DocumentNode
                                     .SelectSingleNode(_configuration.PriceUnitXPath)
                                     .InnerText;

            return _configuration.DefaultPriceUnits
                                 .First(x => Regex.IsMatch(footer, x, RegexOptions.IgnoreCase));
        }

        public IEnumerable<Category> ExtractCategories(string priceTable)
        {
            throw new NotImplementedException();
        }
    }
}