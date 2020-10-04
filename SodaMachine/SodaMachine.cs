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
        private List<MachineFunction> _machineFunctionList;
        private int _totalMoneyIn;
        private int _currentUserCredit;
        private string _systemMessage;

        #endregion
        #region Properties
        private string SystemMessage
        {
            get { return _systemMessage; }
            set
            {
                _systemMessage += $"{value}";

                if (!_systemMessage.Equals(String.Empty))
                    _systemMessage += "\n";
            }
        }
        #endregion
        #region Constructor
        public SodaMachine()
        {
            _currentUserCredit = 0;
            SystemMessage = String.Empty;
            _totalMoneyIn = 0;
            _machineFunctionList = new List<MachineFunction>();
            _inventory = new List<Soda>();

            //Machine functions get added and bound here
            MachineFunction dispenseFunction = new MachineFunction("order", "Order Drink", TryDispenseDrink);
            MachineFunction insertCoinFunction = new MachineFunction("insert", "Insert coins: (1), (5), (10), (20)", InsertCoin);
            MachineFunction refundFunction = new MachineFunction("r", "Return Credit", ReturnCredit);
            MachineFunction systemInformationFunction = new MachineFunction("s", "System Information", ShowSystemInformation);

            _machineFunctionList.Add(dispenseFunction);
            _machineFunctionList.Add(insertCoinFunction);
            _machineFunctionList.Add(refundFunction);
            _machineFunctionList.Add(systemInformationFunction);

            //Initial machine inventory
            Soda coke = new Soda("Coke", 5, 25);
            Soda sprite = new Soda("Sprite", 0, 25);
            Soda fanta = new Soda("Fanta", 5, 25);
            Restock(coke, sprite, fanta);
        }
        #endregion
        #region Methods

        public void Start()
        {
            while (true)
            {
                ShowOptions();

                Console.WriteLine($"\nCredit: {_currentUserCredit}");
                Console.WriteLine(
                    SystemMessage.Equals(String.Empty) ? "" : $"\n{SystemMessage}"
                    );
                _systemMessage = String.Empty;

                GetUserCommand();
            }
        }

        private void ShowOptions()
        {
            Console.Clear();
            Console.WriteLine("########Drink Dispenser 5000########\n");

            foreach (var soda in _inventory)
            {
                Console.WriteLine(
                    String.Format("{0, -12} - {1}", soda.Name,
                        (soda.StockCount > 0 ? soda.Price.ToString() + ".-" : "Out of stock"))
                );
            }

            Console.WriteLine("\n");
            foreach (var function in _machineFunctionList)
            {
                Console.WriteLine($"({function.CallToken}) - {function.Description}");
            }
        }

        private void GetUserCommand()
        {
            Console.WriteLine("Command: ");

            var inputArgsArray = Console.ReadLine().ToLower().Split(' ');
            var matchingMachineFunctionsList = _machineFunctionList.Where(f => f.CallToken.Equals(inputArgsArray[0]));

            if (matchingMachineFunctionsList.Any())
            {
                foreach (var function in matchingMachineFunctionsList)
                {
                    //If additional arguments are provided, pass them to function else pass empty
                    if (inputArgsArray.Length >= 2)
                        function.Function(inputArgsArray[inputArgsArray.Length - 1] ?? String.Empty);
                    else function.Function(String.Empty);
                }
            }
            else SystemMessage = "Unrecognized command";
        }

        private void AddDrink(Soda soda)
        {
            if (!_inventory.Where(s => s.Name.ToLower().Equals(soda.Id)).Any())
                _inventory.Add(soda);
        }

        private void Restock(params Soda[] sodaList)
        {
            foreach (var soda in sodaList)
            {
                AddDrink(soda);
            }
        }

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
                    _currentUserCredit += 1;
                    break;
                case 5:
                    _currentUserCredit += 5;
                    break;
                case 10:
                    _currentUserCredit += 10;
                    break;
                case 20:
                    _currentUserCredit += 20;
                    break;
                default:
                    SystemMessage = "Coin not recognised";
                    break;
            }
        }

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

        private void ReturnCredit(string option)
        {
            SystemMessage = $"{_currentUserCredit} refunded.";
            _currentUserCredit = 0;
        }

        private void TryDispenseDrink(string option)
        {
            if (option.Equals(String.Empty))
            {
                SystemMessage = "No drink specified!";
                return;
            }

            var matchingSodas = _inventory.Where(s => s.Id.Equals(option));

            if (!matchingSodas.Any())
            {
                SystemMessage = "Drink doesn't exist";
                return;
            }

            foreach (var soda in matchingSodas)
            {
                if (soda.StockCount > 0)
                {
                    if (_currentUserCredit >= soda.Price)
                    {
                        _currentUserCredit -= soda.Price;
                        _totalMoneyIn += soda.Price;
                        soda.StockCount--;

                        SystemMessage = $"{soda.Name} dispensed!";
                        ReturnCredit(String.Empty);
                        return;
                    }
                    SystemMessage = "Not enough credit";
                    return;
                }
                SystemMessage = "Out of stock!";
            }
        }

        #endregion
        #region Nested Classes

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
