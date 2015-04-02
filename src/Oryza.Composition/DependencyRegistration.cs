using System;
using Oryza.Capture;
using Oryza.Configuration;
using Oryza.Extract;
using Oryza.Parsing;
using Oryza.ServiceInterfaces;
using Raven.Client.Document;
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

            // Oryza.Parsing
            container.Register<IPriceTableParser, PriceTableParser>();

            // Oryza.Extract
            container.Register<IDateExtractor, PriceTableExtractor>();
            container.Register<ICategoriesExtractor, PriceTableExtractor>();
            container.Register<IPriceUnitExtractor, PriceTableExtractor>();

            // Oryza.DataAccess
            container.RegisterSingle(() => new DocumentStore
                                           {
                                               Url = "http://localhost:8080",
                                               DefaultDatabase = "test"
                                           }.Initialize());

            // Packages
            container.Register<IRestClient>(() => new RestClient());
        }
    }
}