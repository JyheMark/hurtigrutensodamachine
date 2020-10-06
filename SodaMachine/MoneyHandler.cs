using System;
using System.Collections.Generic;
using System.Text;

namespace SodaMachine
{
    class MoneyHandler
    {
        public int TotalMoneyIn { get; private set; }
        public int CurrentUserCredit { get; private set; }
        public int TotalTransactions { get; private set; }

        public MoneyHandler()
        {
            CurrentUserCredit = 0;
            TotalMoneyIn = 0;
            TotalTransactions = 0;
        }

        public bool InsertCoin(string coin)
        {
            int coinValue = 0;
            if (!Int32.TryParse(coin, out coinValue))
            {
                UserInteraction.Instance.AppendSystemMessage("Coin not recognised");
                return false;
            }

            switch (coinValue)
            {
                case 1:
                    CurrentUserCredit += 1;
                    return true;
                case 5:
                    CurrentUserCredit += 5;
                    return true;
                case 10:
                    CurrentUserCredit += 10;
                    return true;
                case 20:
                    CurrentUserCredit += 20;
                    return true;
                default:
                    UserInteraction.Instance.AppendSystemMessage("Coin not recognised");
                    return false;
            }
        }

        public bool CheckEnoughCredit(int intent)
        {
            return (CurrentUserCredit >= intent);
        }

        public void makePurchase(int amount)
        {
            TotalMoneyIn += amount;
            CurrentUserCredit -= amount;
            TotalTransactions++;
            ReturnCredit("");
        }

        public bool ReturnCredit(string args)
        {
            UserInteraction.Instance.AppendSystemMessage($"{CurrentUserCredit} returned");
            CurrentUserCredit = 0;
            return false;
        }
    }
}
