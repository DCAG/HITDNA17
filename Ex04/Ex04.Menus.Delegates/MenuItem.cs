using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Delegates
{
    public delegate void MenuItemSelectionNotificationDelegate();

    public class MenuItem
    {
        public event MenuItemSelectionNotificationDelegate MenuItemSelected;

        private string m_title;

        public string Title
        {
            get
            {
                return m_title;
            }
        }

        public MenuItem(string i_title)
        {
            m_title = i_title;
        }

        public virtual void OnMenuItemSelected()
        {
            if (MenuItemSelected != null)
            {
                MenuItemSelected.Invoke();
            }
        }
    }
}
