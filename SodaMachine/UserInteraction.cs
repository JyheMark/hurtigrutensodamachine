using System;
using System.Collections.Generic;
using System.Text;

namespace SodaMachine
{
    class UserInteraction
    {
        private static UserInteraction _userInsteraction = null;
        private string _message;

        public static UserInteraction GetUserInteraction()
        {
            if (_userInsteraction == null)
                _userInsteraction = new UserInteraction();

            return _userInsteraction;
        }

        public void AppendMessageToOutput(string message)
        {
            _message += $"\n{message}\n";
        }

        public string[] GetInput()
        {
            return Console.ReadLine().Split(' ');
        }

        public void SendOutput()
        {
            Console.WriteLine(_message);
            _message = "";
        }
    }
}
