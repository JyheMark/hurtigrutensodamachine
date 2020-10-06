using System;
using System.Collections.Generic;
using System.Text;

namespace SodaMachine
{
    class OptionDisplayBuilder
    {
        public void BuildOptions(FunctionManager functionManager, Inventory inventory, MoneyHandler moneyHandler)
        {
            string output = String.Empty;
            UserInteraction userInteraction = UserInteraction.Instance;

            output += "########Drink Dispenser 5000########\n\n";

            foreach (var inventoryItem in inventory.InventoryList)
            {
                output += String.Format("{0, -12} - {1}", inventoryItem.Soda.Name,
                        (inventoryItem.Stock > 0 ? inventoryItem.Soda.Price.ToString() + ".-\n" : "Out of stock\n"));
            }

            output += "\n";

            foreach (var function in functionManager.MachineFunctionList)
            {
                output += ($"({function.CallToken}) - {function.Description}\n");
            }
            output += "\n";
            output += $"Credit: {moneyHandler.CurrentUserCredit}";

            userInteraction.AppendMessageToOutput(output);
        }
    }
}
