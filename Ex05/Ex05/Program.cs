using Ex02_Othelo;
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
            GameSettingsForm gameSettingsForm = new GameSettingsForm();
            if (gameSettingsForm.ShowDialog() == DialogResult.OK)
            {
                GameService gameService = new GameService(gameSettingsForm.BoardSize, "Red", "Yellow", gameSettingsForm.IsComputerOpponent);
                (new GameBoard(gameService)).ShowDialog();
            }
        }
    }
}
