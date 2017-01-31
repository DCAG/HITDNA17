using System;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    internal class ShowDate : IMenuItemAction
    {
        public void Invoke()
        {
            showDate();
        }

        private void showDate()
        {
            Console.WriteLine(DateTime.Now.ToShortDateString());
            Console.Write("Press any key to continue...");
            Console.ReadLine();
        }
    }
}