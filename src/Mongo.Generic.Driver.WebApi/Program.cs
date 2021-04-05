using EventStoreDb.Generic.Driver.Core;
using Marten.Generic.Driver.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Mongo.Generic.Driver.Core;
using Redis.Cache.Driver;

namespace Mongo.Generic.Driver.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseGenericMongo();
                    //webBuilder.UseGenericMarten();
                    //webBuilder.UseGenericEventStoreDb();
                    webBuilder.UseRedisCache();
                    webBuilder.UseStartup<Startup>();
                });
    }
}
