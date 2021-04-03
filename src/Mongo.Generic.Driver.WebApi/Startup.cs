using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mongo.Generic.Driver.Core;

namespace Mongo.Generic.Driver.WebApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddGenericMongo(a =>
            //{
            //    a.ConnectionString = "connection_string";
            //    a.Database = "database";
            //    a.Document = "document";
            //});

            //done
            services.AddGenericMongo();


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
