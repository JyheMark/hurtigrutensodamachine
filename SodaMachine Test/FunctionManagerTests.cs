using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using SodaMachine;

namespace SodaMachine_Test
{
    [TestClass]
    public class FunctionManagerTests
    {
        [TestMethod]
        public void CanCallFunction()
        {
            FunctionManager functionManager = new FunctionManager();

            Assert.IsTrue(functionManager.AddFunction("test", "this is a test", (args) => { return true; }),
                "FunctionManager failed to insert unique function");

            Assert.IsFalse(functionManager.AddFunction("test", "this is a test", (args) => { return true; }),
                "FunctionManager inserted function but duplicate callToken exists");

            Assert.IsTrue(functionManager.RunFunction("test", "test"), "FunctionManager failed to run callback function");
        }
    }
}
