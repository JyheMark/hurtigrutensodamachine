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
        public void CheckingStock()
        {
            Inventory inventory = new Inventory((args) => { return true; });
            inventory.AddSoda(new Soda("Coke", 25), 5);
            
            Assert.IsTrue(inventory.AddSoda(new Soda("Fanta", 25), 0), "Inventory failed to insert unique soda");
            Assert.IsFalse(inventory.AddSoda(new Soda("Coke", 25), 5), "Inventory to inserted soda with duplicate code");
            Assert.IsTrue(inventory.TryDispenseSoda("coke"), "Couldn't dispense soda but it is in stock");
            Assert.IsFalse(inventory.TryDispenseSoda("fanta"), "Dispensed soda but it is out of stock");
            Assert.IsFalse(inventory.TryDispenseSoda("notadrink"), "Dispensed soda but soda doesn't exist");
        }
    }
}
