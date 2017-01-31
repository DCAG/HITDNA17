using Ex04.Menus.Interfaces;
using System;

namespace Ex04.Menus.Test
{
    internal class ShowTime : IMenuItemAction
    {
        public void Invoke()
        {
            showTime();
        }

        private void showTime()
        {
            Console.WriteLine(DateTime.Now.ToShortTimeString());
            Console.Write("Press any key to continue...");
            Console.ReadLine();
        }
    }
}