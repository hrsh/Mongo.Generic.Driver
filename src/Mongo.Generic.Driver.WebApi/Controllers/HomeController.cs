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
        private readonly IMongoRepository<Product> _repository;

        public HomeController(
            IOptions<MongoOptions> options,
            IMongoRepository<Product> repository)
        {
            _options = options;
            _repository = repository;
        }

        [HttpGet, Route("~/")]
        public IActionResult Index()
        {
            //return "MongoOptions: " + _options.Value.ConnectionString + ", " + _options.Value.Database
            //    + ", " + _options.Value.Document;

            return Ok(_repository.List(a => a.Id));
        }
    }
}
