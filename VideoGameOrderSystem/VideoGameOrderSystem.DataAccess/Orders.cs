using System;
using System.Collections.Generic;

namespace VideoGameOrderSystem.DataAccess
{
    public partial class Orders
    {
        public Orders()
        {
            OrderItems = new HashSet<OrderItems>();
        }

        public int Id { get; set; }
        public int StoreId { get; set; }
        public int CustomerId { get; set; }
        public DateTime TimePlaced { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
