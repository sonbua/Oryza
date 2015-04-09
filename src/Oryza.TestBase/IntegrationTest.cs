using System;
using SimpleInjector;

namespace Oryza.TestBase
{
    public class IntegrationTest : Test
    {
        protected IntegrationTest()
        {
            TruncateDatabase();
        }

        protected IntegrationTest(Action<Container> additionalRegistration)
        {
            TruncateDatabase();
        }
    }
}