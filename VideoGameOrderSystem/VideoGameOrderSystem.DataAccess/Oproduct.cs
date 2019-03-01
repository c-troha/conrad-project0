using System;
using System.Collections.Generic;

namespace VideoGameOrderSystem.DataAccess
{
    public partial class Oproduct
    {
        public Oproduct()
        {
            ObundleItems = new HashSet<ObundleItems>();
            OrderItems = new HashSet<OrderItems>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<ObundleItems> ObundleItems { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
