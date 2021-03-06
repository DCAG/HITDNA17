﻿using System;
using System.Collections.Generic;
using System.Text;

namespace A17_Ex01_2
{
    public class Program
    {
        public static void Main()
        {
            HourglassLines(5);
            PrintProgramExitMessage();
        }

        private static void PrintProgramExitMessage()
        {
            Console.WriteLine("{0}Press any key to exit...", Environment.NewLine);
            Console.ReadLine();
        }

        public static void HourglassLines(int i_NumOfLines)
        {
            StringBuilder hourglass = new StringBuilder();

            for (int i = 1; i <= (i_NumOfLines / 2) + 1; i++)
            {
                hourglass.Append(' ', i - 1);
                hourglass.Append('*', i_NumOfLines - (2 * (i - 1)));
                hourglass.Append(Environment.NewLine);
            }

            for (int i = i_NumOfLines / 2; i >= 1; i--)
            {
                hourglass.Append(' ', i - 1);
                hourglass.Append('*', i_NumOfLines - (2 * (i - 1)));
                hourglass.Append(Environment.NewLine);
            }

            System.Console.WriteLine(hourglass);
        }
    }
}
