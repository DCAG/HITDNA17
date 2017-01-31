using Ex04.Menus.Interfaces;
using System;

namespace Ex04.Menus.Test
{
    internal class ShowVersion : IMenuItemAction
    {
        public void Invoke()
        {
            Console.WriteLine("Version: 17.1.4.0");
        }
    }
}