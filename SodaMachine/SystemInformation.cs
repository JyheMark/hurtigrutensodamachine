using System;
using System.Collections.Generic;
using System.Text;

namespace SodaMachine
{
    class SystemInformation
    {
        private static SystemInformation _systemInformation = null;
        private int _totalMoneyIn;

        public static SystemInformation Instance
        {
            get
            {
                if (_systemInformation == null)
                {
                    _systemInformation = new SystemInformation();
                }

                return _systemInformation;
            }

            private set
            {
                _systemInformation = value;
            }
        }

        private SystemInformation()
        {
            _totalMoneyIn = 0;
        }

        public bool ShowSystemInformation(string args)
        {
            return false;
        }
    }
}
