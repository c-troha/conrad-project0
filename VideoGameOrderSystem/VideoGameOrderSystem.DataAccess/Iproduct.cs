using System;
using System.Collections.Generic;

namespace VideoGameOrderSystem.DataAccess
{
    public partial class Iproduct
    {
        public Iproduct()
        {
            IbundleItems = new HashSet<IbundleItems>();
            Inventory = new HashSet<Inventory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<IbundleItems> IbundleItems { get; set; }
        public virtual ICollection<Inventory> Inventory { get; set; }
    }
}
