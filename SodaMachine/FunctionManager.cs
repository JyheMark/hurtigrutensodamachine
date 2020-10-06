using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SodaMachine
{
    class FunctionManager
    {
        private List<MachineFunction> _machineFunctionList;

        public FunctionManager()
        {
            _machineFunctionList = new List<MachineFunction>();
        }

        public bool AddFunction(string callToken, string description, Action<string> function)
        {
            var matchingCallTokens = _machineFunctionList.Where(f => f.CallToken.Equals(callToken));
            if (matchingCallTokens.Any())
                return false;

            MachineFunction newFunction = new MachineFunction(callToken, description, function);
            _machineFunctionList.Add(newFunction);

            return true;
        }

        private class MachineFunction
        {
            public string CallToken { get; private set; }
            public string Description { get; private set; }
            public Action<string> Function { get; private set; }

            public MachineFunction(string callToken, string description, Action<string> function)
            {
                this.CallToken = callToken;
                this.Description = description;
                this.Function = function;
            }
        }
    }
}
