using System;
using System.Collections.Generic;
using System.Text;

namespace VideoGameOrderSystem.DataAccess.Repos
{
    public interface IOrderRepository
    {
        // Logic to handle the placement of orders
        IEnumerable<Models.Order> GetAllOrders();
        Models.Order GetOrderById(int id);
        void AddOrder(Models.Order order);
        void RemoveOrder(int id);
        float OrderTotal(int orderID, IEnumerable<Models.OrderItems> oI, 
            IEnumerable<Models.Product> p);

        IEnumerable<Models.OrderItems> GetOrderItems(int orderId);
        IEnumerable<Models.Product> GetOrderProducts(IEnumerable<Models.OrderItems> oI);
        bool OrderIsEmpty(int orderId);
        void AddOrderItems(int orderId, int productId);
        void RemoveOrderItems(int orderId, int productId);
        void AddToOrder(int orderId, int storeId, int productId, int val);
        void RemoveFromOrder(int orderId, int storeId, int productId, int val);
        void AddProduct(int orderId, Models.Product p);
        void RemoveProduct(int orderId, int id);

        Models.Product getProductByName(string name);
        bool checkProduct(int productId, int orderId);

        void PrintCustomerHistory(int customerID);
        void PrintStoreHistory(int storeID);

        // Order Statistics
        void HighestAverageCustomer();
        void HighestSumTotal();
        void MostOrders();
    }
}
