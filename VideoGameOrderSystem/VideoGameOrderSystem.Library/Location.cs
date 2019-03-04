using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoGameOrderSystem.Models
{
    public class Location
    {
        private int _locationId;
        public string Name { get; set; }
        private List<Product> _inventory = new List<Product>();

        public int LocationId
        {
            get => _locationId;

            set
            {
                _locationId = value;
            }
        }

        public void AddProductToInventory(Product p)
        {
            _inventory.Add(p);
        }

        public void RemoveProductFromInventory(Product p)
        {
            _inventory.Remove(p);
        }


        public bool Contains(int pId)
        { 
            if(_inventory.Any(p => p.Id == pId))
            {
                return true;
            }

            return false;
        }

        public bool IsEmpty()
        {
            if (_inventory.Any()) return true;
            return false;
        }


    }
}
