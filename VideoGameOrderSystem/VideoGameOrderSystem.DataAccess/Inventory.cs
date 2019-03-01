using System;
using System.Collections.Generic;

namespace VideoGameOrderSystem.DataAccess
{
    public partial class Inventory
    {
        public int StoreId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }

        public virtual Iproduct Product { get; set; }
        public virtual Store Store { get; set; }
    }
}
