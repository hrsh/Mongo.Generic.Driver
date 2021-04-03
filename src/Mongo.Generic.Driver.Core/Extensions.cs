using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mongo.Generic.Driver.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddGenericMongo(
            this IServiceCollection services,
            string configSection = "mongo")
        {
            using var scope = services.BuildServiceProvider().CreateScope();

            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            services.Configure<MongoOptions>(
                configuration.GetSection(configSection),
                opt => configuration.Bind(opt));

            services.AddScoped(
                typeof(IMongoRepository<>),
                typeof(MongoRepository<>));

            return services;
        }

        public static IServiceCollection AddGenericMongo(
            this IServiceCollection services,
            Action<MongoOptions> action)
        {
            using var scope = services.BuildServiceProvider().CreateScope();

            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            services.Configure(action);

            services.AddScoped(
                typeof(IMongoRepository<>),
                typeof(MongoRepository<>));

            return services;
        }

        public static IWebHostBuilder UseGenericMongo(this IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.ConfigureAppConfiguration(cfg =>
            {
                cfg.AddJsonFile("mongo.json", true, true);
                
            });
            return webHostBuilder;
        }

        public static IWebHostBuilder UseGenericMongo(
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
