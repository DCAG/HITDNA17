using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Interfaces
{
    public class MainMenu
    {
        string m_Title;
        Dictionary<int,MenuItem> m_MenuItems;
        MenuItem m_BackOrExit; //access mod? const is impossible... so is it ok to leave it here like this?

        public MainMenu()
        {
            m_MenuItems[0] = m_BackOrExit;
        }

        public void Show()
        {
            MenuItem selectedItem = null;
            do
            {
                printCurrentMenuLevel();
                printRequestToSelectOneItem();
                string selectedItemStr = readUserSelection();
                if (validateUserInput(selectedItemStr,out selectedItem))
                {
                    clearScreen();
                    selectedItem.Invoke();
                }
            }
            while (!m_BackOrExit.Equals(selectedItem));
        }

        private void clearScreen()
        {
            Console.Clear();
        }

        private bool validateUserInput(string selectedItemStr, out MenuItem selectedItem)
        {
            bool validationSuccessful = false;
            selectedItem = null;
            try
            {
                selectedItem = m_MenuItems[int.Parse(selectedItemStr)];
                validationSuccessful = true;
            }
            catch(FormatException ex)
            {

            }
            catch(NullReferenceException ex)
            {

            }
            
            return validationSuccessful;
        }

        private string readUserSelection()
        {
            return Console.ReadLine();
        }

        private void printRequestToSelectOneItem()
        {
            foreach(KeyValuePair<int,MenuItem> item in m_MenuItems)
            {
                Console.WriteLine("{0} - {1}",item.Key,item.Value.ToString());
            }
        }

        private void printCurrentMenuLevel()
        {
            Console.WriteLine(m_Title);
        }
    }
}
