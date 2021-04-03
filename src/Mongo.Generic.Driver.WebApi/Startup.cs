using Marten.Generic.Driver.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Mongo.Generic.Driver.Core;

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

            //var clientId2 = Configuration["configuredClients:clients:1:clientId"]?.ToString();
            //var clientName1 = Configuration.GetValue<string>("configuredClients:clients:0:clientName");

            //var config = Configuration.GetSection("configuredClients").Bind<ClientConfiguration>();
            //var config = Configuration.GetSection("configuredClients").Get<ClientConfiguration>();
            //var positionOptions = new PositionOptions();
            //Configuration.GetSection(PositionOptions.Position).Bind(positionOptions);
            //positionOptions = Configuration.GetSection(PositionOptions.Position)
            //                                         .Get<PositionOptions>();
            services.AddGenericMrten();


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
