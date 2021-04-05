using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Redis.Cache.Driver
{
    public static class Extensions
    {
        public static IServiceCollection AddRedisCache(
            this IServiceCollection services,
            string configSection = "redis")
        {
            using var scope = services.BuildServiceProvider().CreateScope();

            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            services.Configure<RedisOptions>(
                configuration.GetSection(configSection),
                opt => configuration.Bind(opt));

            var options = configuration
                .GetSection(configSection)
                .Get<RedisOptions>();

            options.ValidateServer().ValidatePortNumber();

            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = options.ConnectionString;
                opt.InstanceName = options.InstanceName;
                
                if(options.Ssl)
                    opt.ConfigurationOptions.Ssl = options.Ssl;

                if (!string.IsNullOrEmpty(options.Password))
                    opt.ConfigurationOptions.Password = options.Password;
            });

            services.AddSingleton(
                typeof(IRedisCache),
                typeof(RedisCache));

            return services;
        }

        public static IServiceCollection AddRedisCache(
            this IServiceCollection services,
            Action<RedisOptions> action)
        {
            using var scope = services.BuildServiceProvider().CreateScope();

            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            services.Configure(action);


            var options = new RedisOptions();
            action.Invoke(options);
            options.ValidateServer().ValidatePortNumber();
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = options.ConnectionString;
                opt.InstanceName = options.InstanceName;
                opt.ConfigurationOptions.Ssl = options.Ssl;
                if (!string.IsNullOrEmpty(options.Password))
                    opt.ConfigurationOptions.Password = options.Password;
            });

            services.AddSingleton(
                typeof(IRedisCache),
                typeof(RedisCache));

            return services;
        }

        public static IWebHostBuilder UseRedisCache(this IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.ConfigureAppConfiguration(cfg =>
            {
                cfg.AddJsonFile("redis.json", true, true);

            });
            return webHostBuilder;
        }

        public static IWebHostBuilder UseRedisCache(
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
