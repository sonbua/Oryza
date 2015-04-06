using System;
using Oryza.TestBase.Composition;
using Raven.Client.Document;
using Raven.Client.Embedded;
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
            container.RegisterSingle(() => new DocumentStore
                                           {
                                               Url = "http://localhost:8080",
                                               DefaultDatabase = "oryzatest"
                                           }.Initialize());
        }
    }
}