using System;

namespace A17_Ex01_4
{
    class Program
    {
        public static void Main(string[] args)
        {
            AnalyzeString();
            PrintProgramExitMessage();
        }

        private static void PrintProgramExitMessage()
        {
            Console.WriteLine("{0}Press any key to exit...", Environment.NewLine);
            Console.ReadLine();
        }

        private static void AnalyzeString()
        {
            string inputStr = ReadInputString();
            if(IsPalindrom(inputStr))
            {
                Console.WriteLine("Your input is a palindrom");
            }
            else
            {
                Console.WriteLine("Your input is NOT a palindrom");
            }

            if (IsAllDigits(inputStr))
            {
                Console.WriteLine("Your input is a series of numbers. Its Average is {0:0.00}", GetAverage(inputStr));
            }
            else if (IsAllLetters(inputStr))
            {
                Console.WriteLine("Your input is an english text string. The amount of capital letters is {0}", CountCapitalLetters(inputStr));
            }
        }

        private static string ReadInputString()
        {
            string inputStr;
            bool tryAgain = true;
            do
            {
                Console.WriteLine(@"
Enter a string consisting of 6 characters in length.
your input is to be consisted from english letters only (upper and lower case) or digits only.
");
                inputStr = Console.ReadLine();
                if (inputStr.Length != 6)
                {
                    Console.WriteLine("Illegal input: There are more than or less than 6 characters.");
                }
                else if (!IsAllDigits(inputStr) && !IsAllLetters(inputStr))
                {
                    Console.WriteLine("Illegal input: Your input does not contain only digits or only letters.");
                }
                else
                {
                    tryAgain = false;
                }
            }
            while (tryAgain);
            return inputStr;
        }

        private static bool IsAllLetters(string i_Str)
        {
            bool allLetters = true;
            foreach (char c in i_Str)
            {
                if (!char.IsLetter(c))
                {
                    allLetters = false;
                    break;
                }
            }

            return allLetters;
        }

        private static bool IsAllDigits(string i_Str)
        {
            bool allDigits = true;
            foreach (char c in i_Str)
            {
                if (!char.IsDigit(c))
                {
                    allDigits = false;
                    break;
                }
            }

            return allDigits;
        }

        private static int CountCapitalLetters(string i_Str)
        {
            int counter = 0;
            foreach (char c in i_Str)
            {
                if (char.IsUpper(c))
                {
                    counter++;
                }
            }

            return counter;
        }

        private static int GetAverage(string i_DigitsSequenceStr)
        {
            int sum = 0;
            for (int i = 0; i < i_DigitsSequenceStr.Length; i++)
            {
                sum += (int)char.GetNumericValue(i_DigitsSequenceStr[i]);
            }

            return sum / i_DigitsSequenceStr.Length;
        }

        private static bool IsPalindrom(string i_Str)
        {
            bool strIsPlaindrom = true;
            for (int i = 0; i < i_Str.Length / 2; i++)
            {
                if (i_Str[i] != i_Str[i_Str.Length - 1 - i])
                {
                    strIsPlaindrom = false;
                }
            }

            return strIsPlaindrom;
        }
    }
}
