using EventStoreDb.Generic.Driver.Core;
using Marten.Generic.Driver.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Redis.Cache.Driver;

//https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-5.0
//https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-5.0
namespace Mongo.Generic.Driver.WebApi
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddGenericMongo(a =>
            //{
            //    a.ConnectionString = "connection_string";
            //    a.Database = "database";
            //    a.Document = "document";
            //});

            //done
            //services.AddGenericMongo();

            //services.AddGenericMrten();

            //services.AddGenericEventStoreDb();

            //services.AddRedisCache(opt =>
            //{
            //    opt.Server = "127.0.0.1";
            //    opt.Port = "6379";
            //});

            services.AddRedisCache();


            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
