using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Delegates
{
    public delegate void MenuItemSelectionEventHandler();
    public class MenuItem
    {
        public event MenuItemSelectionEventHandler MenuItemAction;
        private string m_Name;
        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        public MenuItem(string i_Name)
        {
            m_Name = i_Name;
        }

        public void Invoke()
        {
            if (MenuItemAction != null)
            {
                MenuItemAction.Invoke();
            }
        }
    }
}
