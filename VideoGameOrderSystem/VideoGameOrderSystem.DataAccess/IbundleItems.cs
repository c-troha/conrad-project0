using System;
using System.Collections.Generic;

namespace VideoGameOrderSystem.DataAccess
{
    public partial class IbundleItems
    {
        public int BundleId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }

        public virtual Ibundles Bundle { get; set; }
        public virtual Iproduct Product { get; set; }
    }
}
