using Ex04.Menus.Interfaces;
using System;

namespace Ex04.Menus.Test
{
    internal class CountSpaces : IMenuItemAction
    {
        public void Invoke()
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
        }
    }
}