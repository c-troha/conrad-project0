using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoGameOrderSystem.Models;

namespace VideoGameOrderSystem.DataAccess.Repos
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly OrderSystemContext _dbContext;

        public CustomerRepository(OrderSystemContext db)
        {
            _dbContext = db ?? throw new ArgumentNullException(nameof(db));
        }

        public Models.Customer CreateNewCustomer()
        {
            var newCustomer = new Models.Customer();

            Console.WriteLine("Please enter your first name:");
            newCustomer.FirstName = Console.ReadLine();

            Console.WriteLine("Please enter your last name:");
            newCustomer.LastName = Console.ReadLine();

            Console.WriteLine("Please enter your birthday:");
            Console.WriteLine("Month:");
            int month = int.Parse(Console.ReadLine());

            Console.WriteLine("Day:");
            int day = int.Parse(Console.ReadLine());

            Console.WriteLine("Year:");
            int year = int.Parse(Console.ReadLine());

            newCustomer.Birthday = new DateTime(year, month, day);

            newCustomer.StoreId = _dbContext.Store.First().Id;

            return newCustomer;
        }

        public bool ContainsId(int id)
        {
            return _dbContext.Customer.Any(c => c.Id == id);
        }

        public void AddCustomer(Models.Customer customer)
        {
            _dbContext.Add(Mapping.Map(customer));
            _dbContext.SaveChanges();
        }

        public IEnumerable<Models.Customer> GetAll()
        {
            var customers = new List<Models.Customer>();

            foreach (Customer c in _dbContext.Customer)
            {
                customers.Add(Mapping.Map(c));
            }

            return customers;
        }

        public Models.Customer GetCustomerById(int id)
        {
            if(!_dbContext.Customer.Any(c => c.Id == id))
            {
                return null;
            }

            return Mapping.Map(_dbContext.Customer.First(c => c.Id == id));
        }

        public Models.Customer GetCustomerByName(string fName, string lName)
        {
            throw new NotImplementedException();
        }

        public void RemoveCustomer(int id)
        {
            if(_dbContext.Customer.Any(c => c.Id == id))
            {
                _dbContext.Remove(_dbContext.Customer.First(c => c.Id == id));
                _dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void UpdateLocation(int storeId, int customerId)
        {
            if(_dbContext.Store.Any(s => s.Id == storeId))
            {
                _dbContext.Customer.First(c => c.Id == customerId).StoreId = storeId;
                _dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("ID not associated with a store...");
            }
        }
    }
}
