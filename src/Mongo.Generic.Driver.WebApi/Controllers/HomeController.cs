using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Mongo.Generic.Driver.Core;

namespace Mongo.Generic.Driver.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IOptions<MongoOptions> _options;

        public HomeController(IOptions<MongoOptions> options)
        {
            _options = options;
        }

        [HttpGet, Route("~/")]
        public string Index()
        {
            return "MongoOptions: " + _options.Value.ConnectionString + ", " + _options.Value.Database
                + ", " + _options.Value.Document;
        }
    }
}
