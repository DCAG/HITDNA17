using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OthelloLogic;

namespace Ex05
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GameManager.StartGame();
        }
    }
}
