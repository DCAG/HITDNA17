using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Test
{
    class Program
    {
        public static void Main()
        {
            showInterfacesMainMenu();
            showDelegatesMainMenu();
        }

        private static void showInterfacesMainMenu()
        {
            Interfaces.MainMenu m_InterfacesMainMenu = new Interfaces.MainMenu("Iterfaces Main Menu");

            Interfaces.Menu showDateTime = new Interfaces.Menu("Show Date/Time");
            showDateTime.Add(new ShowTime() as Interfaces.IMenuItemAction, "Show Time");
            showDateTime.Add(new ShowDate() as Interfaces.IMenuItemAction, "Show Date");
            m_InterfacesMainMenu.Add(showDateTime as Interfaces.IMenuItemAction, showDateTime.Title);

            Interfaces.Menu versionAndActions = new Interfaces.Menu("Version and Actions");
            versionAndActions.Add(new ShowVersion() as Interfaces.IMenuItemAction, "Show Version");

            Interfaces.Menu actions = new Interfaces.Menu("Actions");
            actions.Add(new CharsCount() as Interfaces.IMenuItemAction, "Chars Count");
            actions.Add(new CountSpaces() as Interfaces.IMenuItemAction, "Count Spaces");

            versionAndActions.Add(actions as Interfaces.IMenuItemAction, actions.Title);
            m_InterfacesMainMenu.Add(versionAndActions as Interfaces.IMenuItemAction, versionAndActions.Title);

            m_InterfacesMainMenu.Show();
        }

        private static void showDelegatesMainMenu()
        {
            Delegates.MainMenu m_DelegatesMainMenu = new Delegates.MainMenu();
            m_DelegatesMainMenu.Show();
        }
    } 
}