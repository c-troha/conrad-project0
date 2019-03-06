using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VideoGameOrderSystem.Models;
using VideoGameOrderSystem.DataAccess;
using VideoGameOrderSystem.DataAccess.Repos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace VideoGameOrderSystem.ConsoleApp
{
    class Program
    { 
        public static readonly LoggerFactory AppLoggerFactory =
#pragma warning disable CS0618 
            new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });
#pragma warning restore CS0618 
    
        static void Main(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder<OrderSystemContext>();
            optionsBuilder.UseSqlServer(SecretConfiguration.ConnectionString);
           // optionsBuilder.UseLoggerFactory(AppLoggerFactory);
            var options = optionsBuilder.Options;

            using (var dbContext = new OrderSystemContext(options))
            {
                IStoreRepository storeRepository = new StoreRepository(dbContext);
                ICustomerRepository customerRepository = new CustomerRepository(dbContext);
                IOrderRepository orderRepository = new OrderRepository(dbContext);

                Console.WriteLine("Video Game Order System");

                bool flag = true;
                while (flag)
                {
                    Console.WriteLine();
                    Console.WriteLine("1:\tEnter Customer Portal");
                    Console.WriteLine("2:\tStore Records");
                    Console.WriteLine("3:\tCustomer Records");
                    Console.WriteLine("");
                    Console.WriteLine();
                    Console.WriteLine("Please enter a valid option or press \"q\" to quit");

                    var input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":

                            bool flag2 = true;
                            while (flag2)
                            {
                                var customers = customerRepository.GetAll().ToList();
                                Console.WriteLine();

                                if (customers.Count() == 0)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("No customers in the system.");
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Customers in the system:");
                                    Console.WriteLine();
                                    Console.WriteLine("ID\tName");
                                    Console.WriteLine("--\t----");

                                    for (int i = 0; i < customers.Count; i++)
                                    {
                                        Console.WriteLine($"{customers[i].Id}\t{customers[i].FirstName} {customers[i].LastName}");
                                    }
                                }


                                Console.WriteLine("");
                                Console.WriteLine("1. Login using your customer ID");
                                Console.WriteLine("2. Create new user");
                                Console.WriteLine("");
                                Console.WriteLine("Please enter a valid option or press " +
                                    "\"b\" to return to the main menu");

                                var input2 = Console.ReadLine();

                                switch (input2)
                                {
                                    case "1":
                                        Console.WriteLine("Enter your customer ID: ");
                                        var id = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("");

                                        if (!customerRepository.ContainsId(id))
                                        {
                                            // Add new customer
                                            Console.WriteLine("This ID does not exist");
                                            Console.WriteLine("Would you like to create a new user? (y/n)");

                                            var response = Console.ReadLine();
                                            if (response.Equals("y"))
                                            {
                                                var newCustomer = customerRepository.CreateNewCustomer();
                                                customerRepository.AddCustomer(newCustomer);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Returning to customer login portal...");
                                                Console.WriteLine("");
                                            }

                                        }
                                        else
                                        {
                                            Console.WriteLine($"Welcome {customers.First(c => c.Id == id).FirstName}");

                                            bool flag4 = true;
                                            while (flag4)
                                            {
                                                Console.WriteLine("");
                                                Console.WriteLine("1. Place Order");
                                                Console.WriteLine("2. View Order History");
                                                Console.WriteLine("3. Change Store");
                                                Console.WriteLine("");
                                                Console.WriteLine("Please enter a valid option " +
                                                    "or press \"b\" to go back");

                                                var input4 = Console.ReadLine();

                                                switch (input4)
                                                {
                                                    case "1":
                                                        // place order
                                                        var stores = storeRepository.GetAllStores().ToList();

                                                        Models.Order newOrder = new Models.Order();
                                                        newOrder.CustomerId = id;
                                                        newOrder.StoreId = customerRepository.GetCustomerById(id).StoreId;
                                                        newOrder.TimePlaced = DateTime.Now;
                                                        orderRepository.AddOrder(newOrder);

                                                        var orders = orderRepository.GetAllOrders().ToList();

                                                        bool flag6 = true;
                                                        while (flag6)
                                                        {
                                                            bool submit = false;

                                                            int storeID = customerRepository.GetCustomerById(id).StoreId;
//---------------------------------------------------------------------------------------------------------------------------------------------------------------
                                                            //var history = orderRepository.GetAllOrders().
                                                            //    Where(o => o.StoreId == storeID && o.CustomerId == id).ToList();

                                                            //if (history.Any())
                                                            //{
                                                            //    if (history.Any(o => Math.Abs(o.TimePlaced.Hour - DateTime.Now.Hour) < 2))
                                                            //    {
                                                            //        Console.WriteLine();
                                                            //        Console.WriteLine("Please check back again soon.");
                                                            //        orderRepository.RemoveOrder(orders.Last().Id);
                                                            //        break;
                                                            //    }
                                                            //}
//---------------------------------------------------------------------------------------------------------------------------------------------------------------
                                                            var myStore = storeRepository.GetStoreById(storeID);
                                                            var myInventory = storeRepository.GetInventory(storeID);
                                                            var myProducts = storeRepository.GetInventoryProducts(myInventory);

                                                            Console.WriteLine();
                                                            Console.WriteLine($"Store: {myStore.Name}");
                                                            Console.WriteLine();

                                                            if (!storeRepository.InventoryIsEmpty(storeID))
                                                            {

                                                                Console.WriteLine("Inventory List");
                                                                Console.WriteLine();
                                                                Console.WriteLine("{0,-10}{1,-35}{2,-15}{3,-10}",
                                                                    "ID", "Name", "Quantity", "Price");
                                                                Console.WriteLine("{0,-10}{1,-35}{2,-15}{3,-10}",
                                                                    "--", "----", "--------", "-----");

                                                                foreach (Models.Product p in myProducts)
                                                                {
                                                                    Console.WriteLine("{0,-10}{1,-35}{2,-15}{3,-10}",
                                                                        p.Id, p.Name, myInventory.First(i => i.ProductId == p.Id).Quantity,
                                                                        p.Price);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Nothin in inventory...");
                                                            }

                                                            var myOrder = orders.Last();
                                                            var myOrderItems = orderRepository.GetOrderItems(myOrder.Id);
                                                            var orderProducts = orderRepository.GetOrderProducts(myOrderItems);
                                                            if (!orderRepository.OrderIsEmpty(myOrder.Id))
                                                            {
                                                                Console.WriteLine();
                                                                Console.WriteLine("Order Summary");
                                                                Console.WriteLine();
                                                                Console.WriteLine("{0,-10}{1,-35}{2,-15}{3,-10}",
                                                                    "ID", "Name", "Quantity", "Price");
                                                                Console.WriteLine("{0,-10}{1,-35}{2,-15}{3,-10}",
                                                                    "--", "----", "--------", "-----");

                                                                foreach (Models.Product p in orderProducts)
                                                                {
                                                                    Console.WriteLine("{0,-10}{1,-35}{2,-15}{3,-10}",
                                                                        p.Id, p.Name, myOrderItems.First(i => i.ProductId == p.Id).Quantity,
                                                                        p.Price);
                                                                }

                                                                Console.WriteLine();
                                                                Console.WriteLine($"Order Total: " +
                                                                    $"{orderRepository.OrderTotal(myOrder.Id, myOrderItems, orderProducts)}");


                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine();
                                                                Console.WriteLine("Your cart is empty.");
                                                            }

                                                            Console.WriteLine("");

                                                            Console.WriteLine("1. Add item to order");
                                                            Console.WriteLine("2. Remove item from order");
                                                            Console.WriteLine("3. Submit Order");
                                                            Console.WriteLine("");
                                                            Console.WriteLine("Please enter a valid option " +
                                                                "or press \"b\" to go back");

                                                            var input3 = Console.ReadLine();

                                                            switch (input3)
                                                            {
                                                                case "1":
                                                                    // Add Item to order
                                                                    orders = orderRepository.GetAllOrders().ToList();
                                                                    Console.WriteLine("Enter the ID of the product you wish to add:");

                                                                    int choice;
                                                                    if (int.TryParse(Console.ReadLine(), out choice))
                                                                    {
                                                                        if(myInventory.First(i => i.ProductId == choice).Quantity == 0)
                                                                        {
                                                                            Console.WriteLine();
                                                                            Console.WriteLine("Sorry, this item is out of stock!");
                                                                            break;
                                                                        }

                                                                        try
                                                                        {
                                                                            orderRepository.AddProduct(orders.Last().Id, myProducts.First(p => p.Id == choice));
                                                                        }
                                                                        catch(InvalidOperationException e)
                                                                        {
                                                                            Console.WriteLine();
                                                                            Console.WriteLine("No product found matching that ID...");
                                                                            break;
                                                                        }

                                                                        Console.WriteLine("");
                                                                        Console.WriteLine("How many would you like to add?");

                                                                        int val;
                                                                        if (int.TryParse(Console.ReadLine(), out val))
                                                                        {
                                                                            if(val <= 0)
                                                                            {
                                                                                Console.WriteLine();
                                                                                Console.WriteLine("Input value must be greater than zero...");
                                                                                break;
                                                                            }

                                                                            orderRepository.AddToOrder(orders.Last().Id, myStore.LocationId, choice, val);
                                                                        }
                                                                        else
                                                                        {
                                                                            Console.WriteLine();
                                                                            Console.WriteLine("Invalid Input...");
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.WriteLine();
                                                                        Console.WriteLine("Invalid ID...");
                                                                    }
                                                                    break;
                                                                case "2":
                                                                    // Remove Item from order
                                                                    orders = orderRepository.GetAllOrders().ToList();

                                                                    if(orderRepository.OrderIsEmpty(myOrder.Id))
                                                                    {
                                                                        Console.WriteLine();
                                                                        Console.WriteLine("There are no items to remove.");
                                                                        break;
                                                                    }

                                                                    Console.WriteLine("Enter the ID of the product you wish to Remove:");

                                                                    if (int.TryParse(Console.ReadLine(), out choice))
                                                                    {

                                                                        if(!orderRepository.checkProduct(choice, myOrder.Id))
                                                                        {
                                                                            Console.WriteLine();
                                                                            Console.WriteLine("Invalid ID...");
                                                                            break;
                                                                        }

                                                                        var returnItem = orderRepository.GetOrderItems(myOrder.Id);
                                                                        foreach (Models.OrderItems item in returnItem)
                                                                        {
                                                                            storeRepository.AddToInventory(storeID,
                                                                                myProducts.First(p => p.Name == orderProducts.
                                                                                First(i => i.Id == item.ProductId).Name).Id, item.Quantity);
                                                                        }
                                                                        orderRepository.RemoveProduct(orders.Last().Id, orderProducts.First(p => p.Id == choice).Id);

                                                                    }
                                                                    else
                                                                    {
                                                                        Console.WriteLine();
                                                                        Console.WriteLine("Invalid ID...");
                                                                    }
                                                                    break;
                                                                case "3":
                                                                    // Submit Order

                                                                    if(orderRepository.OrderIsEmpty(myOrder.Id))
                                                                    {
                                                                        Console.WriteLine();
                                                                        Console.WriteLine("To place an order it must have at least one item...");
                                                                        break;
                                                                    }

                                                                    submit = true;
                                                                    Console.WriteLine();
                                                                    Console.WriteLine("Order submitted successfully");
                                                                    Console.WriteLine();
                                                                    flag6 = false;
                                                                    break;
                                                                case "b":
                                                                    if (!submit)
                                                                    {
                                                                        var returnItem = orderRepository.GetOrderItems(myOrder.Id);
                                                                        foreach (Models.OrderItems item in returnItem)
                                                                        {
                                                                            storeRepository.AddToInventory(storeID,
                                                                                myProducts.First(p => p.Name == orderProducts.
                                                                                First(i => i.Id == item.ProductId).Name).Id, item.Quantity);
                                                                        }
                                                                        orderRepository.RemoveOrder(orders.Last().Id);
                                                                    }
                                                                    flag6 = false;
                                                                    break;
                                                                default:
                                                                    break;

                                                            }
                                                        }
                                                        break;

                                                    case "2":
                                                        // print order history
                                                        Console.WriteLine();
                                                        orderRepository.PrintCustomerHistory(id);
                                                        break;

                                                    case "3":
                                                        stores = storeRepository.GetAllStores().ToList();

                                                        Console.WriteLine("ID\tName\n--\t----");

                                                        if (stores.Count() == 0)
                                                        {
                                                            Console.WriteLine("No stores in the system.");
                                                        }
                                                        else
                                                        {
                                                            for (int i = 0; i < stores.Count; i++)
                                                            {
                                                                Console.WriteLine($"{stores[i].LocationId}\t" +
                                                                    $"{stores[i].Name}");
                                                            }
                                                        }


                                                        Console.WriteLine();
                                                        Console.WriteLine("Which store would you like to shop at?");
                                                        Console.WriteLine();

                                                        int newStore;
                                                        int.TryParse(Console.ReadLine(), out newStore);

                                                        try
                                                        {
                                                            customerRepository.UpdateLocation(newStore, id);
                                                        }
                                                        catch(ArgumentException e)
                                                        {
                                                            Console.WriteLine();
                                                            Console.WriteLine("Invalid store ID...");
                                                        }
                                                        
                                                        break;

                                                    case "b":
                                                        flag4 = false;
                                                        break;
                                                    default:
                                                        break;
                                                }
                                            }
                                        }
                                        break;
                                    case "2":
                                        var newCustomer1 = customerRepository.CreateNewCustomer();
                                        customerRepository.AddCustomer(newCustomer1);
                                        break;
                                    case "b":
                                        flag2 = false;
                                        break;
                                    default:
                                        Console.WriteLine("Please choose a valid menu option...");
                                        break;
                                }

                            }

                            break;

                        case "2":

                            bool flag3 = true;
                            while (flag3)
                            {
                                var stores = storeRepository.GetAllStores().ToList();
                                Console.WriteLine();

                                Console.WriteLine("ID\tName\n--\t----");

                                if (stores.Count() == 0)
                                {
                                    Console.WriteLine("No stores in the system.");
                                }
                                else
                                {
                                    for (int i = 0; i < stores.Count; i++)
                                    {
                                        Console.WriteLine($"{stores[i].LocationId}\t" +
                                            $"{stores[i].Name}");
                                    }
                                }

                                Console.WriteLine("");
                                Console.WriteLine("1. Add Store");
                                Console.WriteLine("2. Delete Store");
                                Console.WriteLine("3. Display Store");
                                Console.WriteLine("");
                                Console.WriteLine("Please enter a valid option or press \"b\" to return to the main menu");

                                var input3 = Console.ReadLine();

                                switch (input3)
                                {
                                    case "1":
                                        // Add Store
                                        var store = new Location();

                                        while (true)
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine("Please enter the name of the store: ");

                                            string storeName;
                                            storeName = Console.ReadLine();

                                            if (storeName.Equals("") || storeName.Equals(null))
                                            {
                                                Console.WriteLine();
                                                Console.WriteLine("Invalid store name...");
                                            }
                                            else
                                            {
                                                store.Name = storeName;
                                                storeRepository.AddStore(store);
                                                break;
                                            }
                                        }

                                        break;
                                    case "2":
                                        // Delete Store
                                        int result;

                                        Console.WriteLine("Please input the ID of the store to be deleted:");
                                        var inputId = Console.ReadLine();
                                        if (int.TryParse(inputId, out result))
                                        {
                                            storeRepository.RemoveStore(result);
                                        }
                                        break;
                                    case "3":
                                        // Display Store by ID
                                        int storeID;

                                        Console.WriteLine("Enter a valid store ID:");
                                        bool result1 = int.TryParse(Console.ReadLine(), out storeID);

                                        bool flag4 = true;
                                        while (flag4)
                                        {

                                            if (result1)
                                            {
                                                Models.Location myStore;

                                                try
                                                {
                                                    myStore = storeRepository.GetStoreById(storeID);
                                                }
                                                catch (InvalidOperationException e)
                                                {
                                                    Console.WriteLine();
                                                    Console.WriteLine("Invalid Store ID...");
                                                    break;
                                                }

                                                var myInventory = storeRepository.GetInventory(storeID);
                                                var myProducts = storeRepository.GetInventoryProducts(myInventory);

                                                Console.WriteLine();
                                                Console.WriteLine($"{storeID}: {myStore.Name}");
                                                Console.WriteLine();

                                                if (!storeRepository.InventoryIsEmpty(storeID))
                                                {

                                                    Console.WriteLine("Inventory List");
                                                    Console.WriteLine();
                                                    Console.WriteLine("{0,-10}{1,-35}{2,-15}{3,-10}",
                                                        "ID", "Name", "Quantity", "Price");
                                                    Console.WriteLine("{0,-10}{1,-35}{2,-15}{3,-10}",
                                                        "--", "----", "--------", "-----");

                                                    foreach (Models.Product p in myProducts)
                                                    {
                                                        Console.WriteLine("{0,-10}{1,-35}{2,-15}{3,-10}",
                                                            p.Id, p.Name, myInventory.First(i => i.ProductId == p.Id).Quantity,
                                                            p.Price);
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Nothin in inventory...");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine();
                                                Console.WriteLine("Invalid Store ID...");
                                                break;
                                            }

                                            Console.WriteLine("");
                                            Console.WriteLine("1. Add Product");
                                            Console.WriteLine("2. Remove Product");
                                            Console.WriteLine("3. Adjust Inventory");
                                            Console.WriteLine("4. Order History");
                                            Console.WriteLine("Please enter a valid option or press \"b\" to go back");
                                            Console.WriteLine("");


                                            var input4 = Console.ReadLine();

                                            switch (input4)
                                            {
                                                case "1":
                                                    // Add Product
                                                    storeRepository.AddProduct(storeID);
                                                    break;
                                                case "2":
                                                    // Remove Product
                                                    Console.WriteLine();
                                                    Console.WriteLine("Product ID to be removed:");

                                                    int pId;
                                                    if (int.TryParse(Console.ReadLine(), out pId))
                                                    {
                                                        try
                                                        {
                                                            storeRepository.RemoveProduct(storeID, pId);
                                                        }
                                                        catch( InvalidOperationException e)
                                                        {
                                                            Console.WriteLine();
                                                            Console.WriteLine("Invalid Product ID...");
                                                        }
                                                    }
                                                    break;
                                                case "3":
                                                    // Adjust Inventory
                                                    if(storeRepository.InventoryIsEmpty(storeID))
                                                    {
                                                        Console.WriteLine("There is no inventory to adjust...");
                                                        Console.WriteLine();
                                                        break;
                                                    }

                                                    Console.WriteLine();
                                                    Console.WriteLine("Product ID to be adjusted:");

                                                    if (int.TryParse(Console.ReadLine(), out pId))
                                                    {
                                                        Console.WriteLine();
                                                        Console.WriteLine("1. Add");
                                                        Console.WriteLine("2. Remove");
                                                        Console.WriteLine();

                                                        int val = 0;
                                                        var choice = Console.ReadLine();
                                                        switch (choice)
                                                        {
                                                            case "1":
                                                                Console.WriteLine("Enter the amount to add to inventory:");
                                                                if (int.TryParse(Console.ReadLine(), out val))
                                                                {
                                                                    try
                                                                    {
                                                                        storeRepository.AddToInventory(storeID, pId, val);
                                                                    }
                                                                    catch(InvalidOperationException e)
                                                                    {
                                                                        Console.WriteLine();
                                                                        Console.WriteLine("Invalid Product ID...");
                                                                    }
                                                                }
                                                                break;
                                                            case "2":
                                                                Console.WriteLine("Enter the amount to remove from inventory:");
                                                                if (int.TryParse(Console.ReadLine(), out val))
                                                                {
                                                                    try
                                                                    {
                                                                        storeRepository.RemoveFromInventory(storeID, pId, val);
                                                                    }
                                                                    catch (InvalidOperationException e)
                                                                    {
                                                                        Console.WriteLine();
                                                                        Console.WriteLine("Invalid Product ID...");
                                                                    }
                                                                }
                                                                break;
                                                            default:

                                                                break;
                                                        }
                                                    }

                                                    break;
                                                case "4":
                                                    orderRepository.PrintStoreHistory(storeID);
                                                    break;
                                                case "b":
                                                    flag4 = false;
                                                    break;
                                                default:
                                                    Console.WriteLine("Please enter a valid option...");
                                                    break;

                                            }

                                        }

                                        break;
                                    case "b":
                                        flag3 = false;
                                        break;
                                    default:
                                        Console.WriteLine("Please choose a valid menu option...");
                                        break;
                                }

                            }
                            break;

                        case "3":
                            var myCustomers = customerRepository.GetAll().ToList();

                            Console.WriteLine();

                            bool flag5 = true;
                            while (flag5)
                            {
                                Console.WriteLine("Customer List");
                                Console.WriteLine();
                                Console.WriteLine("{0,-10}{1,-35}{2,-25}{3,-10}",
                                    "ID", "Name", "Birthday", "Current Store");
                                Console.WriteLine("{0,-10}{1,-35}{2,-25}{3,-10}",
                                    "--", "----", "--------", "-------------");

                                foreach (Models.Customer c in myCustomers)
                                {
                                    Console.WriteLine("{0,-10}{1,-35}{2,-25}{3,-10}",
                                        c.Id, c.FirstName + " " + c.LastName,
                                        c.Birthday.Date, storeRepository.GetStoreById(c.StoreId).Name);
                                }

                                Console.WriteLine();
                                Console.WriteLine("1: Order History");
                                Console.WriteLine("2. Order Statistics");
                                Console.WriteLine("Please enter a valid option or press \"b\" to go back to the main menu");
                                Console.WriteLine("");

                                input = Console.ReadLine();

                                switch (input)
                                {
                                    case "1":
                                        Console.WriteLine("Please enter the Customer ID: ");
                                        Console.WriteLine();

                                        int histID;
                                        if (int.TryParse(Console.ReadLine(), out histID))
                                        {
                                            try
                                            {
                                                orderRepository.PrintCustomerHistory(histID);
                                            }
                                            catch(InvalidOperationException e)
                                            {
                                                Console.WriteLine();
                                                Console.WriteLine("Customer does not exist in the records..");
                                                Console.WriteLine();
                                            }
                                        }
                                        break;

                                    case "2":
                                        Console.WriteLine();
                                        orderRepository.HighestAverageCustomer();

                                        Console.WriteLine();
                                        orderRepository.HighestSumTotal();

                                        Console.WriteLine();
                                        orderRepository.MostOrders();

                                        break;

                                    case "b":
                                        flag5 = false;
                                        break;
                                    default:
                                        break;
                                }

                            }
                            break;

                        case "q":
                            flag = false;
                            break;

                        default:
                            Console.WriteLine("Please choose a valid menu option...");
                            break;
                    }
                }
            }
        }
    }
}
