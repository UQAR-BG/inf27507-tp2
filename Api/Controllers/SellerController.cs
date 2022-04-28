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
        [Route("/api/seller/info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetInfo()
        {
            try
            {
                UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);
                User seller = _database.GetSellerWithUserName(userContext.UserName);

                return Ok(UserInfo.CreateFrom(seller));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPatch]
        [Route("/api/seller/info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateInfo([FromForm] UpdateSellerInfo sellerInfo)
        {
            try
            {
                UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);

                Seller seller = _database.GetSellerWithUserName(userContext.UserName);
                seller.Email = sellerInfo.Email ?? seller.Email;
                seller.PhoneNumber = sellerInfo.Phone ?? seller.PhoneNumber;
                seller.Lastname = sellerInfo.LastName ?? seller.Lastname;
                seller.Firstname = sellerInfo.Firstname ?? seller.Firstname;

                _database.UpdateSellerInfo(seller);

                return Ok(UserInfo.CreateFrom(seller));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("/api/seller/products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);
                Seller seller = _database.GetSellerWithUserName(userContext.UserName);
                List<Product> products = _database.getProductsOwnedBySeller(seller.Id);

                return Ok(products);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("/api/seller/products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProduct([FromForm] AddProductInfo productinfo)
        {
            try
            {
                UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);
                Seller seller = _database.GetSellerWithUserName(userContext.UserName);

                Product product = new Product
                {
                    Title = productinfo.Title,
                    Image = productinfo.Image,
                    ImageURL = productinfo.ImageURL,
                    Price = productinfo.Price,
                    Active = true,
                    GenderId = (int)productinfo.Gender,
                    UsageId = (int)productinfo.Usage,
                    ColourId = (int)productinfo.Colour,
                    CategoryId = (int)productinfo.Category,
                    SubCategoryId = (int)productinfo.SubCategory,
                    ProductTypeId = (int)productinfo.ProductType,
                    SellerId = seller.Id
                };
                product = _database.AddProduct(product);
                return Ok(product);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("/api/seller/products/{product_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProduct([FromRoute] int product_id)
        {
            try
            {
                Product? product = GetSellerProduct(product_id);
                if (product == null)
                    return NotFound("Product not found for this seller with id: " + product_id);

                return Ok(product);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPatch]
        [Route("/api/seller/products/{product_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct([FromRoute] int product_id, [FromForm] UpdateProduct update)
        {
            try
            {
                Product? product = GetSellerProduct(product_id);
                if (product == null)
                    return NotFound("Product not found for current seller with id: " + product_id);

                Product newProduct = _database.UpdateProduct(new ProductUpdate 
                { 
                    Id = product_id, 
                    Title = update.Title ?? product.Title,
                    Quantity = update.Quantity ?? 20,
                    Price = update.Price ?? product.Price
                });
                return Ok(newProduct);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("/api/seller/products/{product_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProduct([FromRoute] int product_id)
        {
            try
            {
                Product? product = GetSellerProduct(product_id);
                if (product == null)
                    return NotFound("Product not found for current seller with id: " + product_id);

                _database.DeleteProduct(product_id, true);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("/api/seller/stats")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStats()
        {
            try
            {
                UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);

                Seller seller = _database.GetSellerWithUserName(userContext.UserName);
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
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private Product? GetSellerProduct(int id)
        {
            UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);
            Seller seller = _database.GetSellerWithUserName(userContext.UserName);
            return _database.getProductsOwnedBySeller(seller.Id).FirstOrDefault(p => p.Id == id);
        }
    }
}