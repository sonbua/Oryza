using System;

namespace Oryza.TestBase
{
    public static class ServiceProviderExtensions
    {
        public static T GetService<T>(this IServiceProvider serviceProvider) where T : class
        {
            var service = serviceProvider.GetService(typeof (T));

            if (service == null)
            {
                throw new Exception(string.Format("No registration for type {0} could be found.", typeof (T).Name));
            }

            return (T) service;
        }
    }
}