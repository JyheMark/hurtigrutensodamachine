using System;
using System.Collections.Generic;
using System.Text;

namespace SodaMachine
{
    class SystemInformation
    {
        private Inventory _inventory;
        private MoneyHandler _moneyHandler;

        public SystemInformation(Inventory inventory, MoneyHandler moneyHandler)
        {
            _inventory = inventory;
            _moneyHandler = moneyHandler;
        }

        public bool ShowSystemInformation(string args)
        {
            string output = String.Empty;
            UserInteraction userInteraction = UserInteraction.Instance;

            output += "########System Information##########\n\n";
            output += "Stock\n\n";
            foreach (var inventoryItem in _inventory.InventoryList)
            {
                output += String.Format("{0, -12}: {1}", inventoryItem.Soda.Name, inventoryItem.Stock + "\n");
            }

            output += "Statistics\n";
            output += $"Total money in: {_moneyHandler.TotalMoneyIn}\n";
            output += $"Total purchases: {_moneyHandler.TotalTransactions}\n";

            userInteraction.AppendMessageToOutput(output);
            userInteraction.SendOutput();
            userInteraction.GetInput("Press any key to go back");

            return true;
        }
    }
}
