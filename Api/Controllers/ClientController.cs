using INF27507_Boutique_En_Ligne.Models;
using INF27507_Boutique_En_Ligne.Models.FormData;
using INF27507_Boutique_En_Ligne.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetInfo()
        {
            try
            {
                UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);
                Client client = _database.GetClientWithUserName(userContext.UserName);

                UserInfo infos = UserInfo.CreateFrom(client);
                infos.Balance = client.Balance;

                return Ok(infos);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //mettre a jour les informations du client
        [HttpPatch]
        [Route("/api/infos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateInfo([FromForm] UpdateUserInfo userInfo)
        {
            try
            {
                UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);

                Client client = _database.GetClientWithUserName(userContext.UserName);
                client.Email = userInfo.Email ?? client.Email;
                client.PhoneNumber = userInfo.Phone ?? client.PhoneNumber;
                client.Lastname = userInfo.LastName ?? client.Lastname;
                client.Firstname = userInfo.Firstname ?? client.Firstname;
                client.Balance = userInfo.Balance ?? client.Balance;

                _database.UpdateClientInfo(client);

                UserInfo infos = UserInfo.CreateFrom(client);
                infos.Balance = client.Balance;

                return Ok(infos);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //recupérer une liste de produits par mots clés
        [HttpGet]
        [Route("/api/product/{keyword}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct([FromRoute] string keyword)
        {
            try
            {
                if (string.IsNullOrEmpty(keyword))
                    return NotFound();

                List<Product> products = _database.GetProducts(keyword);
                return Ok(products);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //recuperer une liste de produits avec un identifiant
        [HttpGet]
        [Route("/api/products/{product_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProduct([FromRoute] int product_id)
        {
            try
            {
                Product product = _database.GetProduct(product_id);
                if (product == null)
                    return NotFound(string.Empty);

                return Ok(product);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        
        [HttpGet]
        [Route("/api/cart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCart()
        {
            try
            {
                UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);

                Client client = _database.GetClientWithUserName(userContext.UserName);
                Cart cart = _database.CreateActiveCartIfNotExist(client.Id);
                return Ok(cart);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        
        //ajouter un nouvaeu produit
        [HttpPost]
        [Route("/api/cart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddToCart([FromForm] int productId, [FromForm] int itemQuantity)
        {
            try
            {
                UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);

                Client client = _database.GetClientWithUserName(userContext.UserName);
                Product product = _database.GetProduct(productId);
                if (product == null)
                    return NotFound(string.Empty);

                Cart cart = _database.CreateActiveCartIfNotExist(client.Id);
                _database.AddItem(client.Id, productId, itemQuantity);
                return Ok(cart);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //changer la quantité
        [HttpPatch]
        [Route("/api/cart/{product_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPoduct([FromRoute] int product_id, [FromForm] int newQuantity)
        {
            try
            {
                UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);

                Client client = _database.GetClientWithUserName(userContext.UserName);
                List<CartItem> cartItems = _database.CreateActiveCartIfNotExist(client.Id).Items.ToList();
                if (cartItems.All(o => o.ProductId != product_id))
                    return NotFound($"Aucun produit avec l'identifiant {product_id}");

                CartItem cartItem = cartItems.First(o => o.ProductId == product_id);
                _database.UpdateItem(client.Id, product_id, newQuantity);
                return Ok(cartItems);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //enlever le produit du panier
        [HttpDelete]
        [Route("/api/cart/{product_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveFromCart([FromRoute] int product_id)
        {
            try
            {
                UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);

                Client client = _database.GetClientWithUserName(userContext.UserName);
                List<CartItem> cartItems = _database.CreateActiveCartIfNotExist(client.Id).Items.ToList();
                if (cartItems.All(o => o.ProductId != product_id))
                    return NotFound($"Aucun produit avec l'identifiant {product_id}");

                _database.RemoveItem(client.Id, product_id);

                return Ok(_database.GetActiveCart(client.Id));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //faire le paiement
        [HttpGet]
        [Route("/api/pay")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPay()
        {
            try
            {
                UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);

                Client client = _database.GetClientWithUserName(userContext.UserName);
                Cart cart = _database.GetActiveCart(client.Id);
                if (cart == null || cart.Items.Count == 0)
                    return BadRequest("Aucun produit dans le panier");

                double cartTotal = _database.GetCartTotal(cart.Id);

                if (client.Balance < cartTotal)
                    return BadRequest("Votre solde est insuffisant");

                _database.UpdateClientBalance(client, cartTotal);
                int orderId = _database.CreateOrder(client.Id, new PaymentMethod() { Id = 1 });
                Order order = _database.GetOrder(orderId);
                return Ok(order);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //recuperer la liste des factures
        [HttpGet]
        [Route("/api/invoices")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFacture()
        {
            try
            {
                UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);

                Client client = _database.GetClientWithUserName(userContext.UserName);
                List<Order> orders = _database.GetOrders(client);
                return Ok(orders);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //recuperer une facture en particulier
        [HttpGet]
        [Route("/api/invoices/{invoice_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFacture([FromRoute] int invoice_id) 
        {
            try
            {
                UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);

                Client client = _database.GetClientWithUserName(userContext.UserName);
                Order order = _database.GetOrder(invoice_id);
                if (order == null || order.Id != invoice_id || order.Cart.ClientId != client.Id)
                    return NotFound($"Aucune facture avec l'identifiant {invoice_id} pour ce client");

                return Ok(order);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //recuperer les stats
        [HttpGet]
        [Route("/api/stats")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStats()
        {
            try
            {
                UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);

                Client client = _database.GetClientWithUserName(userContext.UserName);
                List<Order> orders = _database.GetOrders(client);

                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"total", $"{orders.Sum(o => o.Cart.Items.Sum(i => i.Quantity * i.SalePrice)):C}"},
                    {"art", orders.Sum(o => o.Cart.Items.Sum(i => i.Quantity)).ToString("N0", CultureInfo.CreateSpecificCulture("fr-FR"))}
                };

                return Ok(data);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
