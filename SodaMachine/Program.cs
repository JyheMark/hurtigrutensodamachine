using System;
using SodaMachine;

namespace SodaMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInteraction userInteraction = UserInteraction.Instance;
            MoneyHandler moneyHandler = new MoneyHandler();
            Inventory inventory = new Inventory(moneyHandler.CheckEnoughCredit, moneyHandler.makePurchase);
            SystemInformation systemInformation = new SystemInformation(inventory, moneyHandler);
            FunctionManager functionManager = new FunctionManager();
            OptionDisplayBuilder optionDisplayBuilder = new OptionDisplayBuilder();

            functionManager.AddFunction("order", "Order Drink", inventory.TryDispenseSoda);
            functionManager.AddFunction("insert", "Insert Coins: (1), (5), (10), (20)", moneyHandler.InsertCoin);
            functionManager.AddFunction("refund", "Return Credit", moneyHandler.ReturnCredit);
            functionManager.AddFunction("information", "System Information", systemInformation.ShowSystemInformation);

            inventory.AddSoda(new Soda("Coke", 25), 5);
            inventory.AddSoda(new Soda("Sprite", 25), 0);
            inventory.AddSoda(new Soda("Fanta", 25), 5);

            while (true)
            {
                optionDisplayBuilder.BuildOptions(functionManager, inventory, moneyHandler);
                userInteraction.SendOutput();
                var input = userInteraction.GetInput("Input: ");
                functionManager.RunFunction(input[0], input[input.Length-1]);
            }
        }
    }
}
