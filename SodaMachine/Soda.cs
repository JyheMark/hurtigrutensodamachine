using System;
using System.Collections.Generic;
using System.Text;

namespace SodaMachine
{
    public class Soda
    {
        public string Id { get; private set; }
        public int Price { get; private set; }

        public string Name { get; private set; }

        public Soda(string sodaName, int sodaPrice)
        {
            this.Name = sodaName;
            this.Id = sodaName.ToLower();
            this.Price = sodaPrice;
        }
    }
}
