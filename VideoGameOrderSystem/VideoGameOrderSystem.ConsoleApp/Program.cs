using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VideoGameOrderSystem.Models;
using VideoGameOrderSystem.DataAccess;
using VideoGameOrderSystem.DataAccess.Repos;

namespace VideoGameOrderSystem.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder<OrderSystemContext>();
            optionsBuilder.UseSqlServer(SecretConfiguration.ConnectionString);
            var options = optionsBuilder.Options;


            var dbContext = new OrderSystemContext(options);
            IStoreRepository storeRepository = new StoreRepository(dbContext);
            ICustomerRepository customerRepository = new CustomerRepository(dbContext);

            Console.WriteLine("Video Game Order System");

            bool flag = true;
            while(flag)
            {
                Console.WriteLine();
                Console.WriteLine("1:\tEnter Customer Portal");
                Console.WriteLine("2:\tStore Records");
                Console.WriteLine("3:\tCustomer Records");
                Console.WriteLine("");
                Console.WriteLine();
                Console.WriteLine("Please enter a valid option or press \"q\" to quit");

                var input = Console.ReadLine();

                switch(input)
                {
                    case "1":
                        var customers = customerRepository.GetAll().ToList();
                        Console.WriteLine();

                        bool flag2 = true;
                        while (flag2)
                        {
                            if (customers.Count() == 0)
                            {
                                Console.WriteLine("");
                                Console.WriteLine("No customers in the system.");
                            }
                            else
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Customers in the system:");

                                for (int i = 0; i < customers.Count; i++)
                                {
                                    Console.WriteLine($"Name:\t{customers[i].FirstName} {customers[i].LastName}");
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
                                        if(response.Equals("y"))
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
                                            Console.WriteLine("3. Change Order Location");
                                            Console.WriteLine("");
                                            Console.WriteLine("Please enter a valid option " +
                                                "or press \"b\" to go back");

                                            var input4 = Console.ReadLine();

                                            switch (input4)
                                            {
                                                case "1":
                                                    // place order
                                                    break;
                                                case "2":
                                                    // print order history
                                                    break;
                                                case "3":
                                                    // Change customer location
                                                    var stores = storeRepository.GetAllStores().ToList();
                                                    Console.WriteLine();
                                                    Console.WriteLine("Which store would you like to shop at?");
                                                    Console.WriteLine();

                                                    if (stores.Count() == 0)
                                                    {
                                                        Console.WriteLine("No stores in the system.");
                                                    }
                                                    else
                                                    {
                                                        for (int i = 0; i < stores.Count; i++)
                                                        {
                                                            Console.WriteLine($"ID: {stores[i].LocationId}\t" +
                                                                $"Name: {stores[i].Name}");
                                                        }
                                                    }

                                                    int newStore;
                                                    int.TryParse(Console.ReadLine(), out newStore);
                                                    customerRepository.UpdateLocation(newStore, id);

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

                            if (stores.Count() == 0)
                            {
                                Console.WriteLine("No stores in the system.");
                            }
                            else
                            {
                                for (int i = 0; i < stores.Count; i++)
                                {
                                    Console.WriteLine($"ID: {stores[i].LocationId}\t" +
                                        $"Name: {stores[i].Name}");
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

                                    var storeName = Console.ReadLine();

                                    store.Name = storeName;
                                    storeRepository.AddStore(store);
                               
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
                                            var myStore = storeRepository.GetStoreById(storeID);
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

                                        Console.WriteLine("");
                                        Console.WriteLine("1. Add Product");
                                        Console.WriteLine("2. Remove Product");
                                        Console.WriteLine("3. Adjust Inventory");
                                        Console.WriteLine("Please enter a valid option or press \"b\" to go back");
                                        Console.WriteLine("");


                                        var input4 = Console.ReadLine();

                                        switch(input4)
                                        {
                                            case "1":
                                                // Add Product
                                                storeRepository.AddProduct(storeID);
                                                break;
                                            case "2":
                                                // Remove Product
                                                Console.WriteLine("Product ID to be removed:");

                                                int pId;
                                                bool maybeId = int.TryParse(Console.ReadLine(), out pId);
                                                if (maybeId)
                                                {
                                                    storeRepository.RemoveProduct(storeID, pId);
                                                }
                                                break;
                                            case "3":
                                                // Adjust Inventory
                                                Console.WriteLine();
                                                Console.WriteLine("Product ID to be adjusted:");

                                                maybeId = int.TryParse(Console.ReadLine(), out pId);
                                                if(maybeId)
                                                {
                                                    Console.WriteLine();
                                                    Console.WriteLine("1. Add");
                                                    Console.WriteLine("2. Remove");
                                                    Console.WriteLine();

                                                    int val = 0;
                                                    var choice = Console.ReadLine();
                                                    switch(choice)
                                                    {
                                                        case "1":
                                                            Console.WriteLine("Enter the amount to add to inventory:");
                                                            if (int.TryParse(Console.ReadLine(), out val))
                                                            {
                                                                storeRepository.AddToInventory(storeID, pId, val);
                                                            }
                                                            break;
                                                        case "2":
                                                            Console.WriteLine("Enter the amount to remove from inventory:");
                                                            if (int.TryParse(Console.ReadLine(), out val))
                                                            {
                                                                storeRepository.RemoveFromInventory(storeID, pId, val);
                                                            }
                                                            break;
                                                        default:

                                                            break;
                                                    }
                                                }

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
                        while(flag5)
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
                            Console.WriteLine("1: Delete Customer Record");
                            Console.WriteLine("Please enter a valid option or press \"b\" to go back to the main menu");
                            Console.WriteLine("");

                            input = Console.ReadLine();

                            switch(input)
                            {
                                case "1":
                                    // Delete Customer from database
                                    Console.WriteLine("Please enter the ID of the customer you wish to delete?");
                                    Console.WriteLine();

                                    int delID;

                                    if(int.TryParse(Console.ReadLine(), out delID))
                                    {
                                        customerRepository.RemoveCustomer(delID);
                                    }
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
