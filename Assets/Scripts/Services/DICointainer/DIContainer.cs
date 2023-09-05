using System;
using System.Collections.Generic;

namespace Core
{
    // This service locator only for test task, in real project will be used DI like Zenject or VContainer
    
    public class DIContainer
    {
        private static readonly Dictionary<Type, object> Services = new();

        public static void Register<TService>(TService service)
        {
            Type serviceType = typeof(TService);
            if (!Services.ContainsKey(serviceType))
            {
                Services[serviceType] = service;
            }
            else
            {
                // Handle registration conflicts, if needed.
                // You can throw an exception, update the existing service, etc.
            }
        }

        public static TService GetService<TService>()
        {
            Type serviceType = typeof(TService);
            if (Services.ContainsKey(serviceType))
            {
                return (TService)Services[serviceType];
            }
            else
            {
                // Handle service not found, throw an exception, or return a default value.
                // For simplicity, this example throws an exception.
                throw new InvalidOperationException($"Service of type {serviceType.Name} not registered.");
            }
        }
    }
}