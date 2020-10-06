using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SodaMachine
{
    public class FunctionManager
    {
        public List<MachineFunction> MachineFunctionList { get; private set; }

        public FunctionManager()
        {
            MachineFunctionList = new List<MachineFunction>();
        }

        public bool AddFunction(string callToken, string description, Func<string, bool> function)
        {
            callToken = callToken.ToLower();
            var matchingFunctions = MachineFunctionList.Where(f => f.CallToken.Equals(callToken));
            if (matchingFunctions.Any())
                return false;

            MachineFunction newFunction = new MachineFunction(callToken, description, function);
            MachineFunctionList.Add(newFunction);

            return true;
        }

        public bool RunFunction(string callToken, string args)
        {
            callToken = callToken.ToLower();
            var matchingFunctions = MachineFunctionList.Where(f => f.CallToken.Equals(callToken));
            if (!matchingFunctions.Any())
            {
                UserInteraction.Instance.AppendSystemMessage("Unknown Command");
                return false;
            }

            foreach (var function in matchingFunctions)
            {
                function.Function(args);
            }

            return true;
        }

        public class MachineFunction
        {
            public string CallToken { get; private set; }
            public string Description { get; private set; }
            public Func<string, bool> Function { get; private set; }

            public MachineFunction(string callToken, string description, Func<string, bool> function)
            {
                this.CallToken = callToken;
                this.Description = description;
                this.Function = function;
            }
        }
    }
}
