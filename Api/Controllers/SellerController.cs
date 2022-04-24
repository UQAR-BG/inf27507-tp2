using System.Text;
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
        private readonly IJwtHandler _jwtHandler;

        public SellerController(IDatabaseAdapter database, IJwtHandler jwtHandler)
        {
            _database = database;
            _jwtHandler = jwtHandler;
        }

        [HttpGet]
        [Route("/api/info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInfo()
        {
            UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);
            User seller = _database.GetSeller(userContext.Email);

            return Ok(new UserInfo()
            {
                Email = seller.Email,
                Firstname = seller.Firstname,
                LastName = seller.Lastname,
                Phone = seller.PhoneNumber
            });
        }

        [HttpPatch]
        [Route("/api/info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateInfo([FromForm] UserInfo userInfo)
        {
            UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);
            Seller seller = _database.GetSeller(userContext.Email);
            seller.Email = userInfo.Email;
            seller.PhoneNumber = userInfo.Phone;
            seller.Lastname = userInfo.LastName;
            seller.Firstname = userInfo.Firstname;
            _database.UpdateSellerInfo(seller);
            return Ok(seller);
        }

        [HttpGet]
        [Route("/api/products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts()
        {
            UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);
            Seller seller = _database.GetSeller(userContext.Email);
            List<Product> products = _database.getProductsOwnedBySeller(seller.Id);

            return Ok(products);
        }

        [HttpPost]
        [Route("/api/products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProduct([FromForm] ProductInfo productinfo)
        {
            UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);
            Seller seller = _database.GetSeller(userContext.Email);

            Dictionary<string, bool> checks = new Dictionary<string, bool>()
            {
                {"GenderId", _database.GetGenders().Exists(o => o.Id == productinfo.GenderId)},
                {"UsageId", _database.GetUsages().Exists(o => o.Id == productinfo.UsageId)},
                {"ColourId", _database.GetColours().Exists(o => o.Id == productinfo.ColourId)},
                {"CategoryId", _database.GetCategories().Exists(o => o.Id == productinfo.CategoryId)},
                {"SubCategoryId", _database.GetSubCategories().Exists(o => o.Id == productinfo.SubCategoryId)},
                {"ProductTypeId", _database.GetProductTypes().Exists(o => o.Id == productinfo.ProductTypeId)}
            };

            if (checks.ContainsValue(false))
            {
                Console.WriteLine("One of the checks failed");
                StringBuilder sb = new StringBuilder();
                checks = checks.Where(o => o.Value == false).ToDictionary(o => o.Key, o => o.Value);
                var last = checks.Last();
                foreach (KeyValuePair<string, bool> check in checks)
                {
                    sb.Append(check.Key);
                    if (check.Key != last.Key)
                    {
                        sb.Append(", ");
                    }
                }

                return BadRequest("Invalid product info : " + sb);
            }

            Product product = new Product
            {
                Title = productinfo.Title,
                Image = productinfo.Image,
                ImageURL = productinfo.ImageURL,
                Price = productinfo.Price,
                Active = productinfo.Active,
                GenderId = productinfo.GenderId,
                UsageId = productinfo.UsageId,
                ColourId = productinfo.ColourId,
                CategoryId = productinfo.CategoryId,
                SubCategoryId = productinfo.SubCategoryId,
                ProductTypeId = productinfo.ProductTypeId,
                SellerId = seller.Id
            };
            product = _database.AddProduct(product);
            return Ok(product);
        }

        [HttpGet]
        [Route("/api/product/v/{product_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProduct([FromRoute] int product_id)
        {
            Product? product = GetSellerProduct(product_id);

            if (product == null)
            {
                return NotFound("Product not found for this seller with id: " + product_id);
            }

            return Ok(product);
        }

        [HttpPatch]
        [Route("/api/products/{product_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProduct([FromRoute] int product_id, [FromForm] ProductUpdate update)
        {
            Product? product = GetSellerProduct(product_id);

            if (product == null)
            {
                return NotFound("Product not found for current seller with id: " + product_id);
            }

            Product newProduct = _database.UpdateProduct(update);
            return Ok(newProduct);
        }

        [HttpDelete]
        [Route("/api/products/{product_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProduct([FromRoute] int product_id)
        {
            Product? product = GetSellerProduct(product_id);
            if (product == null)
            {
                return NotFound("Product not found for current seller with id: " + product_id);
            }

            _database.DeleteProduct(product_id, true);

            return Ok(product);
        }

        [HttpGet]
        [Route("/api/v/stats")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStats()
        {
            UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);
            Seller seller = _database.GetSeller(userContext.Email);
            List<CartItem> items = _database.GetCartItems(seller);
            double total = items.Sum(i => i.Quantity * i.SalePrice);
            double profits = total * 0.15;
            int quantity = items.Sum(i => i.Quantity);

            Dictionary<string, string> data = new Dictionary<string, string>
            {
                {"total", string.Format("{0:C}", total)},
                {"profits", string.Format("{0:C}", profits)},
                {"quantity", quantity.ToString()}
            };
            return Ok(data);
        }

        private Product? GetSellerProduct(int id)
        {
            UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);
            Seller seller = _database.GetSeller(userContext.Email);
            return _database.getProductsOwnedBySeller(seller.Id).FirstOrDefault(p => p.Id == id);
        }
    }
}