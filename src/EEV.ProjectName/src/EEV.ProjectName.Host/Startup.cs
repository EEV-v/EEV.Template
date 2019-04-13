using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EEV.ProjectName.API.Controllers;
using EEV.ProjectName.API.DI;
using EEV.ServiceDiscovery.Client;
using EEV.ServiceDiscovery.Client.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Swashbuckle.AspNetCore.Swagger;

namespace EEV.ProjectName.Host
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddApplicationPart(typeof(DefaultController).Assembly);
            services.AddLogging((builder) =>
            {
                builder.AddConsole((options) =>
                {
                    options.IncludeScopes = true;
                    options.DisableColors = false;
                });
                builder.AddConfiguration(_configuration);
            });
            services.AddScoped<ConventionalMiddleware>();

            services.AddHttpContextAccessor();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
            services.RegisterAuthenticationClients();
            services.RegisterServiceDiscoveryClients();
            services.RegisterControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILogger<Startup> logger,
            ServicesClient servicesClient)
        {
            logger.LogDebug(nameof(Configure));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseMiddleware<ConventionalMiddleware>();
            //app.Use(async (t1, t2) =>
            //{
            //    await t2.Invoke();
            //});
            app.UseMvc();
            servicesClient.Register(_configuration["Client:Id"]).Wait();
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }

    public class ConventionalMiddleware
    {
        private readonly RequestDelegate _next;

        public ConventionalMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, TokensClient tokensClient)
        {
#if DEBUG
            context.Request.Headers.Add("Authorization", "Bearer 123");
#endif
            if (tokensClient == null)
            {
                throw new ArgumentNullException(nameof(tokensClient));
            }

            var keyValue = context.Request.Query["Authorization"];
            await tokensClient.Check(keyValue);

            await _next(context);
        }
    }
}
