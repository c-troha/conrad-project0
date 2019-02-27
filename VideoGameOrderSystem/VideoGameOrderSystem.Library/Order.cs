using System;
using System.Collections.Generic;
using System.Text;

namespace VideoGameOrderSystem.Library
{
    public class Order
    {
        private int _id;

        public Customer Customer { get; set; } = new Customer();
        public Location StoreLocation { get; set; } = new Location();

        public DateTime TimePlaced { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();

        public int Id
        {
            get => _id;

            set
            {
                _id = value;
            }
        }
    }
}
