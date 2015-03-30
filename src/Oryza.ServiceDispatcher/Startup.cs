using System;
using Oryza.Composition;
using SimpleInjector;

namespace Oryza.ServiceDispatcher
{
    public class Startup
    {
        public void Start()
        {
            throw new NotImplementedException();
        }

        private Container BuildContainer()
        {
            var container = new Container();

            container.RegisterDependencies();

            return container;
        }
    }
}