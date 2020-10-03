﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SodaMachine
{
    class Soda
    {
        private string _name;
        private string _id;

        public string Id 
        { 
            get { return _id; } 
            private set { _id = value.ToLower(); } 
        }

        public string Name 
        {
            get { return _name; }
            set 
            { 
                _name = value; 
                Id = value.ToLower(); 
            }
        }

        public int StockCount { get; set; }
        public int Price { get; set; }

        public Soda(string name, int stockCount, int price)
        {
            this.Name = name;
            this.StockCount = stockCount;
            this.Price = price;
        }
    }
}
