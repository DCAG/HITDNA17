using System;
using System.Text;

namespace A17_Ex01_1
{
    class Program
    {
        public static void Main(string[] args)
        {
            BinarySequences();
            PrintProgramExitMessage();
        }

        private static void PrintProgramExitMessage()
        {
            Console.WriteLine("{0}Press any key to exit...", Environment.NewLine);
            Console.ReadLine();
        }

        private static void BinarySequences()
        {
            string[] numbersAsStrings  = new string[3];
            int[] numbersAsIntegers = new int[3];
            string[] numbersAsBinary   = new string[3];

            for (byte i = 0; i < 3; i++)
            {
                numbersAsStrings[i]  = ReadInputNumber();
                numbersAsIntegers[i] = int.Parse(numbersAsStrings[i]);
                numbersAsBinary[i]   = ConvertStrToBinary(numbersAsIntegers[i]);
            }
            
            Console.WriteLine();
            Console.WriteLine(string.Join(Environment.NewLine, numbersAsBinary));
            PrintStatistics(numbersAsStrings, numbersAsIntegers, numbersAsBinary);
        }

        private static string ReadInputNumber()
        {
            string number;
            bool tryAgain = true;
            do
            {
                Console.WriteLine("Please enter a 3-digit (positive) number:");
                number = Console.ReadLine();
                int tempNumber; //discarded after use in int.TryParse()
                if (number.Length != 3)
                {
                    Console.WriteLine("Invalid input! More than or less than 3 characters were entered.");
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

        private static string ConvertStrToBinary(int i_DecimalNumber)
        {
            StringBuilder str = new StringBuilder(string.Empty);
            while (i_DecimalNumber > 0)
            {
                str.Insert(0, i_DecimalNumber % 2);
                i_DecimalNumber = i_DecimalNumber >> 1;
            }

            if (str.Length == 0)
            {
                str.Append("0");
            }
            return str.ToString();
        }

        private static void PrintStatistics(string[] i_NumbersAsStrings, int[] i_NumbersAsIntegers, string[] i_NumbersAsBinary)
        {
            Console.WriteLine("{0}Statistics on given input:{0}", Environment.NewLine);
            Console.WriteLine("Average amount of binary digits: {0:0.00}", AvgAmountOfDigits(i_NumbersAsBinary));
            Console.WriteLine("Number of Sequences monotinically increasing: {0}", CountMonotonicSequences(i_NumbersAsStrings, SequenceBehavior.Increasing));
            Console.WriteLine("Number of Sequences monotinically decreasing: {0}", CountMonotonicSequences(i_NumbersAsStrings, SequenceBehavior.Decreasing));
            Console.WriteLine("Average value: {0:0.00}", CalculateAverage(i_NumbersAsIntegers));
        }

        private static float AvgAmountOfDigits(string[] i_NumbersAsBinary)
        {
            float totalDigitsCount = 0;
            foreach (string binaryNumber in i_NumbersAsBinary)
            {
                totalDigitsCount += binaryNumber.Length;
            }
            return totalDigitsCount / i_NumbersAsBinary.Length;
        }

        #region Monotonic Sequences
        [Flags]
        private enum SequenceBehavior {
            NonMonotonic = 0,
            Increasing   = 1,
            Decreasing   = 2
        }

        private static byte CountMonotonicSequences(string[] i_NumbersSequences, SequenceBehavior i_MonotonicBehavior)
        {
            byte counter = 0;
            foreach (string sequence in i_NumbersSequences)
            {
                if ((GetSequenceBehavior(sequence) & i_MonotonicBehavior) == i_MonotonicBehavior)
                {
                    counter++;
                }
            }
            return counter;
        }

        private static SequenceBehavior GetSequenceBehavior(string i_Sequence)
        {
            SequenceBehavior Behavior = SequenceBehavior.NonMonotonic;
            if (char.GetNumericValue(i_Sequence[0]) <= char.GetNumericValue(i_Sequence[1]) &&
                char.GetNumericValue(i_Sequence[1]) <= char.GetNumericValue(i_Sequence[2]))
            {
                Behavior = SequenceBehavior.Increasing;
            }
            if (char.GetNumericValue(i_Sequence[0]) >= char.GetNumericValue(i_Sequence[1]) &&
                char.GetNumericValue(i_Sequence[1]) >= char.GetNumericValue(i_Sequence[2]))
            {
                Behavior |= SequenceBehavior.Decreasing;
            }
            return Behavior;
        }
        #endregion

        private static float CalculateAverage(int[] i_NumbersAsIntegers)
        {
            float sum = 0;
            foreach (int number in i_NumbersAsIntegers)
            {
                sum += number;
            }

            return sum / i_NumbersAsIntegers.Length;
        }
    }
}
