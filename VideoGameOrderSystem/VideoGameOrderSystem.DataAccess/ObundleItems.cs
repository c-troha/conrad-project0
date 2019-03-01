using System;
using System.Collections.Generic;

namespace VideoGameOrderSystem.DataAccess
{
    public partial class ObundleItems
    {
        public int BundleId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }

        public virtual Obundles Bundle { get; set; }
        public virtual Oproduct Product { get; set; }
    }
}
