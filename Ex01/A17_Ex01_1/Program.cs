using System;
using System.Text;

namespace A17_Ex01_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            binarySequences();
            printProgramExitMessage();
        }

        private static void printProgramExitMessage()
        {
            Console.WriteLine("{0}Press any key to exit...", Environment.NewLine);
            Console.ReadLine();
        }

        private static void binarySequences()
        {
            string[] numbersAsStrings = new string[3];
            int[] numbersAsIntegers = new int[3];
            string[] numbersAsBinary = new string[3];

            for (byte i = 0; i < 3; i++)
            {
                numbersAsStrings[i] = readInputNumber();
                numbersAsIntegers[i] = int.Parse(numbersAsStrings[i]);
                numbersAsBinary[i] = convertStrToBinary(numbersAsIntegers[i]);
            }

            Console.WriteLine();
            Console.WriteLine("The binary numbers are:{0}{1}", Environment.NewLine, string.Join(Environment.NewLine, numbersAsBinary));
            printStatistics(numbersAsStrings, numbersAsIntegers, numbersAsBinary);
        }

        private static string readInputNumber()
        {
            string number;
            bool tryAgain = true;

            do
            {
                Console.WriteLine("Please enter a 3-digit (positive) number:");
                number = Console.ReadLine();
                int tempNumber; // discarded after use in int.TryParse()

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

        private static string convertStrToBinary(int i_DecimalNumber)
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

        private static void printStatistics(string[] i_NumbersAsStrings, int[] i_NumbersAsIntegers, string[] i_NumbersAsBinary)
        {
            Console.WriteLine("{0}Statistics on given input:{0}", Environment.NewLine);
            Console.WriteLine("Average amount of binary digits: {0:0.00}", avgAmountOfDigits(i_NumbersAsBinary));
            Console.WriteLine("Number of Sequences monotinically increasing: {0}", countMonotonicSequences(i_NumbersAsStrings, eSequenceBehaviors.Increasing));
            Console.WriteLine("Number of Sequences monotinically decreasing: {0}", countMonotonicSequences(i_NumbersAsStrings, eSequenceBehaviors.Decreasing));
            Console.WriteLine("Average value: {0:0.00}", calculateAverage(i_NumbersAsIntegers));
        }

        private static float avgAmountOfDigits(string[] i_NumbersAsBinary)
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
        private enum eSequenceBehaviors
        {
            NonMonotonic = 0,
            Increasing = 1,
            Decreasing = 2
        }

        private static byte countMonotonicSequences(string[] i_NumbersSequences, eSequenceBehaviors i_MonotonicBehavior)
        {
            byte counter = 0;

            foreach (string sequence in i_NumbersSequences)
            {
                if ((getSequenceBehavior(sequence) & i_MonotonicBehavior) == i_MonotonicBehavior)
                {
                    counter++;
                }
            }

            return counter;
        }

        private static eSequenceBehaviors getSequenceBehavior(string i_Sequence)
        {
            eSequenceBehaviors Behavior = eSequenceBehaviors.NonMonotonic;

            if (char.GetNumericValue(i_Sequence[0]) <= char.GetNumericValue(i_Sequence[1]) &&
                char.GetNumericValue(i_Sequence[1]) <= char.GetNumericValue(i_Sequence[2]))
            {
                Behavior = eSequenceBehaviors.Increasing;
            }

            if (char.GetNumericValue(i_Sequence[0]) >= char.GetNumericValue(i_Sequence[1]) &&
                char.GetNumericValue(i_Sequence[1]) >= char.GetNumericValue(i_Sequence[2]))
            {
                Behavior |= eSequenceBehaviors.Decreasing;
            }

            return Behavior;
        }
        #endregion

        private static float calculateAverage(int[] i_NumbersAsIntegers)
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