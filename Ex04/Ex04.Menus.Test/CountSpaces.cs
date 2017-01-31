using System;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    internal class CountSpaces : IMenuItemAction
    {
        public void Invoke()
        {
            countSpaces();
        }

        private void countSpaces()
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
    }
}