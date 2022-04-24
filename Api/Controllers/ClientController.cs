using INF27507_Boutique_En_Ligne.Models;
using INF27507_Boutique_En_Ligne.Models.FormData;
using INF27507_Boutique_En_Ligne.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetInfo()
        {
            UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);

            Client client = _database.GetClient(userContext.Email);

            return Ok(new UserInfo()
            {
                Email = client.Email,
                Balance = client.Balance,
                Firstname = client.Firstname,
                LastName = client.Lastname,
                Phone = client.PhoneNumber
            });
            return Ok(client);
        }

        //mettre a jour les informations du client
        [HttpPatch]
        [Route("/api/infos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateInfo([FromForm] UserInfo userInfo)
        {
            UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);
            Client client = _database.GetClient(userContext.Email);
            client.Email = userInfo.Email;
            client.PhoneNumber = userInfo.Phone;
            client.Lastname = userInfo.LastName;
            client.Firstname = userInfo.Firstname;
            client.Balance = userInfo.Balance;
            _database.UpdateClientInfo(client);
            //_database.UpdateClientBalance(client);
            return Ok(client);
        }

        //recupérer une liste de produits par mots clés
        [HttpGet]
        [Route("/api/product/{keyword}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProduct([FromRoute] string keyword)
        {
            //Product product = _database.GetProduct(keyword);
            List<Product> products = _database.GetProducts(keyword);
            if (string.IsNullOrEmpty(keyword))
            {
                return NotFound();
            }
            return Ok(products);
        }

        //recuperer une liste de produits avec un identifiant
        [HttpGet]
        [Route("/api/products/{product_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProduct([FromRoute] int product_id)
        {
            //List<Product> products = _database.GetProduct(Product_id) ;
            Product product = _database.GetProduct(product_id);
            if (product_id == null)
            {
                return NotFound(string.Empty);
            }

            return Ok(product);
        }
        
        [HttpGet]
        [Route("/api/cart/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCart()
        {
            UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);
            Client client = _database.GetClient(userContext.Email);
            Cart cart = _database.CreateActiveCartIfNotExist(client.Id);
            return Ok(cart);
        }
        
        //ajouter un nouvaeu produit
        [HttpPost]
        [Route("/api/cart/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddToCart([FromForm] int product, [FromForm] int itemQuantity)
        {
            UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);
            Client client = _database.GetClient(userContext.Email);
            Product product1 = _database.GetProduct(product);
            if (product1 == null || product1.Id == 0)
            {
                return NotFound(string.Empty);
            }
            Cart cart = _database.CreateActiveCartIfNotExist(client.Id);
            _database.AddItem(client.Id, product, itemQuantity);
            return Ok(cart);
        }

        //changer la quantité
        [HttpPatch]
        [Route("/api/cart/{product_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPoduct([FromRoute] int product_id, [FromForm] int newQuantity)
        {
            UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);
            Client client = _database.GetClient(userContext.Email);
            List<CartItem> cartItems = _database.CreateActiveCartIfNotExist(client.Id).Items.ToList();
            if (cartItems.All(o => o.ProductId != product_id))
            {
                return NotFound($"Aucun produit avec l'identifiant {product_id}");
            }
            CartItem cartItem = cartItems.First(o => o.ProductId == product_id);
            _database.UpdateItem(client.Id, product_id, newQuantity);
            return Ok(cartItems);
        }

        //enlever le produit du panier
        [HttpDelete]
        [Route("/api/cart/{product_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveFromCart([FromRoute] int product_id)
        {

            UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);
            Client client = _database.GetClient(userContext.Email);
            List<CartItem> cartItems = _database.CreateActiveCartIfNotExist(client.Id).Items.ToList();
            if (cartItems.All(o => o.ProductId != product_id))
            {
                return NotFound($"Aucun produit avec l'identifiant {product_id}");
            }

            _database.RemoveItem(client.Id, product_id);

            return Ok(_database.GetActiveCart(client.Id));
        }


        //faire le paiement
        [HttpGet]
        [Route("/api/pay/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPay()
        {
            UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);
            Client client = _database.GetClient(userContext.Email);
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

        //recuperer la liste des factures
        [HttpGet]
        [Route("/api/invoices/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFacture()
        {
            UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);
            Client client = _database.GetClient(userContext.Email);
            List<Order> orders = _database.GetOrders(client);
            return Ok(orders);
        }

        //recuperer une facture en particulier
        [HttpGet]
        [Route("/api/invoices/{invoice_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFacture([FromRoute] int invoice_id) 
        {
            UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);
            Client client = _database.GetClient(userContext.Email);
            Order order = _database.GetOrder(invoice_id);
            if (order == null || order.Id != invoice_id || order.Cart.ClientId != client.Id)
                return NotFound($"Aucune facture avec l'identifiant {invoice_id} pour ce client");
            return Ok(order);
        }

        //recuperer les stats
        [HttpGet]
        [Route("/api/stats/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStats()
        {
            UserContext userContext = _jwtHandler.GetUserContext(HttpContext.Request);
            Client client = _database.GetClient(userContext.Email);
            //List<CartItem> items = _database.GetCartItems(client);
            List<Order> orders = _database.GetOrders(client);
            Dictionary<string, string> data = new Dictionary<string, string>
            {
            {"total", $"{orders.Sum(o => o.Cart.Items.Sum(i => i.Quantity * i.SalePrice)):C}"},
            {"art", orders.Sum(o => o.Cart.Items.Sum(i => i.Quantity)).ToString("N0", CultureInfo.CreateSpecificCulture("fr-FR"))}
            };

            return Ok(data);
        }











        //changer la quantité d'un produit
        //[HttpPatch]
        //[Route("/api/product/{product_id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<IActionResult> UpdateProduct([FromRoute] int quantity_product)
        //{
        //    return Ok();
        //}

        //[HttpGet("{product_id}", Name = "GetProduct")]
        //[Route("/api/product/{product_id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<IActionResult> GetProduct([FromRoute] int product_id)
        //{
        //    //List<Product> products = new List<Product>();
        //    Product product = new Product();
        //    return Ok(product);
        //}

        //[HttpGet]
        //[Route("/api/product/{product_id}")]
        //public async Task<IActionResult> GetProduct([FromRoute]int product_id)
        //{
        //    try
        //    {
        //        Product product = new Product();
        //        return Ok(product); 
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}



    }
}
