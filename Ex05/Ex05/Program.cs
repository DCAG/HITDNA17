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

            Ex02_Othelo.GameService gameService;

            DialogResult result = (new GameSettingsForm(gameService)).ShowDialog();
            if (result == DialogResult.OK)
            {
                (new GameBoard()).ShowDialog();
            }
        }
    }
}
