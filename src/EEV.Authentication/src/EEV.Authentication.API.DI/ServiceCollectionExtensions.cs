using EEV.Authentication.API.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace EEV.Authentication.API.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterControllers(
            this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<TokensController>();
            return serviceCollection;
        }
    }
}
