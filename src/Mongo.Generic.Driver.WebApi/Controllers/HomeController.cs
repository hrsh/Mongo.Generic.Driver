using EventStoreDb.Generic.Driver.Core;
using Marten.Generic.Driver.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Mongo.Generic.Driver.Core;
using Newtonsoft.Json;
using Redis.Cache.Driver;
using System;
using System.Threading.Tasks;

namespace Mongo.Generic.Driver.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IOptions<RedisOptions> _options;
        private readonly IRedisCache _cache;

        public HomeController(
            IOptions<RedisOptions> options,
            IRedisCache cache)
        {
            _options = options;
            _cache = cache;
        }

        [HttpGet, Route("~/")]
        public async Task<IActionResult> Index()
        {
            //return "MongoOptions: " + _options.Value.ConnectionString + ", " + _options.Value.Database
            //    + ", " + _options.Value.Document;

            //return Ok(_repository.List(a => a.Id));
            //var t = _repository.List(a => a.Id, a => a.Id >= 1000);

            //var data = new
            //{
            //    name = "action",
            //    method = "GET",
            //    controller = "home",
            //    date = DateTime.Now
            //};

            //var e = new CustomEvent
            //{
            //    EventId = Guid.NewGuid(),
            //    Data = JsonConvert.SerializeObject(data),
            //    EventType = "event-type",
            //    MetaData = "{}",
            //    StreamName = "custom-event-stream"
            //};

            //await _repository.AppendAsync(e);

            await _cache.SetData<CustomEvent>("event_1", new CustomEvent
            {
                Data = "data",
                EventId = Guid.NewGuid(),
                EventType = "info",
                MetaData = "meta data",
                StreamName = "stream name"
            });

            return Ok(await _cache.GetData<CustomEvent>("event_1"));
        }
    }
}
