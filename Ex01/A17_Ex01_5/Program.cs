using System;
using System.Collections.Generic;
using System.Text;

namespace A17_Ex01_5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NumbersStatictics();
            printProgramExitMessage();
        }

        private static void printProgramExitMessage()
        {
            Console.WriteLine("{0}Press any key to exit...", Environment.NewLine);
            Console.ReadLine();
        }

        private static void NumbersStatictics()
        {
            string userInputNumber = readInputNumber();

            Console.WriteLine("The biggest digit: {0}", getBiggestDigit(userInputNumber));
            Console.WriteLine("The smallest digit: {0}", getSmallestDigit(userInputNumber));
            Console.WriteLine("The count of digits bigger than the ones place digit: {0}", countBiggerThanOnesPlaceDigit(userInputNumber));
            Console.WriteLine("The count of digits smaller than the ones place digit: {0}", countSmallerThanOnesPlaceDigit(userInputNumber));
        }

        private static string readInputNumber()
        {
            string number;
            bool tryAgain = true;

            do
            {
                Console.WriteLine("Please enter a 7-digit (positive) number:");
                number = Console.ReadLine();
                int tempNumber; // discarded after use in int.TryParse()
                if (number.Length != 7)
                {
                    Console.WriteLine("Invalid input! More than or less than 7 characters were entered.{0}", Environment.NewLine);
                }
                else if (!int.TryParse(number, out tempNumber) || char.GetNumericValue(number[0]) == 0)
                {
                    Console.WriteLine("Invalid input! Invalid number was entered.{0}", Environment.NewLine);
                }
                else
                {
                    tryAgain = false;
                }
            }
            while (tryAgain);

            return number;
        }

        private static int getBiggestDigit(string i_Number)
        {
            int biggestDigit = (int)char.GetNumericValue(i_Number[0]);

            for (int i = 1; i < i_Number.Length; i++)
            {
                int digitNumericValue = (int)char.GetNumericValue(i_Number[i]);

                if (digitNumericValue > biggestDigit)
                {
                    biggestDigit = digitNumericValue;
                }
            }

            return biggestDigit;
        }

        private static int getSmallestDigit(string i_Number)
        {
            int smallestDigit = (int)char.GetNumericValue(i_Number[0]);

            for (int i = 1; i < i_Number.Length; i++)
            {
                int digitNumericValue = (int)char.GetNumericValue(i_Number[i]);

                if (digitNumericValue < smallestDigit)
                {
                    smallestDigit = digitNumericValue;
                }
            }

            return smallestDigit;
        }

        private static int countBiggerThanOnesPlaceDigit(string i_Number)
        {
            int onesPlaceDigit = (int)char.GetNumericValue(i_Number[i_Number.Length - 1]);
            int counter = 0;

            for (int i = 0; i < i_Number.Length - 1; i++)
            {
                if ((int)char.GetNumericValue(i_Number[i]) > onesPlaceDigit)
                {
                    counter++;
                }
            }

            return counter;
        }

        private static int countSmallerThanOnesPlaceDigit(string i_Number)
        {
            int onesPlaceDigit = (int)char.GetNumericValue(i_Number[i_Number.Length - 1]);
            int counter = 0;

            for (int i = 0; i < i_Number.Length - 1; i++)
            {
                if ((int)char.GetNumericValue(i_Number[i]) < onesPlaceDigit)
                {
                    counter++;
                }
            }

            return counter;
        }
    }
}
