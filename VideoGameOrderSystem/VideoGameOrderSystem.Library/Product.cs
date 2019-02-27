﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VideoGameOrderSystem.Library
{
    public class Product
    {
        private int _id;
        private float _price;

        public int Quantity = 0;
        public string Name { get; set; }

        public int Id  

        {
            get { return _id; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException($"{nameof(value)} must be nonnegative");
                }

                _id = value;
            }
        }

        public float Price

        {
            get { return _price; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException($"{nameof(value)} must be nonnegative");
                }

                _price = value;
            }
        }



    }
}
