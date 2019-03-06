using System;
using System.Collections.Generic;
using System.Text;

namespace VideoGameOrderSystem.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public int StoreId { get; set; }

        public DateTime TimePlaced { get; set; }
    }
}
