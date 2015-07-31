using System;
using Oryza.Infrastructure.Configuration;
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
            // Oryza.Configuration
            container.RegisterSingle<IConfiguration, OryzaConfiguration>();

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