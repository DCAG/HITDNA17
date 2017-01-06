using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    public class GameManager
    {
        GameBoard m_GameBoard;
        Player m_Player1, m_Player2;

        GameManager()
        {
            m_Player1.Name = AskPlayerName();

            if(IsMultiplayer())
            {
                m_Player2.Name = AskPlayerName();
            }

            m_GameBoard = new GameBoard(AskBoardSize());
            do
            {
                m_GameBoard.SetInitialBoard();
                Play();
            }
            while(PlayerWantsAnotherRound());
        }

        private bool PlayerWantsAnotherRound()
        {
            throw new NotImplementedException();
        }

        private void Play()
        {
            PrintBoard(m_GameBoard.Board);
            do
            {
                if (m_GameBoard.HasMoves(m_Player1))
                {
                    PlayTurn(m_Player1);
                    PrintBoard(m_GameBoard.Board);
                }
                else
                {
                    PrintNoMoves(m_Player1);
                }

                if (m_GameBoard.HasMoves(m_Player2))
                {
                    PlayTurn(m_Player2);
                    PrintBoard(m_GameBoard.Board);
                }
                else
                {
                    PrintNoMoves(m_Player2);
                }
            } while (m_GameBoard.HasMoves(m_Player1) || m_GameBoard.HasMoves(m_Player2));

            PrintHighscore();
        }

        private void PrintHighscore()
        {
            //m_GameBoard.GetNumOfCoins(Player1);
            //m_GameBoard.GetNumOfCoins(Player2);
            throw new NotImplementedException();
        }

        private void PrintNoMoves(Player m_Player1)
        {
            throw new NotImplementedException();
        }

        private void PlayTurn(Player m_Player1)
        {
            //ask player for his decision
            //if player input is 'Q' exit
            //if illegal square was chosen try again
            //update board
            throw new NotImplementedException();
        }

        private void PrintBoard(char[,] board)
        {
            throw new NotImplementedException();
        }

        private int AskBoardSize()
        {
            throw new NotImplementedException();
        }

        private bool IsMultiplayer()
        {
            throw new NotImplementedException();
        }

        private string AskPlayerName()
        {
            throw new NotImplementedException();
        }
    }
}
