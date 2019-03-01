using System;
using System.Collections.Generic;

namespace VideoGameOrderSystem.DataAccess
{
    public partial class Obundles
    {
        public Obundles()
        {
            ObundleItems = new HashSet<ObundleItems>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ObundleItems> ObundleItems { get; set; }
    }
}
