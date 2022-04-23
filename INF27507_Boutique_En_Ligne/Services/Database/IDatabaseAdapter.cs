﻿using INF27507_Boutique_En_Ligne.Models;
using Microsoft.EntityFrameworkCore;

namespace INF27507_Boutique_En_Ligne.Services
{
    public interface IDatabaseAdapter
    {
        DbContext GetContext();
        Client GetClient(int Id);
        Client GetClient(string email);
        void AddClient(Client client);
        void UpdateClientInfo(Client client);
        List<Client> GetClients();
        Seller GetSeller(int id);
        Seller GetSeller(string email);
        List<Seller> GetSellers();
        void AddSeller(Seller seller);
        void UpdateSellerInfo(Seller seller);
        void UpdateClientBalance(Client client, double amountToPay);
        List<Product> GetProducts();
        Product GetProduct(int id);
        Product GetProductForValidation(int id);
        List<Product> getProductsOwnedBySeller(int id);
        bool ProductIsOwnedBy(int productId, int sellerId);
        Product AddProduct(Product product);
        Product UpdateProduct(ProductUpdate update);
        void DeleteProduct(int id);
        public void DeleteProduct(int id, bool FullDelete);
        Cart GetActiveCart(int clientId);
        double GetCartTotal(int cartId);
        Cart CreateActiveCart(int clientId);
        Cart CreateActiveCartIfNotExist(int clientId);
        List<CartItem> GetCartItems(int cartId);
        List<CartItem> GetCartItems(Seller seller);
        CartItem GetCartItem(int cartId, int productId);
        void AddItem(int clientId, int productId, int quantity);
        void AddItem(CartItem item);
        void UpdateItem(int clientId, int productId, int quantity);
        void UpdateItem(CartItem item, int quantity);
        void RemoveItem(int clientId, int productId);
        void RemoveItem(CartItem item);
        List<PaymentMethod> GetPaymentMethods();
        Order GetOrder(int id);
        List<Order> GetOrders(Client client);
        List<Order> GetOrders(Seller seller);
        int CreateOrder(int clientId, PaymentMethod method);
        List<Gender> GetGenders();
        List<Usage> GetUsages();
        List<Colour> GetColours();
        List<Category> GetCategories();
        List<SubCategory> GetSubCategories();
        List<ProductType> GetProductTypes();
        List<Product> GetProduct(string keyword);
    }
}
