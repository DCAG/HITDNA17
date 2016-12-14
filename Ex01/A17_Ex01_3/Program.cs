using System;
using System.Collections.Generic;
using System.Text;
using A17_Ex01_2;

namespace A17_Ex01_3
{
    class Program
    {
        public static void Main()
        {
            HighOfHourglass();
        }

        public static void HighOfHourglass()
        {
            int highHourglass = 0;
            bool goodInput = false;

            while ((!goodInput) || (highHourglass <= 0))
            {
                System.Console.WriteLine("Please enter the high of hourglass (high>0)");
                System.Console.WriteLine();
                string highHourglassStr = System.Console.ReadLine();
                System.Console.WriteLine();
                goodInput = int.TryParse(highHourglassStr, out highHourglass);
            }

            if (highHourglass % 2 == 0)
            {
                highHourglass--;
            }

            
            A17_Ex01_2.Program.HourglassLines(highHourglass);

        }

    }
}
