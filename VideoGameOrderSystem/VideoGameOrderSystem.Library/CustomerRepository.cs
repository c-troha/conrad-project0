using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoGameOrderSystem.Models
{
    public class CustomerRepository
    {
        private readonly ICollection<Customer> _data;

        public CustomerRepository(ICollection<Customer> data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public IEnumerable<Customer> GetCustomers(string search = null)

        {
            if (search == null)
            {
                foreach (var item in _data)
                {
                    yield return item;
                }
            }

            else

            {
                foreach (var item in _data.Where(r => r.FirstName.Contains(search)))
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<Customer> GetCustomerByLastName(string search = null)

        {
            if (search == null)
            {
                foreach (var item in _data)
                {
                    yield return item;
                }
            }

            else

            {
                foreach (var item in _data.Where(r => r.LastName.Contains(search)))
                {
                    yield return item;
                }
            }
        }

        public Customer GetCustomerById(int id)
        {
            return _data.First(c => c.Id == id);
        }

        public bool ContainsId(int id)
        {
            if(_data.Any(c => c.Id == id))
            {
                return true;
            }

            return false;
        }


    }
}
