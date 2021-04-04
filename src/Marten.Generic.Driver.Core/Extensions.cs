using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Marten.Generic.Driver.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddGenericMrten(
            this IServiceCollection services,
            string configSection = "marten")
        {
            using var scope = services.BuildServiceProvider().CreateScope();

            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            services.Configure<MartenOptions>(
                configuration.GetSection(configSection),
                opt => configuration.Bind(opt));

            //var options = configuration
            //    .GetSection(configSection)
            //    .GetValue<MartenOptions>(configSection);

            //var x = configuration["marten:connectionstring"];
            var connectionstring = configuration
                .GetSection(configSection)
                .Get<MartenOptions>()
                .ConnectionString;

            services.AddMarten(opt =>
            {
                opt.Connection(connectionstring);
                opt.AutoCreateSchemaObjects = AutoCreate.All;
            })
            .BuildSessionsWith<DefaultSessionFactory>();


            services.AddScoped(
                typeof(IMartenRepository<>),
                typeof(MartenRepository<>));

            return services;
        }

        public static IServiceCollection AddGenericMrten(
            this IServiceCollection services,
            Action<MartenOptions> action)
        {
            using var scope = services.BuildServiceProvider().CreateScope();

            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            services.Configure(action);

            var connectionstring = configuration
                .GetSection("marten")
                .Get<MartenOptions>()
                .ConnectionString;

            services.AddMarten(opt =>
            {
                opt.Connection(connectionstring);
                opt.AutoCreateSchemaObjects = AutoCreate.All;
            })
            .BuildSessionsWith<DefaultSessionFactory>();


            services.AddScoped(
                typeof(IMartenRepository<>),
                typeof(MartenRepository<>));

            return services;
        }

        public static IWebHostBuilder UseGenericMarten(this IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.ConfigureAppConfiguration(cfg =>
            {
                cfg.AddJsonFile("marten.json", true, true);

            });
            return webHostBuilder;
        }

        public static IWebHostBuilder UseGenericMarten(
            this IWebHostBuilder webHostBuilder,
            string filename)
        {
            webHostBuilder.ConfigureAppConfiguration(cfg =>
            {
                cfg.AddJsonFile($"{filename}.json", true, true);
            });
            return webHostBuilder;
        }
    }
}
