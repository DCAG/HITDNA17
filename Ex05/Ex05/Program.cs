using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Ex05
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            (new GameManager()).StartGame();
        }
    }
}
