using System;
using Oryza.Capture;
using Oryza.Configuration;
using Oryza.Extract;
using Oryza.Parsing;
using Oryza.ServiceInterfaces;
using Raven.Client;
using Raven.Client.Document;
using RestSharp;
using SimpleInjector;

namespace Oryza.Composition
{
    public static class DependencyRegistration
    {
        public static Container RegisterDependencies(this Container container)
        {
            // Oryza.Capture
            container.Register<IWebCapture, WebCapture>();

            // Oryza.Configuration
            container.Register<IConfiguration, OryzaConfiguration>();

            // Oryza.Parsing
            container.Register<IPriceTableParser, PriceTableParser>();

            // Oryza.Extract
            var priceTableExtractorRegistration = Lifestyle.Transient.CreateRegistration<PriceTableExtractor>(container);

            container.AddRegistration(typeof (IPriceTableExtractor), priceTableExtractorRegistration);
            container.AddRegistration(typeof (IDateExtractor), priceTableExtractorRegistration);
            container.AddRegistration(typeof (ICategoriesExtractor), priceTableExtractorRegistration);
            container.AddRegistration(typeof (IPriceUnitExtractor), priceTableExtractorRegistration);

            // Packages
            container.Register<IRestClient>(() => new RestClient());

            return container;
        }

        public static Container RegisterDatabaseDependency(this Container container)
        {
            // Oryza.DataAccess
            container.RegisterSingle<Func<IDocumentStore>>(() => () => new DocumentStore
                                                                       {
                                                                           Url = "http://localhost:8080",
                                                                           DefaultDatabase = "oryza"
                                                                       }.Initialize());

            return container;
        }
    }
}