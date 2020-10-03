using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SodaMachine
{
    class SodaMachine
    {
        private List<Soda> _inventory;
        private int _moneyIn;
        private int _currentCredit;
        private string _systemMessage;

        public SodaMachine()
        {
            //Initial machine inventory
            Soda coke = new Soda("Coke", 5, 25);
            Soda sprite = new Soda("Sprite", 0, 25);
            Soda fanta = new Soda("Fanta", 5, 25);

            _inventory = new List<Soda>();
            Restock(coke, sprite, fanta);

            _currentCredit = 0;
            _systemMessage = "";
            _moneyIn = 0;
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

                //Display current stock
                foreach (var soda in _inventory)
                {
                    Console.WriteLine(
                        String.Format("{0, -12} - {1}", soda.Name,
                            (soda.StockCount > 0 ? soda.Price.ToString()+".-" : "Out of stock"))
                    );
                }

                Console.WriteLine("\n(order) - Order Drink");
                Console.WriteLine("(s) - System Diagnostics");
                Console.WriteLine("(r) - Refund");

                Console.WriteLine("\nInsert coins: (1), (5), (10), (20)");
                Console.WriteLine($"Credit: {_currentCredit}");

                Console.WriteLine($"\n{_systemMessage}");
                _systemMessage = String.Empty;
                Console.WriteLine("Command: ");
                string input = Console.ReadLine().ToLower();

                //Drink selected
                if (input.StartsWith("order"))
                {
                    string drink;
                    try
                    {
                        drink = input.Split(' ')[1];
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        _systemMessage = "No drink specified";
                        continue;
                    }
                    catch (Exception e)
                    {
                        _systemMessage = "Command invalid";
                        continue;
                    }

                    //Check to see drink exists
                    if (!_inventory.Where(s => s.Name.ToLower().Equals(drink)).Any())
                        _systemMessage = "Drink doesn't exist";
                    else
                        foreach (var soda in _inventory.Where(s => s.Name.ToLower().Equals(drink)))
                        {
                            TryDispenseDrink(soda);
                            break;
                        }

                    continue;
                }

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
                        ShowSystemInformation();
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

        //Add drink to machine
        private void AddDrink(Soda soda)
        {
            if (!_inventory.Where(s => s.Name.ToLower().Equals(soda.Id)).Any())
                _inventory.Add(soda);
        }

        //Reset machine to default state
        private void Restock(params Soda[] sodaList)
        {
            foreach (var soda in sodaList)
            {
                AddDrink(soda);
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

        private void ShowSystemInformation()
        {
            Console.Clear();
            Console.WriteLine("########System Information########");
            Console.WriteLine("\nCurrent stock:\n");
            foreach (var soda in _inventory)
            {
                Console.WriteLine(
                    String.Format("{0, -12} - {1}", soda.Name, soda.StockCount)
                );
            }
            Console.WriteLine($"\nTotal money in: {_moneyIn}");
            Console.ReadLine();
        }

        private void ReturnCredit()
        {
            _systemMessage = $"{_currentCredit} refunded.";
            _currentCredit = 0;
        }

        private void TryDispenseDrink(Soda soda)
        {
            if (soda.StockCount > 0)
            {
                if (_currentCredit >= soda.Price)
                {
                    soda.StockCount--;
                    _moneyIn += soda.Price;
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
