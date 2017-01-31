using System;
using System.Collections.Generic;

namespace Ex04.Menus.Interfaces
{
    public class Menu : IMenuItemAction
    {
        private const string k_CloseMenuStr = "Back";
        private string m_Title;
        private readonly List<MenuItem> m_MenuItems;

        public string Title
        {
            get
            {
                return m_Title;
            }
        }

        protected virtual string CloseMenuStr
        {
            get
            {
                return k_CloseMenuStr;
            }
        }

        public Menu(string i_Title)
        {
            m_Title = i_Title;
            m_MenuItems = new List<MenuItem>();
        }

        void IMenuItemAction.Invoke()
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

                if(!closeMenu)
                {
                    Console.Clear();
                    selectedItem.MenuItemAction.Invoke();
                    if (!(selectedItem.MenuItemAction is Menu))
                    {
                        Console.Write("Press any key to continue...");
                        Console.ReadLine();
                    }
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
                    selectedItem = m_MenuItems[selecedItemNum - 1];
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
            Console.WriteLine(new string('-',Title.Length));
            for (int i=0; i < m_MenuItems.Count; i++)
            {
                Console.WriteLine("{0} - {1}", i + 1, m_MenuItems[i].Name);
            }
            Console.WriteLine();
            Console.WriteLine("0 - {0}", CloseMenuStr);
            Console.WriteLine();
        }

        public void Add(IMenuItemAction i_MenuItem, string i_MenuItemName)
        {
            m_MenuItems.Add(new MenuItem(i_MenuItem,i_MenuItemName));
        }

        public void Remove(IMenuItemAction i_MenuItem)
        {
            //??????m_MenuItems.Remove(MenuItem)
        }

        private class MenuItem
        {
            private string m_Name;
            private readonly IMenuItemAction m_Item;

            public string Name
            {
                get
                {
                    return m_Name;
                }
            }

            public IMenuItemAction MenuItemAction
            {
                get
                {
                    return m_Item;
                }
            }

            public MenuItem(IMenuItemAction i_Item, string i_Name)
            {
                m_Item = i_Item;
                m_Name = i_Name;
            }
        }
    }
}