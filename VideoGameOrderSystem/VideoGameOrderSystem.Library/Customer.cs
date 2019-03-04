using System;
using System.Collections.Generic;

namespace VideoGameOrderSystem.Models
{
    public class Customer
    {
        private string _firstName;
        private string _lastName;

        private int _id;

        public int Id
        {
            get => _id;

            set
            {
                _id = value;
            }
        }

        public string FirstName
        { 
            get => _firstName;

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("First Name must contain a value.", nameof(value));
                }

                _firstName = value;

            }

        }

        public string LastName
        {
            get => _lastName;

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Last Name must contain a value.", nameof(value));
                }

                _lastName = value;

            }
        }


        public DateTime Birthday;

        public int StoreId;
    }
}
