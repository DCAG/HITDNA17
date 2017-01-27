using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Test
{
    class Program
    {
        public static void Main()
        {
            Interfaces.MainMenu interfacesMenu = new Interfaces.MainMenu();
            Delegates.MainMenu delegatesMenu = new Delegates.MainMenu();
        }
    }
}