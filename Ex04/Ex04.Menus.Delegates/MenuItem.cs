using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Delegates
{
    public delegate void MenuItemSelectionEventHandler();
    public class MenuItem
    {
        public event MenuItemSelectionEventHandler MenuItemAction;
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

        public virtual void Invoke()
        {
            if (MenuItemAction != null)
            {
                MenuItemAction.Invoke();
            }
        }
    }
}
