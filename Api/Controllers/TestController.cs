using INF27507_Boutique_En_Ligne.Models;
using INF27507_Boutique_En_Ligne.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Tests")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IDatabaseAdapter _database;

        public TestController(IDatabaseAdapter database)
        {
            _database = database;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<Colour> colours = _database.GetColours();

            return colours.Select(c => c.Name).ToArray();
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
