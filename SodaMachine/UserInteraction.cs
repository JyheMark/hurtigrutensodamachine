using System;
using System.Collections.Generic;
using System.Text;

namespace SodaMachine
{
    class UserInteraction
    {
        private static UserInteraction _userInteraction = null;
        private string _message;
        private string _systemMessage;

        public static UserInteraction Instance
        {
            get
            {
                if (_userInteraction == null)
                    _userInteraction = new UserInteraction();

                return _userInteraction;
            }
            set
            {
                _userInteraction = value;
            }
        }

        private UserInteraction()
        {
            _message = String.Empty;
            _systemMessage = String.Empty;
        }

        public void AppendMessageToOutput(string message)
        {
            _message += $"{message}";
        }

        public void AppendSystemMessage(string message)
        {
            _systemMessage += $"{message}\n";
        }

        public string[] GetInput(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine().ToLower().Split(' ');
        }

        public void SendOutput()
        {
            Console.Clear();
            Console.WriteLine(_message);

            if (_systemMessage != String.Empty)
            {
                Console.WriteLine(_systemMessage);
            }
            else
                Console.WriteLine("\n");

            _message = String.Empty;
            _systemMessage = String.Empty;
        }
    }
}
