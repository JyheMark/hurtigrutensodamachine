using Microsoft.VisualStudio.TestTools.UnitTesting;
using SodaMachine;

namespace SodaMachine_Test
{
    [TestClass]
    public class MoneyHandlerTests
    {
        [TestMethod]
        public void CreditChecking()
        {
            MoneyHandler moneyHandler = new MoneyHandler();
            moneyHandler.InsertCoin("20");

            bool result = moneyHandler.MakePurchase(100);

            Assert.IsFalse(result, "MoneyHandler approved purchase with insufficient credit");

            result = moneyHandler.MakePurchase(10);

            Assert.IsTrue(result, "MoneyHandler rejected purchase with sufficient credit");

        }

        [TestMethod]
        public void CoinRecognition()
        {
            MoneyHandler moneyHandler = new MoneyHandler();

            Assert.IsFalse(moneyHandler.InsertCoin("100"), "MoneyHandler approved strange coin");
            Assert.IsFalse(moneyHandler.InsertCoin("notacoin"), "MoneyHandler approved strange coin");
        }
    }
}
