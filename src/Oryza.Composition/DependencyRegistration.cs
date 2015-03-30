using Oryza.Capture;
using Oryza.Configuration;
using Oryza.ServiceInterfaces;
using RestSharp;
using SimpleInjector;

namespace Oryza.Composition
{
    public static class DependencyRegistration
    {
        public static void RegisterDependencies(this Container container)
        {
            container.Register<IRestClient>(() => new RestClient());
            container.Register<IConfiguration, OryzaConfiguration>();
            container.Register<IWebCapture, WebCapture>();
        }
    }
}