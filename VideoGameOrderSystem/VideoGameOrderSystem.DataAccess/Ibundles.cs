using System;
using System.Collections.Generic;

namespace VideoGameOrderSystem.DataAccess
{
    public partial class Ibundles
    {
        public Ibundles()
        {
            IbundleItems = new HashSet<IbundleItems>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<IbundleItems> IbundleItems { get; set; }
    }
}
