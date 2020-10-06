using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SodaMachine;

namespace SodaMachine_Test
{
    [TestClass]
    public class InventoryTests
    {
        [TestMethod]
        public void CheckingStockLevels()
        {
            Inventory inventory = new Inventory((args) => { return true; });
            inventory.AddSoda(new Soda("Coke", 25), 5);
            inventory.AddSoda(new Soda("Coke", 25), 0);

            Assert.IsTrue(inventory.TryDispenseSoda("coke"), "Couldn't dispense soda but it is in stock");
            Assert.IsFalse(inventory.TryDispenseSoda("fanta"), "Dispensed soda but it is out of stock");
            Assert.IsFalse(inventory.TryDispenseSoda("notadrink"), "Dispensed soda but soda doesn't exist");
        }

        [TestMethod]
        public void AddingSoda()
        {
            Inventory inventory = new Inventory((args) => { return true; });

            Assert.IsTrue(inventory.AddSoda(new Soda("Fanta", 25), 0), "Inventory failed to insert unique soda");
            Assert.IsFalse(inventory.AddSoda(new Soda("Fanta", 25), 0), "Inventory inserted soda with duplicate code");
        }
    }
}
