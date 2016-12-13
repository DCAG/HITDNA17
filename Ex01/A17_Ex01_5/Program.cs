using System;
using System.Collections.Generic;
using System.Text;

namespace A17_Ex01_5
{
    class Program
    {
        public static void Main(string[] args)
        {
            NumbersStatictics();
            PrintProgramExitMessage();
        }

        private static void PrintProgramExitMessage()
        {
            Console.WriteLine("{0}Press any key to exit...", Environment.NewLine);
            Console.ReadLine();
        }

        private static void NumbersStatictics()
        {
            int userInputNumber = ReadInputNumber();
            GetBiggestDigit(userInputNumber);
            GetSmallest(userInputNumber);
            CountBiggerThanOncePlaceDigit();
        }

        private static int ReadInputNumber()
        {
            string number;
            bool tryAgain = true;
            do
            {
                Console.WriteLine("Please enter a 7-digit (positive) number:");
                number = Console.ReadLine();
                int tempNumber; //discarded after use in int.TryParse()
                if (number.Length != 7)
                {
                    Console.WriteLine("Invalid input! More than or less than 7 characters were entered.");
                }
                else if (!int.TryParse(number, out tempNumber))
                {
                    Console.WriteLine("Invalid input! Invalid number was entered.");
                }
                else
                {
                    tryAgain = false;
                }
            }
            while (tryAgain);
            return number;
        }

        private static void PrintStatistics(string[] i_NumbersAsStrings, int[] i_NumbersAsIntegers, string[] i_NumbersAsBinary)
        {
            Console.WriteLine("{0}Statistics on given input:{0}", Environment.NewLine);
            Console.WriteLine("Average amount of binary digits: {0:0.00}", AvgAmountOfDigits(i_NumbersAsBinary));
            Console.WriteLine("Number of Sequences monotinically increasing: {0}", CountMonotonicSequences(i_NumbersAsStrings, SequenceBehavior.Increasing));
            Console.WriteLine("Number of Sequences monotinically decreasing: {0}", CountMonotonicSequences(i_NumbersAsStrings, SequenceBehavior.Decreasing));
            Console.WriteLine("Average value: {0:0.00}", CalculateAverage(i_NumbersAsIntegers));
        }
    }
}
