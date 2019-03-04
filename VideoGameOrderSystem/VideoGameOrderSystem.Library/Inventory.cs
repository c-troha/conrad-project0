using System;
using System.Collections.Generic;
using System.Text;

namespace VideoGameOrderSystem.Models
{
    public class Inventory
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
