using System;
using System.Collections.Generic;

namespace VideoGameOrderSystem.DataAccess
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public int StoreId { get; set; }

        public virtual Store Store { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
