using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EventStoreDb.Generic.Driver.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddGenericEventStoreDb(
            this IServiceCollection services,
            string configSection = "eventstoredb")
        {
            using var scope = services.BuildServiceProvider().CreateScope();

            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            services.Configure<EventStoreDbOptions>(
                configuration.GetSection(configSection),
                opt => configuration.Bind(opt));

            var connectionstring = configuration
                .GetSection(configSection)
                .Get<EventStoreDbOptions>()
                .ConnectionString;


            services.AddSingleton(
                typeof(IEventStoreDbRepository<>),
                typeof(EventStoreDbRepository<>));

            return services;
        }

        public static IServiceCollection AddGenericEventStoreDb(
            this IServiceCollection services,
            Action<EventStoreDbOptions> action)
        {
            using var scope = services.BuildServiceProvider().CreateScope();

            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            services.Configure(action);

            services.AddScoped(
                typeof(IEventStoreDbRepository<>),
                typeof(EventStoreDbRepository<>));

            return services;
        }

        public static IWebHostBuilder UseGenericEventStoreDb(this IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.ConfigureAppConfiguration(cfg =>
            {
                cfg.AddJsonFile("eventstoredb.json", true, true);

            });
            return webHostBuilder;
        }

        public static IWebHostBuilder UseGenericEventStoreDb(
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
