
using EEV.ProjectName.API.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace EEV.ProjectName.API.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterControllers(this IServiceCollection services)
        {
            services.AddScoped<DefaultController>();
            return services;
        }
    }
}
