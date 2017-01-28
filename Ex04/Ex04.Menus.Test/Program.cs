using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Test
{
    class Program
    {
        public static void Main()
        {
            Interfaces.MainMenu m_InterfacesMainMenu = new Interfaces.MainMenu();
            Delegates.MainMenu m_DelegatesMainMenu = new Delegates.MainMenu();

            m_InterfacesMainMenu.Show();
            m_DelegatesMainMenu.Show();
        }
    }
}