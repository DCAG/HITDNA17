using Ex02_Othelo;
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
        PictureBox[,] m_Grid = new PictureBox[k_BoardSize, k_BoardSize];
        private GameService m_GameService;
        public GameBoard(GameService i_GameService)
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
                    m_Grid[i, j].Click += GameBoard_Click;
                    m_Grid[i, j].Name = string.Format("{0},{1}", i, j);
                    Controls.Add(m_Grid[i, j]);
                }
            }

            ClientSize = new Size(m_Grid.GetLength(0) * k_SquareSize + 3 * 2, m_Grid.GetLength(1) * k_SquareSize + 3 * 2);
            m_GameService = i_GameService;
            m_GameService.OnFlip += Coin_Flipped;
        }

        private void GameBoard_Click(object sender, EventArgs e)
        {
            (sender as PictureBox).Name.Split(',');
        }

        private void Coin_Flipped(int i_X, int i_Y, eDiscColor i_DiscColor)
        {
            switch(i_DiscColor)
            {
                case eDiscColor.Black:
                    m_Grid[i_X, i_Y].Image = r_CoinRed;
                    break;
                case eDiscColor.White:
                    m_Grid[i_X, i_Y].Image = r_CoinYellow;
                    break;
                default:
                    m_Grid[i_X, i_Y].Image = null;
                    break;
            }
        }
        
    }
}
