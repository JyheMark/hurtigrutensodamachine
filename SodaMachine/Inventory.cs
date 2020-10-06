using System;
using System.Collections.Generic;
using System.Text;

namespace SodaMachine
{
    class Inventory
    {
        private List<InventoryItem> _inventory;

        public Inventory()
        {
            _inventory = new List<InventoryItem>();
        }

        public void AddSoda(Soda soda, int stock)
        {
            _inventory.ForEach((inventoryItem) => 
            {
                if (inventoryItem.Soda.Id.Equals(soda.Id))
                    return;
            });

            _inventory.Add(new InventoryItem(soda, stock));
        }

        private class InventoryItem
        {
            public Soda Soda { get; set; }
            public int Stock { get; set; }

            public InventoryItem(Soda soda, int stock)
            {
                this.Soda = soda;
                this.Stock = stock;
            }
        }
    }
}
