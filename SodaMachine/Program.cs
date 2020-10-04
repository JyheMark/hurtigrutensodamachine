using System;
using SodaMachine;

namespace SodaMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            SodaMachine sodaMachine = new SodaMachine((message) => { Console.WriteLine(message); });

            while (true)
            {
                sodaMachine.ShowDisplay();

                var inputArgsArray = Console.ReadLine().ToLower().Split(' ');

                sodaMachine.ProcessUserInput(inputArgsArray);
            }
        }
    }
}
