using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ex05
{
    public partial class Form1 : Form
    {
        const int k_BoardSize = 6;
        const int k_SquareSize = 50;

        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < m_Grid.GetLength(0); i++)
            {
                for (int j = 0; j < m_Grid.GetLength(1); j++)
                {
                    m_Grid[i, j] = new PictureBox();
                    //m_Grid[i, j].Anchor = AnchorStyles.Bottom & AnchorStyles.Right & AnchorStyles.Top & AnchorStyles.Left;
                    m_Grid[i, j].Location = new Point(Location.X + i * k_SquareSize, Location.Y + j * k_SquareSize);
                    m_Grid[i, j].Size = new Size(k_SquareSize, k_SquareSize);
                    m_Grid[i, j].BackColor = (i+j)%2==0?Color.Blue:Color.Black;
                    Controls.Add(m_Grid[i, j]);
                }
            }
            ClientSize = new Size(m_Grid.GetLength(0) * k_SquareSize, m_Grid.GetLength(1) * k_SquareSize);
        }

        PictureBox[,] m_Grid = new PictureBox[k_BoardSize, k_BoardSize];
        
    }
}
