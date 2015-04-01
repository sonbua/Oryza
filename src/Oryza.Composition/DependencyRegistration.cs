using System;
using Oryza.Capture;
using Oryza.Configuration;
using Oryza.Extract;
using Oryza.Parsing;
using Oryza.ServiceInterfaces;
using RestSharp;
using SimpleInjector;

namespace Oryza.Composition
{
    public static class DependencyRegistration
    {
        public static void RegisterDependencies(this Container container)
        {
            // Oryza.Capture
            container.Register<IWebCapture, WebCapture>();

            // Oryza.Configuration
            container.Register<IConfiguration, OryzaConfiguration>();
            container.Register<IFormatProvider, OryzaDateFormatProvider>();

            // Oryza.Parsing
            container.Register<IPriceTableParser, PriceTableParser>();

            // Oryza.Extract
            container.Register<IDateExtractor, PriceTableExtractor>();
            container.Register<ICategoriesExtractor, PriceTableExtractor>();
            container.Register<IPriceUnitExtractor, PriceTableExtractor>();

            // Packages
            container.Register<IRestClient>(() => new RestClient());
        }
    }
}