using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Ex02_Othelo;
using System.Windows.Forms;

namespace Ex05
{
    class GameManager
    {
        private GameService m_GameService;
        private Player m_FirstPlayer, m_Opponent;
        public void StartGame()
        {
            DialogResult result = (new GameSettingsForm()).ShowDialog();
            if (result == DialogResult.OK)
            {
                (new GameBoard()).ShowDialog();
            }
        }
    }
}
