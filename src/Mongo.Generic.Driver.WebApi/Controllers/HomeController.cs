using EventStoreDb.Generic.Driver.Core;
using Marten.Generic.Driver.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Mongo.Generic.Driver.Core;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Mongo.Generic.Driver.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IOptions<EventStoreDbOptions> _options;
        private readonly IEventStoreDbRepository<CustomEvent> _repository;

        public HomeController(
            IOptions<EventStoreDbOptions> options,
            IEventStoreDbRepository<CustomEvent> repository)
        {
            _options = options;
            _repository = repository;
        }

        [HttpGet, Route("~/")]
        public async Task<IActionResult> Index()
        {
            //return "MongoOptions: " + _options.Value.ConnectionString + ", " + _options.Value.Database
            //    + ", " + _options.Value.Document;

            //return Ok(_repository.List(a => a.Id));
            //var t = _repository.List(a => a.Id, a => a.Id >= 1000);

            var data = new
            {
                name = "action",
                method = "GET",
                controller = "home",
                date = DateTime.Now
            };

            var e = new CustomEvent
            {
                EventId = Guid.NewGuid(),
                Data = JsonConvert.SerializeObject(data),
                EventType = "info",
                MetaData = "",
                StreamName = "custom-event-stream"
            };

            await _repository.AppendAsync(e);

            return Ok($"Connection string: {_options.Value.ConnectionString}");
        }
    }
}
