using System;

namespace Ex04.Menus.Test
{
    public class Program
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
            Delegates.MainMenu delegatesMainMenu = new Delegates.MainMenu("Delegates Main Menu");

            Delegates.Menu showDateTimeMenu = new Delegates.Menu("Show Date/Time");
            delegatesMainMenu.Add(showDateTimeMenu);
            Delegates.MenuItem showTimeMenuItem = new Delegates.MenuItem("Show Time");
            Delegates.MenuItem showDateMenuItem = new Delegates.MenuItem("Show Date");
            showDateTimeMenu.Add(showTimeMenuItem);
            showDateTimeMenu.Add(showDateMenuItem);
            showTimeMenuItem.MenuItemSelected += showTime;
            showDateMenuItem.MenuItemSelected += showDate;

            Delegates.Menu versionAndActionsMenu = new Delegates.Menu("Version and Actions");
            delegatesMainMenu.Add(versionAndActionsMenu);

            Delegates.MenuItem showVersionMenuItem = new Delegates.MenuItem("Show Version");
            versionAndActionsMenu.Add(showVersionMenuItem);
            showVersionMenuItem.MenuItemSelected += showVersion;

            Delegates.Menu actionsMenu = new Delegates.Menu("Actions");
            versionAndActionsMenu.Add(actionsMenu);
            Delegates.MenuItem charsCountMenuItem = new Delegates.MenuItem("Chars Count");
            actionsMenu.Add(charsCountMenuItem);
            charsCountMenuItem.MenuItemSelected += countLetters; // The name of the menu item was requested to be "Chars Count" although it was also requested to invoke a function that counts the number of *letters* in the user input. 
            Delegates.MenuItem countSpacesMenuItem = new Delegates.MenuItem("Count Spaces");
            actionsMenu.Add(countSpacesMenuItem);
            countSpacesMenuItem.MenuItemSelected += countSpaces;

            delegatesMainMenu.Show();
        }

        private static void countLetters()
        {
            Console.WriteLine("Write a sentence:");
            string sentence = Console.ReadLine();

            int lettersCounter = 0;
            for (int i = 0; i < sentence.Length; i++)
            {
                if (char.IsLetter(sentence[i]))
                {
                    lettersCounter++;
                }
            }

            Console.WriteLine("There are {0} letters in this sentence", lettersCounter);
            Console.Write("Press any key to continue...");
            Console.ReadLine();
        }

        private static void countSpaces()
        {
            Console.WriteLine("Write a sentence:");
            string sentence = Console.ReadLine();

            int spacesCounter = 0;
            for (int i = 0; i < sentence.Length; i++)
            {
                if (sentence[i] == ' ')
                {
                    spacesCounter++;
                }
            }

            Console.WriteLine("There are {0} spaces in this sentence", spacesCounter);
            Console.Write("Press any key to continue...");
            Console.ReadLine();
        }

        private static void showVersion()
        {
            Console.WriteLine("Version: 17.1.4.0");
            Console.Write("Press any key to continue...");
            Console.ReadLine();
        }

        private static void showTime()
        {
            Console.WriteLine(DateTime.Now.ToShortTimeString());
            Console.Write("Press any key to continue...");
            Console.ReadLine();
        }

        private static void showDate()
        {
            Console.WriteLine(DateTime.Now.ToShortDateString());
            Console.Write("Press any key to continue...");
            Console.ReadLine();
        }
    } 
}