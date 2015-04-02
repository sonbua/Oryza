using System;
using SimpleInjector;

namespace Oryza.TestBase
{
    public class Test
    {
        protected readonly IServiceProvider _serviceProvider;

        protected Test()
        {
            _serviceProvider = new TestDoublesContainerBuilder().Build();
        }

        protected Test(Action<Container> additionalRegistration)
        {
            _serviceProvider = new TestDoublesContainerBuilder().AddRegistration(additionalRegistration).Build();
        }
    }
}