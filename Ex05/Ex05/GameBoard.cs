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
        private const int k_SquareSize = 50;
        private const int k_BoardBorderSize = 3;
        private readonly Image r_CoinRed;
        private readonly Image r_CoinYellow;
        private readonly Image r_CoinPurple;
        private PictureBox[,] m_Grid;
        private GameService m_GameService;

        public GameBoard(GameService i_GameService)
        {
            r_CoinRed = Image.FromFile(@"images\CoinRed.png");
            r_CoinYellow = Image.FromFile(@"images\CoinYellow.png");
            r_CoinPurple = Image.FromFile(@"images\CoinPurple.png");
            m_GameService = i_GameService;

            InitializeComponent();
            initializeBoardComponents();
            ClientSize = new Size(m_Grid.GetLength(0) * k_SquareSize + k_BoardBorderSize * 2, m_Grid.GetLength(1) * k_SquareSize + k_BoardBorderSize * 2);

            m_GameService.OnFlip += Coin_Flipped;
            startNewRound();
        }

        private void startNewRound()
        {
            m_GameService.SetInitialBoard(eDiscColor.FirstColor);
            Text = string.Format("Othello - {0}'s Turn", m_GameService.ThisTurn.Name);
        }

        private void initializeBoardComponents()
        {
            m_Grid = new PictureBox[m_GameService.Board.GetLength(0), m_GameService.Board.GetLength(1)];
            for (int i = 0; i < m_Grid.GetLength(0); i++)
            {
                for (int j = 0; j < m_Grid.GetLength(1); j++)
                {
                    m_Grid[i, j] = new PictureBox();
                    m_Grid[i, j].Location = new Point(Location.X + k_BoardBorderSize + i * k_SquareSize, Location.Y + k_BoardBorderSize + j * k_SquareSize);
                    m_Grid[i, j].Size = new Size(k_SquareSize, k_SquareSize);
                    m_Grid[i, j].BackColor = (i + j) % 2 == 0 ? Color.MediumSeaGreen : Color.SeaGreen;
                    m_Grid[i, j].BorderStyle = BorderStyle.FixedSingle;
                    m_Grid[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    m_Grid[i, j].Click += GameBoard_Click;
                    m_Grid[i, j].MouseEnter += GameBoard_MouseEnter;
                    m_Grid[i, j].MouseLeave += GameBoard_MouseLeave;
                    m_Grid[i, j].Name = string.Format("{0},{1}", i, j);
                    Controls.Add(m_Grid[i, j]);
                }
            }
        }

        private void GameBoard_MouseLeave(object sender, EventArgs e)
        {
            PictureBox currentSquare = sender as PictureBox;
            OthelloPoint move = getSquareLocation(currentSquare);
            if (currentSquare.Image == r_CoinPurple)
            {
                m_Grid[move.X, move.Y].Image = null;
            }
        }

        private OthelloPoint getSquareLocation(PictureBox i_Square)
        {
            string[] clickLocation = i_Square.Name.Split(',');
            return new OthelloPoint(int.Parse(clickLocation[0]), int.Parse(clickLocation[1]));
        }

        private void GameBoard_MouseEnter(object sender, EventArgs e)
        {
            PictureBox currentSquare = sender as PictureBox;
            OthelloPoint move = getSquareLocation(currentSquare);
            if (currentSquare.Image == null && m_GameService.IsValidMove(move))
            {
                m_Grid[move.X, move.Y].Image = r_CoinPurple;
            }
        }

        private void GameBoard_Click(object sender, EventArgs e)
        {
            OthelloPoint move = getSquareLocation(sender as PictureBox);
            playTurn(move); // To opponent

            if (!m_GameService.HasMoves()) // Opponent (human/comp) has no moves
            {
                SwitchTurns(); // To this player
                if (!m_GameService.HasMoves()) // This player has no moves either
                {
                    gameOver();
                }
            }
            else if (m_GameService.ThisTurn.IsComputer) // Computer *has* moves
            {
                playComputerTurn();
            }
        }

        private void playComputerTurn()
        {
            bool isGameOver = false;
            bool firstPlayerHasMoves = false;
            OthelloPoint move;
            do
            {
                move = m_GameService.GetRandomMove();
                playTurn(move); // To this player
                firstPlayerHasMoves = m_GameService.HasMoves();
                if (!firstPlayerHasMoves) // This player no moves
                {
                    SwitchTurns(); //To computer
                    isGameOver = !m_GameService.HasMoves(); // Computer has no moves
                }
            } while (!firstPlayerHasMoves & !isGameOver);

            if(isGameOver)
            {
                gameOver();
            }
        }

        private void playTurn(OthelloPoint move)
        {
            if (m_GameService.IsValidMove(move))
            {
                m_GameService.UpdateBoard(move);
                SwitchTurns();
            }
        }

        private void SwitchTurns()
        {
            m_GameService.SwitchTurns();
            Text = string.Format("Othello - {0}'s Turn", m_GameService.ThisTurn.Name);
        }

        private Image parseColor(eDiscColor i_DiscColor)
        {
            Image coinImage;
            switch (i_DiscColor)
            {
                case eDiscColor.FirstColor:
                    coinImage = r_CoinRed;
                    break;
                case eDiscColor.SecondColor:
                    coinImage = r_CoinYellow;
                    break;
                default:
                    coinImage = null;
                    break;
            }
            return coinImage;
        }

        private void Coin_Flipped(int i_X, int i_Y, eDiscColor i_DiscColor)
        {
            m_Grid[i_X, i_Y].Image = parseColor(i_DiscColor);     
        }

        private void gameOver()
        {
            if (DialogResult.Yes == MessageBox.Show(generateResultsMessage(), "Othello", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                startNewRound();
            }
            else
            {
                Close();
            }
        }

        private string generateResultsMessage()
        {
            IPlayer winner = m_GameService.FirstPlayer;
            IPlayer loser = m_GameService.SecondPlayer;
            string resultsMessage;
            switch (m_GameService.GetGameResult())
            {
                case eGameResult.Tie:
                    resultsMessage = string.Format(@"Its a tie! ({0}/{0}) ({1})
Would you like another round?", winner.DiscsCounter, m_GameService.NumberOfPlayedRounds);
                    break;
                case eGameResult.FirstPlayerWon: // winner and lose assigned currectly at the beginning of this function
                    goto default;
                case eGameResult.SecondPlayerWon:
                    winner = m_GameService.SecondPlayer;
                    loser = m_GameService.FirstPlayer;
                    goto default;
                default:
                    resultsMessage = string.Format(@"{0} Won!! ({1}/{2}) ({3}/{4})
Would you like another round?", winner.Name, winner.DiscsCounter, loser.DiscsCounter, winner.RoundsWon, m_GameService.NumberOfPlayedRounds);
                    break;
            }

            return resultsMessage;
        }
    }
}
