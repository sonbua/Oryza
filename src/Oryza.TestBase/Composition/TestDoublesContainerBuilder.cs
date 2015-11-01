using System;
using Oryza.Composition;
using SimpleInjector;

namespace Oryza.TestBase.Composition
{
    public class TestDoublesContainerBuilder
    {
        private readonly Container _container;

        public TestDoublesContainerBuilder()
        {
            _container = new Container {Options = {AllowOverridingRegistrations = true}};

            _container.RegisterDependencies();
        }

        public TestDoublesContainerBuilder AddRegistration(Action<Container> additionalRegistration)
        {
            additionalRegistration(_container);

            return this;
        }

        public IServiceProvider Build()
        {
            _container.Verify();

            return _container;
        }
    }
}