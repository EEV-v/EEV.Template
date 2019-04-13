using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace EEV.ServiceDiscovery.Client.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServiceDiscoveryClients(
            this IServiceCollection services)
        {
            services.AddScoped<ServicesClient>();
            services.AddHttpClient();
            return services;
        }
    }
}
