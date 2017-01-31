using Ex04.Menus.Interfaces;
using System;

namespace Ex04.Menus.Test
{
    internal class CharsCount : IMenuItemAction
    {
        public void Invoke()
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
        }
    }
}