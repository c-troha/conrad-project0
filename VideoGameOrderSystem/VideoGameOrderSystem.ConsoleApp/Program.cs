using System;
using System.Collections.Generic;
using System.Linq;
using VideoGameOrderSystem.Models;

namespace VideoGameOrderSystem.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var customerData = new List<Customer>();
            var storeData = new List<Location>();

            var customerRepo = new CustomerRepository(customerData);
            var storeRepo = new StoreRepository(storeData);

            Console.WriteLine("Video Game Order System");

            bool flag = true;
            while(flag)
            {
                Console.WriteLine();
                Console.WriteLine("1:\tEnter Customer Portal");
                Console.WriteLine("2:\tDisplay or modify stores");
                Console.WriteLine("");
                Console.WriteLine();
                Console.WriteLine("Please enter a valid option or press \"q\" to quit");

                var input = Console.ReadLine();

                switch(input)
                {
                    case "1":
                        var customers = customerRepo.GetCustomers().ToList();
                        Console.WriteLine();

                        if(customers.Count() == 0)
                        {
                            Console.WriteLine("No customers in the system.");
                        }
                        else
                        {
                            for (int i=0; i< customers.Count; i++)
                            {
                                Console.WriteLine($"{i+1}. {customers[i].Id}: " +
                                    $"{customers[i].FirstName} {customers[i].LastName}");
                            }
                        }

                        bool flag2 = true;
                        while(flag2)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("1. Display Customer");
                            Console.WriteLine("2. Delete Customer");
                            Console.WriteLine("3. Login using your customer ID");
                            Console.WriteLine("");
                            Console.WriteLine("Please enter a valid option or press " +
                                "\"b\" to return to the main menu");

                            var input2 = Console.ReadLine();

                            switch(input2)
                            {
                                case "1":
                                    break;
                                case "2":
                                    break;
                                case "3":
                                    Console.WriteLine("Enter your customer ID: ");
                                    var id = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("");

                                    if (!customerRepo.ContainsId(id))
                                    {
                                        // Add new customer
                                    }
                                    else
                                    {
                                        bool flag4 = true;
                                        while(flag4)
                                        {
                                            Console.WriteLine("");
                                            Console.WriteLine("1. Place Order");
                                            Console.WriteLine("2. View Order History");
                                            Console.WriteLine("");
                                            Console.WriteLine("Please enter a valid option " +
                                                "or press \"b\" to go back");

                                            var input4 = Console.ReadLine();

                                            switch(input4)
                                            {
                                                case "1":
                                                    // place order
                                                    break;
                                                case "2":
                                                    // print order history
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
                        var stores = storeRepo.GetStore().ToList();
                        Console.WriteLine();

                        if (stores.Count() == 0)
                        {
                            Console.WriteLine("No stores in the system.");
                        }
                        else
                        {
                            for (int i = 0; i < stores.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {stores[i].LocationId}: " +
                                    $"{stores[i].Name}");
                            }
                        }

                        bool flag3 = true;
                        while (flag3)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("1. Add Store");
                            Console.WriteLine("2. Delete Store");
                            Console.WriteLine("3. Display Store by ID");
                            Console.WriteLine("");
                            Console.WriteLine("Please enter a valid option or press \"b\" to return to the main menu");

                            var input3 = Console.ReadLine();

                            switch (input3)
                            {
                                case "1":
                                    // Add Store
                                    break;
                                case "2":
                                    // Delete Store
                                    break;
                                case "3":
                                    // Display Store by ID
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
