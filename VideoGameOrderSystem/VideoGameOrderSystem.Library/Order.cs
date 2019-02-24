using System;
using System.Collections.Generic;
using System.Text;

namespace VideoGameOrderSystem.Library
{
    class Order
    {
        public Customer Customer { get; set; } = new Customer();
        public Location StoreLocation { get; set; } = new Location();

        public DateTime TimePlaced { get; set; }

        List<Product> products { get; set; } = new List<Product>();
    }
}
