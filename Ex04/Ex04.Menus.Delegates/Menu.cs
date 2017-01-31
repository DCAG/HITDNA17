using System;
using System.Collections.Generic;

namespace Ex04.Menus.Delegates
{
    
    public class Menu : MenuItem
    {
        private const string k_CloseMenuStr = "Back";
        private readonly List<MenuItem> r_MenuItems;

        protected virtual string CloseMenuStr
        {
            get
            {
                return k_CloseMenuStr;
            }
        }

        public Menu(string i_Title) : base(i_Title)
        {
            r_MenuItems = new List<MenuItem>();
        }

        public override void Invoke()
        {
            Show();
        }

        public virtual void Show()
        {
            MenuItem selectedItem;
            bool closeMenu = false;
            do
            {
                Console.Clear();
                printCurrentMenuLevel();
                selectedItem = readUserSelection();
                closeMenu = selectedItem == null;

                if (!closeMenu)
                {
                    Console.Clear();
                    selectedItem.Invoke();
                }
            }
            while (!closeMenu);
        }

        private MenuItem readUserSelection()
        {
            MenuItem selectedItem;
            string selectedItemStr;

            do
            {
                Console.Write("Please select one item from the list or 0 to {0}:", CloseMenuStr);
                selectedItemStr = Console.ReadLine();
            }
            while (!validateUserInput(selectedItemStr, out selectedItem));

            return selectedItem;
        }

        private bool validateUserInput(string selectedItemStr, out MenuItem selectedItem)
        {
            bool result = false;
            selectedItem = null;
            try
            {
                int selecedItemNum = int.Parse(selectedItemStr);
                if (selecedItemNum != 0)
                { 
                    selectedItem = r_MenuItems[selecedItemNum - 1];
                }
                result = true;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input.");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Invalid input.");
            }

            return result;
        }

        private void printCurrentMenuLevel()
        {
            Console.WriteLine(Title);
            Console.WriteLine(new string('-', Title.Length));
            for (int i = 0; i < r_MenuItems.Count; i++)
            {
                Console.WriteLine("{0} - {1}", i + 1, r_MenuItems[i].Title);
            }
            Console.WriteLine();
            Console.WriteLine("0 - {0}", CloseMenuStr);
            Console.WriteLine();
        }

        public void Add(MenuItem i_MenuItem)
        {
            r_MenuItems.Add(i_MenuItem);
        }
    }
}