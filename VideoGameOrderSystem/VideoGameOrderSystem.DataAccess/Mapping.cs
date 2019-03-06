using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoGameOrderSystem.DataAccess
{
    class Mapping
    {
        public static Models.Location Map(Store store) => new Models.Location
        {
            LocationId = store.Id,
            Name = store.Name,
        };

        public static Store Map(Models.Location store) => new Store
        {
            Id = store.LocationId,
            Name = store.Name,
        };

        public static Models.Customer Map(Customer customer) => new Models.Customer
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Birthday = customer.Birthday,
            StoreId = customer.StoreId,
        };

        public static Customer Map(Models.Customer customer) => new Customer
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Birthday = customer.Birthday,
            StoreId = customer.StoreId,
        };

        public static Models.Order Map(Orders order) => new Models.Order
        {
            Id = order.Id,
            StoreId = order.StoreId,
            CustomerId = order.CustomerId,
            TimePlaced = order.TimePlaced
        };

        public static Orders Map(Models.Order order) => new Orders
        {
            Id = order.Id,
            StoreId = order.StoreId,
            CustomerId = order.CustomerId,
            TimePlaced = order.TimePlaced
        };

        public static Models.OrderItems Map(OrderItems order) => new Models.OrderItems
        {
            OrderId = order.OrderId,
            Quantity = order.Quantity,
            ProductId = order.ProductId
        };

        public static OrderItems Map(Models.OrderItems order) => new OrderItems
        {
            OrderId = order.OrderId,
            Quantity = order.Quantity,
            ProductId = order.ProductId
        };

        public static Models.Inventory Map(Inventory inv) => new Models.Inventory
        {
            StoreId = inv.StoreId,
            ProductId = inv.ProductId,
            Quantity = inv.Quantity
        };

        public static Inventory Map(Models.Inventory inv) => new Inventory
        {
            StoreId = inv.StoreId,
            ProductId = inv.ProductId,
            Quantity = inv.Quantity
        };

        public static Models.Product MapI(Iproduct product) => new Models.Product
        {
           Id = product.Id,
           Name = product.Name,
           Price = (float)product.Price
        };

        public static Iproduct MapI(Models.Product product) => new Iproduct
        {
            Id = product.Id,
            Name = product.Name,
            Price = (decimal)product.Price
        };

        public static Models.Product MapO(Oproduct product) => new Models.Product
        {
            Id = product.Id,
            Name = product.Name,
            Price = (float)product.Price
        };

        public static Oproduct MapO(Models.Product product) => new Oproduct
        {
            Id = product.Id,
            Name = product.Name,
            Price = (decimal)product.Price
        };

        public static IEnumerable<Models.Order> Map(IEnumerable<Orders> orders) => orders.Select(Map);
        public static IEnumerable<Orders> Map(IEnumerable<Models.Order> orders) => orders.Select(Map);
    }
}
