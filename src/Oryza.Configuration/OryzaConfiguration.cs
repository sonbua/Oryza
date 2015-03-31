using System;
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
            get { return "//div[contains(@class, 'view-rice-price')]"; }
        }

        public string PublishDateXPath
        {
            get { return "//span[@class='date-display-single']"; }
        }

        public string PublishDateFormat
        {
            get { return "MMMM dd, yyyy"; }
        }
    }
}