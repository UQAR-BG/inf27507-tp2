using INF27507_Boutique_En_Ligne.Models;
using INF27507_Boutique_En_Ligne.Models.FormData;
using INF27507_Boutique_En_Ligne.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Client")]
    [Produces("application/json")]
    public class ClientController : ControllerBase
    {
        private readonly IDatabaseAdapter _database;
        private readonly IJwtHandler _jwtHandler;

        public ClientController(IDatabaseAdapter database, IJwtHandler jwtHandler)
        {
            _database = database;
            _jwtHandler = jwtHandler;
        }

        //recupérer les informations du client
        [HttpGet]
        [Route("/api/infos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInfo()
        {
            UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);

            Client client = _database.GetClient(userContext.Email);

            return Ok(client);
        }

        //mettre a jour les informations du client
        [HttpPatch]
        [Route("/api/infos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateInfo([FromForm] UserInfo userInfo)
        {
            return Ok();
        }

        //recupérer une liste de produits par mots clés
        [HttpGet]
        [Route("/api/product/{keyword}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProduct([FromRoute] string keyword)
        {
            List<Product> products = new List<Product>();
            return Ok(products);
        }

        //enlever le produit du panier
        [HttpDelete]
        [Route("/api/cart/{product_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProduct([FromRoute] int product_id)
        {
            return Ok();
        }

        //changer la quantité d'un produit
        //[HttpPatch]
        //[Route("/api/product/{product_id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<IActionResult> UpdateProduct([FromRoute] int quantity_product)
        //{
        //    return Ok();
        //}

        //[HttpGet]
        //[Route("/api/product/{product_id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<IActionResult> GetProduct([FromRoute] int product_id)
        //{
        //    //List<Product> products = new List<Product>();
        //    Product product = new Product();
        //    return Ok(product);
        //}


    }
}
