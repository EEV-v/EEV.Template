using EEV.ServiceDiscovery.API.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace EEV.ServiceDiscovery.API.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterControllers(
            this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ServicesController>();
            return serviceCollection;
        }
    }
}
