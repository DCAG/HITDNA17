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
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < m_Grid.GetLength(); i++)
            {
                for (int j = 0; j < m_Grid.GetLength(1); j++)
                {
                    m_Grid[i, j] = new PictureBox();
                    m_Grid[i, j].Anchor = AnchorStyles.Bottom & AnchorStyles.Right & AnchorStyles.Top & AnchorStyles.Left;
                    m_Grid[i, j].Location = new Point(0, 0);
                    m_Grid[i, j].Size = new Size(50, 50);
                    m_Grid[i, j].BackColor = Color.Blue;
                    Controls.Add(m_Grid[0, 0]);
                }
            }
        }

        PictureBox[,] m_Grid = new PictureBox[6,6];
        
    }
}
