using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoGameOrderSystem.Library
{
    public class Location
    {
        private List<Product> Inventory { get; set; } = new List<Product>();

        public bool PlaceOrder(Order order)
        {
            int count = order.products.Count;

            if(count == 0)
            {
                throw new ArgumentException("Order must contain at least one product.", nameof(order));
            }

            if (!CheckInventory(order))
            {
                Console.WriteLine("We could not place your order. Please adjust the order and try again.");
                return false;
            }

            for (int i = 0; i < count; i++)
            {
                
            }


            return false;
        }

        private bool CheckInventory(Order order)
        {
            HashSet<int> ids = new HashSet<int>(order.products.Select(s => s.Id));
            var matches = Inventory.Where(p => ids.Contains(p.Id));

            // product entered in order doesn't exist in the inventory
            if (matches.Count() == 0) return false;

            for (int i = 0; i < matches.Count(); i++)
            {
                if()
                {
                    Console.WriteLine($"{order.products[i].Name} is currently out of stock.");
                    return false;
                }
            }

            return true;
        }
    }
}
