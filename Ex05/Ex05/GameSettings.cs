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
    internal partial class GameSettings : Form
    {
        private const int k_MinBoardSize = 6;
        private const int k_MaxBoardSize = 12;
        private const int k_BoardSizeIncreaseDelta = 2;
        private readonly string r_ButtonBoardSizeText = "Board Size: {0}x{0} (click to increase)";

        private int m_BoardSize = k_MinBoardSize;
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

        public GameSettings()
        {
            InitializeComponent();
            MinimizeBox = false;
            MaximizeBox = false;
            buttonBoardSize.Text = string.Format(r_ButtonBoardSizeText, k_MinBoardSize);
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
            else
            {
                m_BoardSize = k_MinBoardSize;
            }

            buttonBoardSize.Text = string.Format(r_ButtonBoardSizeText, m_BoardSize);
        }
    }
}
