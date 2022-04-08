using INF27507_Boutique_En_Ligne.Models;
using INF27507_Boutique_En_Ligne.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace INF27507_Boutique_En_Ligne.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IDatabaseAdapter _database;
        private readonly IAuthentificationAdapter _authService;

        private Cart? _activeCart;
        private int? _clientId;

        public PaymentController(IDatabaseAdapter database, IAuthentificationAdapter authService)
        {
            _database = database;
            _authService = authService;
        }

        [HttpGet]
        public IActionResult PaymentPage()
        {
            if (!CheckIfClientCanCheckout())
                return RedirectToAction("Index", "Home");

            ViewBag.Methods = _database.GetPaymentMethods();
            ViewBag.CartTotal = _database.GetCartTotal(_activeCart.Id) * 100;

            return View();
        }

        [HttpPost]
        public IActionResult CompleteOrder(int methodId)
        {
            if (!CheckIfClientCanCheckout())
                return RedirectToAction("Index", "Home");

            Client client = _database.GetClient((int)_clientId);
            double cartTotal = _database.GetCartTotal(_activeCart.Id);

            if (client.Balance < cartTotal)
                return RedirectToAction("NotEnoughBalance");

            _database.UpdateClientBalance(client, cartTotal);
            int orderId = _database.CreateOrder(client.Id, new Models.PaymentMethod() { Id = methodId });

            return RedirectToAction("OrderPage", "Order", new { orderId = orderId });
        }

        [HttpPost]
        public IActionResult StripePayment(string stripeToken, string stripeEmail)
        {
            if (!CheckIfClientCanCheckout())
                return RedirectToAction("Index", "Home");

            Client client = _database.GetClient((int)_clientId);
            Client stripeClient = _database.GetClient(stripeEmail);
            double totalAmount = _database.GetCartTotal(_activeCart.Id);

            CustomerCreateOptions optionsClient;
            if (stripeClient == null)
            {
                optionsClient = new CustomerCreateOptions
                {
                    Email = stripeEmail,
                    Name = client.Firstname + " " + client.Lastname,
                    Phone = client.PhoneNumber
                };
            }
            else
            {
                optionsClient = new CustomerCreateOptions
                {
                    Email = stripeClient.Email,
                    Name = stripeClient.Firstname + " " + stripeClient.Lastname,
                    Phone = stripeClient.PhoneNumber
                };
            }

            CustomerService customerService = new CustomerService();
            Customer customer = customerService.Create(optionsClient);
            ChargeCreateOptions chargeCreateOptions = new ChargeCreateOptions
            {
                Amount = (long)(totalAmount * 100),
                Currency = "CAD",
                Description = "Nouvelle commande depuis la boutique en ligne du Groupe 7",
                Source = stripeToken,
                ReceiptEmail = stripeEmail,
            };

            ChargeService chargeService = new ChargeService();
            Charge charge = chargeService.Create(chargeCreateOptions);
            if (charge.Status == "succeeded")
            {
                int orderId = _database.CreateOrder(client.Id, new Models.PaymentMethod() { Id = 2, Name = "Stripe" });

                return RedirectToAction("OrderPage", "Order", new { orderId = orderId });
            }

            return View("StripeError");
        }

        [HttpGet]
        public IActionResult NotEnoughBalance()
        {
            return View();
        }

        [HttpGet]
        public IActionResult StripeError()
        {
            return View();
        }

        private bool CheckIfClientCanCheckout()
        {
            _clientId = _authService.GetClientIdIfAuthenticated(HttpContext.Session);
            if (_clientId == 0)
                return false;

            _activeCart = _database.GetActiveCart((int)_clientId);
            if (_activeCart == null || _activeCart.Items.Count == 0)
                return false;

            return true;
        }
    }
}
