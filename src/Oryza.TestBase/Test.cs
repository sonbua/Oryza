using System;
using Oryza.TestBase.Composition;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;
using SimpleInjector;

namespace Oryza.TestBase
{
    public class Test
    {
        protected readonly IServiceProvider _serviceProvider;

        protected Test()
        {
            _serviceProvider = new TestDoublesContainerBuilder().AddRegistration(EmbeddableDocumentStoreRegistration)
                                                                .Build();
        }

        protected Test(Action<Container> additionalRegistration)
        {
            _serviceProvider = new TestDoublesContainerBuilder().AddRegistration(EmbeddableDocumentStoreRegistration)
                                                                .AddRegistration(additionalRegistration)
                                                                .Build();
        }

        private static void EmbeddableDocumentStoreRegistration(Container container)
        {
            container.RegisterSingle<IDocumentStore>(() => new DocumentStore
                                                           {
                                                               Url = "http://localhost:8080",
                                                               DefaultDatabase = "oryzatest"
                                                           }.Initialize());

            container.Register<IDocumentSession>(() => container.GetInstance<IDocumentStore>().OpenSession());
        }

        protected void TruncateDatabase()
        {
            var documentStore = _serviceProvider.GetService<IDocumentStore>();
            var indexDefinitions = documentStore.DatabaseCommands.GetIndexes(0, 100);

            foreach (var indexDefinition in indexDefinitions)
            {
                documentStore.DatabaseCommands.DeleteByIndex(indexDefinition.Name, new IndexQuery());
            }
        }
    }
}