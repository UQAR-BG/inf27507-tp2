using INF27507_Boutique_En_Ligne.Models;
using INF27507_Boutique_En_Ligne.Models.FormData;
using INF27507_Boutique_En_Ligne.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Seller")]
    [Produces("application/json")]
    public class SellerController : ControllerBase
    {
        private readonly IDatabaseAdapter _database;

        public SellerController(IDatabaseAdapter database)
        {
            _database = database;
        }

        [HttpGet]
        [Route("/api/info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInfo()
        {
            UserInfo userInfo = new UserInfo()
            {
                LastName = "User",
                Firstname = "Test",
                Email = "test.user@gmail.com",
                Phone = "418-123-4567"
            };

            return Ok(userInfo);
        }

        [HttpPatch]
        [Route("/api/info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateInfo([FromForm] UserInfo userInfo)
        {
            return Ok();
        }

        [HttpGet]
        [Route("/api/products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts()
        {
            List<Product> products = new List<Product>();

            return Ok(products);
        }

        [HttpPost]
        [Route("/api/products")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddProduct([FromForm] Product product)
        {
            return CreatedAtAction(nameof(GetProduct), product);
        }

        [HttpGet]
        [Route("/api/product/{product_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProduct([FromRoute] int product_id)
        {
            Product product = new Product();

            return Ok(product);
        }

        [HttpPatch]
        [Route("/api/product/{product_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProduct([FromRoute] int product_id, [FromForm] ProductUpdate update)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("/api/product/{product_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProduct([FromRoute] int product_id)
        {
            return Ok();
        }

        [HttpGet]
        [Route("/api/stats")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStats()
        {
            SellerStats stats = new SellerStats()
            {
                TotalSales = 1000.00,
                Profits = 150.00,
                Quantity = 25
            };

            return Ok(stats);
        }
    }
}
