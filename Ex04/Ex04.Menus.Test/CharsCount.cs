using System;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    internal class CharsCount : IMenuItemAction
    {
        public void Invoke()
        {
            countLetters();
        }

        private void countLetters() // The name of the menu item was requested to be "Chars Count" although it was also requested to invoke a function that counts the number of *letters* in the user input. 
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
    }
}