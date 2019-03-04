using System;
using System.Collections.Generic;
using System.Text;
using VideoGameOrderSystem.DataAccess;

namespace VideoGameOrderSystem.DataAccess.Repos
{
    public interface IStoreRepository
    {
        IEnumerable<Models.Location> GetAllStores();
        Models.Location GetStoreById(int id);
        Models.Location GetStoreByName(string name);
        void AddStore(Models.Location store);
        void RemoveStore(int id);

        //Inventory Handling
        IEnumerable<Models.Inventory> GetInventory(int storeId);
        IEnumerable<Models.Product> GetInventoryProducts(IEnumerable<Models.Inventory> inv);
        bool InventoryIsEmpty(int storeId);
        void AddInventory(int storeId, int productId);
        void RemoveInventory(int storeId, int productId);
        void AddProduct(int storeId);
        void RemoveProduct(int storeId, int id);
        void AddToInventory(int storeId, int productId, int val);
        void RemoveFromInventory(int storeId, int productId, int val);
    }
}
