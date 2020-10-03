using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SodaMachine
{
    class SodaMachine
    {
        #region Fields

        private List<Soda> _inventory;
        private List<MachineFunction> _functionList;
        private int _totalMoneyIn;
        private int _currentCredit;
        private string _systemMessage;

        #endregion
        #region Properties
        private string SystemMessage
        {
            get { return _systemMessage; }
            set
            {
                _systemMessage += $"{value}\n";
            }
        }
        #endregion
        #region Constructor
        public SodaMachine()
        {
            //Machine functions get added and bound here
            _functionList = new List<MachineFunction>();
            MachineFunction dispenseFunction = new MachineFunction("order", "Order Drink", TryDispenseDrink);
            MachineFunction insertCoinFunction = new MachineFunction("insert", "Insert coins: (1), (5), (10), (20)", InsertCoin);
            MachineFunction refundFunction = new MachineFunction("r", "Return Credit", ReturnCredit);
            MachineFunction systemInformationFunction = new MachineFunction("s", "System Information", ShowSystemInformation);

            _functionList.Add(dispenseFunction);
            _functionList.Add(insertCoinFunction);
            _functionList.Add(refundFunction);
            _functionList.Add(systemInformationFunction);

            //Initial machine inventory
            _inventory = new List<Soda>();
            Soda coke = new Soda("Coke", 5, 25);
            Soda sprite = new Soda("Sprite", 0, 25);
            Soda fanta = new Soda("Fanta", 5, 25);
            Restock(coke, sprite, fanta);

            _currentCredit = 0;
            SystemMessage = "";
            _totalMoneyIn = 0;
        }
        #endregion
        #region Methods

        //Start SodaMachine
        public void Start()
        {
            ShowOptions();
        }

        //Display options to the user and handle response
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
                            (soda.StockCount > 0 ? soda.Price.ToString() + ".-" : "Out of stock"))
                    );
                }

                Console.WriteLine("\n");
                foreach (var function in _functionList)
                {
                    Console.WriteLine($"({function.CallToken}) - {function.Description}");
                }

                Console.WriteLine($"\nCredit: {_currentCredit}");

                Console.WriteLine($"{SystemMessage}");
                _systemMessage = String.Empty;
                Console.WriteLine("Command: ");
                var input = Console.ReadLine().ToLower().Split(' ');
                var commandList = _functionList.Where(f => f.CallToken.Equals(input[0]));

                if (commandList.Any())
                {
                    foreach (var command in commandList)
                    {
                        if (input.Length >= 2)
                            command.Function(input[input.Length - 1] ?? String.Empty);
                        else command.Function(String.Empty);
                    }
                }
                else SystemMessage = "Unrecognized command";
            }
        }

        //Add drink to machine
        private void AddDrink(Soda soda)
        {
            if (!_inventory.Where(s => s.Name.ToLower().Equals(soda.Id)).Any())
                _inventory.Add(soda);
        }

        //Stock machine
        private void Restock(params Soda[] sodaList)
        {
            foreach (var soda in sodaList)
            {
                AddDrink(soda);
            }
        }

        //Handle coin inserts
        private void InsertCoin(string option)
        {
            int coin = 0;
            if (!Int32.TryParse(option, out coin))
            {
                SystemMessage = "Coin not recognised";
                return;
            }

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
                    SystemMessage = "Coin not recognised";
                    break;
            }
        }

        //Display vending machine system information
        private void ShowSystemInformation(string option)
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
            Console.WriteLine($"\nTotal money in: {_totalMoneyIn}");
            Console.ReadLine();
        }

        //Return credits to user
        private void ReturnCredit(string option)
        {
            SystemMessage = $"{_currentCredit} refunded.";
            _currentCredit = 0;
        }

        //Attempt to dispense a drink
        private void TryDispenseDrink(string option)
        {
            if (option.Equals(String.Empty))
            {
                SystemMessage = "No drink specified!";
                return;
            }

            _inventory.Where(s => s.Id == option).ToList().ForEach((soda) => 
            {
                if (soda.StockCount > 0)
                {
                    if (_currentCredit >= soda.Price)
                    {
                        soda.StockCount--;
                        _totalMoneyIn += soda.Price;
                        _currentCredit -= soda.Price;
                        ReturnCredit(String.Empty);
                        SystemMessage = $"{soda.Name} dispensed!";
                        return;
                    }
                    SystemMessage = "Not enough credit";
                    return;
                }
                SystemMessage = "Out of stock!";
            });
        }

        #endregion
        #region Nested Class

        //Machine function class allows us to specify a function token, description and callback argument to execute
        private class MachineFunction
        {
            public string CallToken { get; set; }
            public string Description { get; set; }
            public Action<string> Function { get; set; }

            public MachineFunction(string callToken, string description, Action<string> function)
            {
                this.CallToken = callToken;
                this.Description = description;
                this.Function = function;
            }
        }

        #endregion
    }
}
