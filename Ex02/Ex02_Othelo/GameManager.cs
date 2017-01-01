using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    enum CoinSideColor
    {
        Black,
        White
    }
    public class GameManager
    {
        GameBoard m_Board;
        Player m_PlayerA;
        Player m_PlayerB;
        int m_RoundNumber;
        OtheloPrinter m_OtheloPrinter;

        public GameManager()//set defaults
        {
            m_PlayerA = new Player() { Type = PlayerType.Human };
            m_PlayerB = new Player() { Type = PlayerType.Computer };
            m_RoundNumber = 0;
            m_OtheloPrinter = new OtheloPrinter();
        }

        internal void Initialize()
        {
            //1
            m_PlayerA.Name = m_OtheloPrinter.ReadPlayerName();
            //2
            if (m_OtheloPrinter.ReadIsMultiplayer())
            {
                m_PlayerB.Name = m_OtheloPrinter.ReadPlayerName();
                m_PlayerB.Type = PlayerType.Human;
            }
            //3
            m_Board.Size = m_OtheloPrinter.ReadBoardSize();
        }

        internal void StartGame()
        {
            do
            {
                StartGameRound();//4
            }
            while (m_OtheloPrinter.ReadAnotherRound());//10
            m_OtheloPrinter.PrintEndMessage();//10
        }

        private void StartGameRound()
        {
            m_RoundNumber++;
            m_Board.SetBoard();
            Play();
            //and at the end of the round:
            CalculateWinner();
        }

        void Play()
        {
            m_OtheloPrinter.PrintBoard(m_Board);
            Player currentTurn = m_PlayerA;
            do
            {
                m_Board.setSuqre(currentPlayer.PlayTurn()); //parameters are [color] and square [column] and [row]
                if (currentTurn == m_PlayerA)
                {
                    currentTurn = m_PlayerB;
                }
                else
                {
                    currentTurn = m_PlayerB;
                }
            }
            while (m_PlayerA.HasMoves() || m_PlayerB.HasMoves());
        }

        Square PlayTurn(Player i_Player)
        {
            if (i_Player.SelectSquare())
            {
                Ex02.ConsoleUtils.Screen.Clear();
            }
        }
        void GetAvailableSquares(Player i_Player)
        {
            
        }
    }
}
