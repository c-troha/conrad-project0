using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoGameOrderSystem.Models
{
    public class StoreRepository
    {
        private readonly ICollection<Location> _data;

        public StoreRepository(ICollection<Location> data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public IEnumerable<Location> GetStore(string search = null)

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
                foreach (var item in _data.Where(r => r.Name.Contains(search)))
                {
                    yield return item;
                }
            }

        }
        
        public Location GetStoreById(int id)
        {
            return _data.First(r => r.LocationId == id);
        }

        public void AddStore(Location store)
        {
            if (_data.Any(r => r.LocationId == store.LocationId))
            {
                throw new InvalidOperationException($"Store with ID {store.LocationId} already exists.");
            }

            _data.Add(store);
        }

        public void DeleteStore(Location store)
        {
            _data.Remove(_data.First(s => s.LocationId == store.LocationId));
        }

        public void AddProductToStore(Product product, Location store)
        {
            store.AddProductToInventory(product);
        }

        public void RemoveProductFromStore(Product product, Location store)
        {
            store.RemoveProductFromInventory(product);
        }

        public void AddToInventory(int storeId, int productId, int inc)
        {
            if (!_data.First(s => s.LocationId == storeId).Contains(productId))
            {
                throw new InvalidOperationException($"The product with id: {productId} does not exist at this location.");
            }

            _data.First(s => s.LocationId == storeId).AddItemsToInventory(productId, inc);
        }

        public void RemoveItemFromInventory(int storeId, int productId, int dec)
        {
            if (!_data.First(s => s.LocationId == storeId).Contains(productId))
            {
                throw new InvalidOperationException($"The product with id: {productId} does not exist at this location.");
            }

            _data.First(s => s.LocationId == storeId).RemoveItemsFromInventory(productId, dec);
        }


    }
}
