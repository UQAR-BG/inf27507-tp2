using INF27507_Boutique_En_Ligne.Models;
using INF27507_Boutique_En_Ligne.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;

        public TestController(IDatabaseAdapter database, UserManager<IdentityUser> userManager)
        {
            _database = database;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Get()
        {
            IdentityUser user = _userManager.GetUserAsync(HttpContext.User).Result;

            if (_userManager.IsInRoleAsync(user, "Client").Result)
            {
                List<Colour> colours = _database.GetColours();

                return Ok(colours.Select(c => c.Name).ToArray());
            }

            return Unauthorized();
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
