using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ex02_Othelo;

namespace Ex05
{
    public partial class GameSettingsForm : Form
    {
        const int k_MinBoardSize = 6;
        const int k_MaxBoardSize = 12;
        const int k_BoardSizeIncreaseDelta = 2;

        int m_BoardSize = k_MinBoardSize;
        readonly string r_ButtonBoardSizeText = "Board Size: {0}x{0} (click to increase)";
        bool m_PlayerVsComputer = false;
        private GameService gameService;

        public GameSettingsForm()
        {
            InitializeComponent();
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            ButtonBoardSize.Text = string.Format(r_ButtonBoardSizeText, k_MinBoardSize);
        }

        public GameSettingsForm(GameService i_GameService)
        {
            gameService = i_GameService;
        }

        private void buttonVsPlayer_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void buttonVsComputer_Click(object sender, EventArgs e)
        {
            m_PlayerVsComputer = true;
            DialogResult = DialogResult.OK;
            gameService.
        }

        private void ButtonBoardSize_Click(object sender, EventArgs e)
        {
            m_BoardSize += k_BoardSizeIncreaseDelta;
            if (m_BoardSize > k_MaxBoardSize)
            {
                m_BoardSize -= k_BoardSizeIncreaseDelta;
            }

            ButtonBoardSize.Text = string.Format(r_ButtonBoardSizeText, m_BoardSize);
        }
    }
}
