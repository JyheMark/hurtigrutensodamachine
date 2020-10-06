using System;
using System.Collections.Generic;
using System.Linq;

namespace SodaMachine
{
    public class Inventory
    {
        private Func<int, bool> _makePurchaseCallback;
        public List<InventoryItem> InventoryList { get; private set; }

        public Inventory(Func<int, bool> makePurchaseCallback)
        {
            InventoryList = new List<InventoryItem>();
            _makePurchaseCallback = makePurchaseCallback;
        }

        public bool AddSoda(Soda soda, int stock)
        {
            foreach (var item in InventoryList)
            {
                if (item.Soda.Id.Equals(soda.Id))
                    return false;
            }

            InventoryList.Add(new InventoryItem(soda, stock));
            return true;
        }

        public bool TryDispenseSoda(string sodaId)
        {
            UserInteraction userInteraction = UserInteraction.Instance;
            var matchingInventory = InventoryList.Where(i => i.Soda.Id.Equals(sodaId));

            if (!matchingInventory.Any())
            {
                userInteraction.AppendSystemMessage("Drink doesn't exist");
                return false;
            }
                
            foreach (var inventoryItem in matchingInventory)
            {
                if (inventoryItem.Stock > 0)
                {
                    if (_makePurchaseCallback(inventoryItem.Soda.Price))
                    {
                        inventoryItem.Stock--;
                        userInteraction.AppendSystemMessage($"{inventoryItem.Soda.Name} dispensed!");
                        return true;
                    }
                    userInteraction.AppendSystemMessage("Not enough credit");
                }
                else 
                    userInteraction.AppendSystemMessage("Out of stock");
            }
            return false;
        }

        public class InventoryItem
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
