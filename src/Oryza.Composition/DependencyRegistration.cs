using Oryza.Capture;
using Oryza.Configuration;
using Oryza.Extract;
using Oryza.ServiceInterfaces;
using RestSharp;
using SimpleInjector;

namespace Oryza.Composition
{
    public static class DependencyRegistration
    {
        public static void RegisterDependencies(this Container container)
        {
            // Oryza.Capture
            container.Register<IWebCapture, WebCapture>();

            // Oryza.Configuration
            container.Register<IConfiguration, OryzaConfiguration>();

            // Oryza.Extract
            container.Register<IExtractor, Extractor>();

            // Packages
            container.Register<IRestClient>(() => new RestClient());
        }
    }
}