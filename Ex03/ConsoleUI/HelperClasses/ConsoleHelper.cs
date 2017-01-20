using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI.HelperClasses
{
    class ConsoleHelper
    {
        public static void PrintEnum(Type i_EnumType)
        {
            foreach (object value in Enum.GetValues(i_EnumType))
            {
                Console.WriteLine("{0} - {1}", (int)value, value);
            }
        }

        public static int ReadIntInput()
        {
            string inputStr;
            int input;
            bool isValidInput;
            do
            {
                inputStr = Console.ReadLine();
                isValidInput = int.TryParse(inputStr, out input);
                if (!isValidInput)
                {
                    Console.WriteLine("Invalid input");
                }
            }
            while (!isValidInput);

            return input;
        }
    }
}
