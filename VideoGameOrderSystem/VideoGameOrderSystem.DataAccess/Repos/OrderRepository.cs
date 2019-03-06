using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoGameOrderSystem.Models;

namespace VideoGameOrderSystem.DataAccess.Repos
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderSystemContext _dbContext;

        public OrderRepository(OrderSystemContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IEnumerable<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();

            foreach (Orders order in _dbContext.Orders)
            {
                orders.Add(Mapping.Map(order));
            }

            return orders;
        }

        public Order GetOrderById(int id)
        {
            return Mapping.Map(_dbContext.Orders.First(s => s.Id == id));
        }

        public void AddOrder(Order order)
        {
            _dbContext.Add(Mapping.Map(order));
            _dbContext.SaveChanges();
        }

        public void RemoveOrder(int id)
        {
            if (_dbContext.Orders.Any(s => s.Id == id))
            {
                foreach (OrderItems oI in _dbContext.OrderItems.Where(i => i.OrderId == id))
                {
                    _dbContext.Remove(oI);
                }

                _dbContext.Remove(_dbContext.Orders.First(s => s.Id == id));
                _dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine($"No Store with ID: {id} exists.");
            }
        }

        public float OrderTotal(int orderID, IEnumerable<Models.OrderItems> oI, IEnumerable<Models.Product> p)
        {
            var items = oI.ToList();
            var products = p.ToList();
            float temp = 0;
            float total = 0;

            foreach (Models.OrderItems item in items)
            {
                if(item.OrderId == orderID)
                {
                    temp = products.First(s => s.Id == item.ProductId).Price * item.Quantity;
                    total += temp;
                }
            }

            return total;
        }

        public void PrintCustomerHistory(int customerID)
        {

            var customer = _dbContext.Customer.First(c => c.Id == customerID);
            var orders = Mapping.Map(_dbContext.Orders.Where(o => o.CustomerId == customerID)).ToList();


            Console.WriteLine();
            Console.WriteLine($"Order History: {customer.FirstName} {customer.LastName}");
            Console.WriteLine();

            foreach (Models.Order o in orders)
            {
                var items = GetOrderItems(o.Id);
                var products = GetOrderProducts(items);

                Console.WriteLine($"Order #: {o.Id}");
                Console.WriteLine("{0,-10}{1,-35}{2,-15}{3,-10}",
                    "ID", "Name", "Quantity", "Price");
                Console.WriteLine("{0,-10}{1,-35}{2,-15}{3,-10}",
                    "--", "----", "--------", "-----");

                foreach (Models.Product p in products)
                {
                    Console.WriteLine("{0,-10}{1,-35}{2,-15}{3,-10}",
                        p.Id, p.Name, items.First(i => i.ProductId == p.Id).Quantity,
                        p.Price);
                }

                Console.WriteLine();
                Console.WriteLine($"Order Total: " +
                    $"{OrderTotal(o.Id, items, products)}");
                Console.WriteLine();
            }
        }

        public void PrintStoreHistory(int storeID)
        {
            var store = _dbContext.Store.First(c => c.Id == storeID);
            var orders = Mapping.Map(_dbContext.Orders.Where(o => o.StoreId == storeID)).ToList();
          
            Console.WriteLine();
            Console.WriteLine($"Order History: {store.Name}");
            Console.WriteLine();

            foreach (Models.Order o in orders)
            {
                var items = GetOrderItems(o.Id);
                var products = GetOrderProducts(items);

                Console.WriteLine($"Order #: {o.Id}");
                Console.WriteLine("{0,-10}{1,-35}{2,-15}{3,-10}",
                    "ID", "Name", "Quantity", "Price");
                Console.WriteLine("{0,-10}{1,-35}{2,-15}{3,-10}",
                    "--", "----", "--------", "-----");

                foreach (Models.Product p in products)
                {
                    Console.WriteLine("{0,-10}{1,-35}{2,-15}{3,-10}",
                        p.Id, p.Name, items.First(i => i.ProductId == p.Id).Quantity,
                        p.Price);
                }

                Console.WriteLine();
                Console.WriteLine($"Order Total: " +
                    $"{OrderTotal(o.Id, items, products)}");
                Console.WriteLine();
            }
        }

        // Order handling
        public IEnumerable<Models.OrderItems> GetOrderItems(int orderId)
        {
            List<Models.OrderItems> order = new List<Models.OrderItems>();

            foreach (OrderItems item in _dbContext.OrderItems)
            {
                if (item.OrderId == orderId)
                {
                    order.Add(Mapping.Map(item));
                }
            }

            return order;
        }

        public IEnumerable<Product> GetOrderProducts(IEnumerable<Models.OrderItems> oI)
        {
            var products = new List<Models.Product>();
            var orderItems = new List<Models.OrderItems>(oI);

            foreach (Oproduct p in _dbContext.Oproduct)
            {
                foreach (Models.OrderItems i in orderItems)
                {
                    if (p.Id == i.ProductId)
                        products.Add(Mapping.MapO(p));
                }
            }

            return products;
        }

        public bool OrderIsEmpty(int orderId)
        {
            return !_dbContext.OrderItems.Any(i => i.OrderId == orderId);
        }

        public void AddOrderItems(int orderId, int productId)
        {
            var orderItems = new Models.OrderItems();

            orderItems.OrderId = orderId;
            orderItems.ProductId = productId;
            orderItems.Quantity = 0;

            _dbContext.Add(Mapping.Map(orderItems));
            _dbContext.SaveChanges();
        }

        public void RemoveOrderItems(int orderId, int productId)
        {
            _dbContext.Remove(_dbContext.OrderItems.First(i => i.ProductId == productId && i.OrderId == orderId));
            _dbContext.SaveChanges();
        }

        public void AddToOrder(int orderId, int storeId, int productId, int val)
        {
            var inv = _dbContext.Inventory.First(i => i.ProductId == productId && i.StoreId == storeId);
            var invProduct = _dbContext.Iproduct.First(p => p.Id == productId);

            if (inv.Quantity >= val)
            {
                _dbContext.OrderItems.First(i => i.ProductId == _dbContext.Oproduct.First(op => op.Name == invProduct.Name).Id && i.OrderId == orderId).Quantity += val;
                _dbContext.Inventory.First(i => i.ProductId == productId && i.StoreId == storeId).Quantity -= val;
                _dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("The inventory failed to update...");
                Console.WriteLine();
            }
        }

        public void RemoveFromOrder(int orderId, int storeId, int productId, int val)
        {
            var orderItems = _dbContext.OrderItems.First(i => i.ProductId == productId && i.OrderId == orderId);
            var inv = _dbContext.Inventory.First(i => i.ProductId == productId && i.StoreId == storeId);
            var invProduct = _dbContext.Iproduct.First(p => p.Id == productId);

            if (orderItems.Quantity >= val)
            {
                
                orderItems.Quantity -= val;
            }

            _dbContext.SaveChanges();
        }

        public void AddProduct(int orderId, Models.Product p)
        {
            if (!_dbContext.Oproduct.Any(s => s.Name == p.Name))
            {
                var product = new Models.Product();

                product.Name = p.Name;
                product.Price = (float)p.Price;

                _dbContext.Add(Mapping.MapO(product));
                _dbContext.SaveChanges();

                var newProduct = _dbContext.Oproduct.Last();

                AddOrderItems(orderId, newProduct.Id);
            }
            else
            {
                AddOrderItems(orderId, _dbContext.Oproduct.First(s => s.Name == p.Name).Id);
            }
        }

        public void RemoveProduct(int orderId, int id)
        {
            if (_dbContext.Oproduct.Any(s => s.Id == id))
            {
                var product = _dbContext.Oproduct.First(p => p.Id == id);

                RemoveOrderItems(orderId, product.Id);
                _dbContext.Remove(_dbContext.Oproduct.First(p => p.Id == id));
                _dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine($"No Product with ID: {id} exists.");
            }
        }

        public Models.Product getProductByName(string name)
        {
            return Mapping.MapO(_dbContext.Oproduct.First(p => p.Name.Equals(name)));
        }
    }
}
