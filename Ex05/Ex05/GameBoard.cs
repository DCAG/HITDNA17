using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ex05
{
    public partial class GameBoard : Form
    {
        const int k_BoardSize = 6;
        const int k_SquareSize = 50;
        readonly Image r_CoinRed = Image.FromFile(@"images\CoinRed.png");
        readonly Image r_CoinYellow = Image.FromFile(@"images\CoinYellow.png");
        public GameBoard()
        {
            InitializeComponent();
            for (int i = 0; i < m_Grid.GetLength(0); i++)
            {
                for (int j = 0; j < m_Grid.GetLength(1); j++)
                {
                    m_Grid[i, j] = new PictureBox();
                    //m_Grid[i, j].Anchor = AnchorStyles.Bottom & AnchorStyles.Right & AnchorStyles.Top & AnchorStyles.Left;
                    m_Grid[i, j].Location = new Point(Location.X + 3 + i * k_SquareSize, Location.Y + 3 + j * k_SquareSize);
                    m_Grid[i, j].Size = new Size(k_SquareSize, k_SquareSize);
                    m_Grid[i, j].BackColor = Color.BlueViolet; //(i+j)%2==0?Color.Blue:Color.Black;
                    m_Grid[i, j].BorderStyle = BorderStyle.FixedSingle;
                    m_Grid[i, j].Image = r_CoinRed;
                    m_Grid[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    Controls.Add(m_Grid[i, j]);
                }
            }
            ClientSize = new Size(m_Grid.GetLength(0) * k_SquareSize + 3 * 2, m_Grid.GetLength(1) * k_SquareSize + 3 * 2);
        }

        PictureBox[,] m_Grid = new PictureBox[k_BoardSize, k_BoardSize];
        
    }
}
