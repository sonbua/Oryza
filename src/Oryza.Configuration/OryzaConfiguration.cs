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

        public string PriceTableCssSelector
        {
            get { return "view-rice-price"; }
        }
    }
}