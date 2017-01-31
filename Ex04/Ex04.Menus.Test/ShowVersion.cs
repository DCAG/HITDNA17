using System;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    internal class ShowVersion : IMenuItemAction
    {
        public void Invoke()
        {
            showVersion();
        }

        private void showVersion()
        {
            Console.WriteLine("Version: 17.1.4.0");
            Console.Write("Press any key to continue...");
            Console.ReadLine();
        }
    }
}