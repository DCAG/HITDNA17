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

        private int m_BoardSize = k_MinBoardSize;
        private readonly string r_ButtonBoardSizeText = "Board Size: {0}x{0} (click to increase)";
        private bool m_PlayerVsComputer = false;

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }
        }

        public bool IsComputerOpponent
        {
            get
            {
                return m_PlayerVsComputer;
            }
        }

        public GameSettingsForm()
        {
            InitializeComponent();
            MinimizeBox = false;
            MaximizeBox = false;
            ButtonBoardSize.Text = string.Format(r_ButtonBoardSizeText, k_MinBoardSize);
        }

        private void buttonVsPlayer_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonVsComputer_Click(object sender, EventArgs e)
        {
            m_PlayerVsComputer = true;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonBoardSize_Click(object sender, EventArgs e)
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
