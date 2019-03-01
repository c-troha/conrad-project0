using System;
using System.Collections.Generic;

namespace VideoGameOrderSystem.DataAccess
{
    public partial class OrderItems
    {
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Oproduct Product { get; set; }
    }
}
