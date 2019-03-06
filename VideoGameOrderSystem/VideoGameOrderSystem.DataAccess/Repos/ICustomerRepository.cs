using System;
using System.Collections.Generic;
using System.Text;

namespace VideoGameOrderSystem.DataAccess.Repos
{
    public interface ICustomerRepository
    {
        IEnumerable<Models.Customer> GetAll();
        Models.Customer GetCustomerById(int id);
        Models.Customer GetCustomerByName(string fName, string lName);
        void AddCustomer(Models.Customer customer);
        void RemoveCustomer(int id);
        bool ContainsId(int id);
        Models.Customer CreateNewCustomer();
        void UpdateLocation(int storeId, int customerId);

    }
}
