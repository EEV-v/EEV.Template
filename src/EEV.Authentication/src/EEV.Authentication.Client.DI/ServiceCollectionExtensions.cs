using System;
using Microsoft.Extensions.DependencyInjection;

namespace EEV.ServiceDiscovery.Client.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterAuthenticationClients(
            this IServiceCollection services)
        {
            services.AddScoped<TokensClient>();
            services.AddHttpClient();
            return services;
        }
    }
}
