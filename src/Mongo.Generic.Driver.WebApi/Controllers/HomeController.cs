using Marten.Generic.Driver.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Mongo.Generic.Driver.Core;

namespace Mongo.Generic.Driver.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IOptions<MartenOptions> _options;
        private readonly IMartenRepository<Product> _repository;

        public HomeController(
            IOptions<MartenOptions> options,
            IMartenRepository<Product> repository)
        {
            _options = options;
            _repository = repository;
        }

        [HttpGet, Route("~/")]
        public IActionResult Index()
        {
            //return "MongoOptions: " + _options.Value.ConnectionString + ", " + _options.Value.Database
            //    + ", " + _options.Value.Document;

            //return Ok(_repository.List(a => a.Id));
            var t = _repository.List(a => a.Id, a => a.Id >= 1000);
            return Ok(t);
        }
    }
}
