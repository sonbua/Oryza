using System;
using Raven.Client.Document;
using SimpleInjector;

namespace Oryza.TestBase
{
    public class DbTest : Test
    {
        protected DbTest() : base(DbRegistration)
        {
        }

        protected DbTest(Action<Container> additionalRegistration) : base(Include(additionalRegistration))
        {
        }

        private static void DbRegistration(Container container)
        {
            container.RegisterSingle(() => new DocumentStore
                                           {
                                               Url = "http://localhost:8080",
                                               DefaultDatabase = "oryzatest"
                                           });
        }

        private static Action<Container> Include(Action<Container> additionalRegistration)
        {
            return container =>
                   {
                       additionalRegistration(container);
                       DbRegistration(container);
                   };
        }
    }
}