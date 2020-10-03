using System;
using System.Collections.Generic;
using System.Text;

namespace SodaMachine
{
    class SodaMachine
    {
        private List<Soda> _inventory;
        private List<Soda> _defaultStock;
        private int _currentCredit;
        private string _systemMessage;

        public SodaMachine()
        {
            //Initial state of inventory
            Soda coke = new Soda("Coke", 5, 25);
            Soda sprite = new Soda("Sprite", 5, 25);
            Soda fanta = new Soda("Fanta", 5, 25);

            _defaultStock = new List<Soda>();
            _defaultStock.Add(coke);
            _defaultStock.Add(sprite);
            _defaultStock.Add(fanta);

            _inventory = new List<Soda>();
            Restock();
            _currentCredit = 0;
            _systemMessage = "";
        }

        public void Start()
        {
            ShowOptions();
        }

        private void ShowOptions()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("########Drink Dispenser 5000########\n");

                foreach (var soda in _inventory)
                {
                    Console.WriteLine(
                        String.Format("{0, -12} - {1}.-", soda.Name,
                            (soda.Price > 0 ? soda.Price.ToString() : "Out of stock"))
                    );
                }

                Console.WriteLine("\n(s) - System Diagnostics");
                Console.WriteLine("(r) - Refund");
                Console.WriteLine("\nInsert coins (1, 5, 10, 20");
                Console.WriteLine($"Credit: {_currentCredit}");

                Console.WriteLine($"\n{_systemMessage}");
                _systemMessage = String.Empty;
                Console.WriteLine("Command: ");
                string input = Console.ReadLine().ToLower();

                //Drink selected
                bool dispenseAttempt = false;
                foreach (var soda in _inventory)
                {
                    if (input.Equals(soda.Name.ToLower()))
                    {
                        TryDispenseDrink(soda);
                        dispenseAttempt = true;
                        break;
                    }
                }
                if (dispenseAttempt)
                    continue;

                int coinInserted = 0;

                if (Int32.TryParse(input, out coinInserted))
                {
                    //Coin inserted
                    InsertCoin(coinInserted);
                    continue;
                }

                //Command issued
                switch (input)
                {
                    case "s":
                        break;
                    case "r":
                        ReturnCredit();
                        break;
                    default:
                        _systemMessage = "Command not recognised";
                        continue;
                }
            }
        }

        //Reset machine to default state
        private void Restock()
        {
            foreach (var soda in _defaultStock)
            {
                _inventory.Add(soda);
            }
        }

        private void InsertCoin(int coin)
        {
            switch (coin)
            {
                case 1:
                    _currentCredit += 1;
                    break;
                case 5:
                    _currentCredit += 5;
                    break;
                case 10:
                    _currentCredit += 10;
                    break;
                case 20:
                    _currentCredit += 20;
                    break;
                default:
                    _systemMessage = "Coin not recognised";
                    break;
            }
        }

        public void ReturnCredit()
        {
            _systemMessage = $"{_currentCredit} refunded.";
            _currentCredit = 0;
        }

        public void TryDispenseDrink(Soda soda)
        {
            if (soda.StockCount > 0)
            {
                if (_currentCredit >= soda.Price)
                {
                    soda.StockCount--;
                    _currentCredit -= soda.Price;
                    ReturnCredit();
                    return;
                }
                _systemMessage = "Not enough credit";
                return;
            }
            _systemMessage = "Out of stock!";
        }
    }
}
