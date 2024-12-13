using System;
using System.Collections.Generic;

namespace WaterTankTool_WFA
{
    public class DIContainer
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();
        private readonly object _lock = new object(); // For thread safety

        // Register a service implementation
        public void Register<TService>(TService implementation)
        {
            lock (_lock)
            {
                _services[typeof(TService)] = implementation ?? throw new ArgumentNullException(nameof(implementation), "Service implementation cannot be null.");
            }
        }

        // Register an interface to a concrete implementation
        public void Register<TService, TImplementation>() where TImplementation : TService, new()
        {
            lock (_lock)
            {
                _services[typeof(TService)] = new TImplementation();
            }
        }

        // Retrieve a service
        public TService GetService<TService>()
        {
            lock (_lock)
            {
                if (_services.TryGetValue(typeof(TService), out var service))
                {
                    return (TService)service;
                }
                else
                {
                    throw new InvalidOperationException($"Service of type {typeof(TService)} has not been registered.");
                }
            }
        }
    }
}