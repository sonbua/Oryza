using System;
using System.Collections.Generic;
using Oryza.ServiceInterfaces;

namespace Oryza.Configuration
{
    public class OryzaConfiguration : IConfiguration
    {
        public Uri OryzaCaptureAddress
        {
            get { return new Uri("http://oryza.com/global-rice-quotes"); }
        }

        public string PriceTableXPath
        {
            get { return ".//div[contains(@class, 'view-rice-price')]"; }
        }

        public string PublishDateXPath
        {
            get { return ".//span[@class='date-display-single']"; }
        }

        public string PublishDateFormat
        {
            get { return "MMMM dd, yyyy"; }
        }

        public string PriceUnitXPath
        {
            get { return ".//div[@class='view-footer']/p/b"; }
        }

        public ISet<string> DefaultPriceUnits
        {
            get { return new SortedSet<string> {"USD per ton"}; }
        }

        public string CategoriesXPath
        {
            get { return ".//table"; }
        }

        public string CategoryNameXPath
        {
            get { return ".//b"; }
        }

        public string CategoryEntriesXPath
        {
            get { return ".//tr"; }
        }

        public string CategoryEntryNameXPath
        {
            get { return ".//td[1]"; }
        }

        public string CategoryEntryPriceRangeXPath
        {
            get { return ".//td[2]"; }
        }

        public string PriceUnavailableText
        {
            get { return "NQ"; }
        }

        public string PriceRangeDelimiter
        {
            get { return "-"; }
        }
    }
}