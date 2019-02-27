﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoGameOrderSystem.Library
{
    public class Location
    {
        private int _locationId;
        private List<Product> _inventory = new List<Product>();

        public OrderHistory History = new OrderHistory();

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

        public void AddItemsToInventory( int id, int amount )
        {
            _inventory.First(i => i.Id == id).Quantity += amount;
        }

        public void RemoveItemsFromInventory(int id, int amount)
        {
            _inventory.First(i => i.Id == id).Quantity -= amount;
        }

        public bool Contains(Product p)
        { 
            if(_inventory.Contains(p))
            {
                return true;
            }

            return false;
        }

        public bool IsEmpty()
        {
            if (_inventory.Count == 0) return true;
            return false;
        }

        public int CheckInventory(int id)
        {
            return _inventory.First(i => i.Id == id).Quantity;
        }


        public bool CanPlaceOrder(Order order)
        {
            int count = order.Products.Count;

            if(count == 0)
            {
                throw new ArgumentException("Order must contain at least one product.", nameof(order));
            }

            foreach (Product p in order.Products)
            {
                if (CheckInventory(p.Id) == 0)
                {
                    Console.WriteLine($"We could not place your order, as {nameof(p.Name)} is out of stock.");
                    return false;
                }
            }

            return true;
        }

    }
}
