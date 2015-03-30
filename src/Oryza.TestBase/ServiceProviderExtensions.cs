using System;

namespace Oryza.TestBase
{
    public static class ServiceProviderExtensions
    {
        public static T GetInstance<T>(this IServiceProvider serviceProvider) where T : class
        {
            return serviceProvider.GetService(typeof (T)) as T;
        }
    }
}