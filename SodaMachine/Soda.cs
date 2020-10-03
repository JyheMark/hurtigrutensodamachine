using System;
using System.Collections.Generic;
using System.Text;

namespace SodaMachine
{
    class Soda
    {
        public string Name { get; set; }
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
