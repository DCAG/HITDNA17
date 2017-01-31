using Ex04.Menus.Interfaces;
using System;

namespace Ex04.Menus.Test
{
    internal class ShowDate : IMenuItemAction
    {
        public void Invoke()
        {
            Console.WriteLine(DateTime.Now.Date.ToString());
        }
    }
}