using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    public class GameManager
    {
        GameBoard m_GameBoard;
        Player m_Player1, m_Player2;
        bool quit;

        public GameManager()
        {
            m_Player1.Name = AskPlayerName();

            if(TwoPlayers())
            {
                m_Player2.Name = AskPlayerName();
            }

            m_GameBoard = new GameBoard(BoardSize());
            do
            {
                m_GameBoard.SetInitialBoard();
                Play();
            }
            while(!quit && PlayerWantsAnotherRound());
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
                    if (quit)
                        break;
                    PrintBoard(m_GameBoard.Board);
                }
                else
                {
                    PrintNoMoves(m_Player1);
                }

                if (m_GameBoard.HasMoves(m_Player2))
                {
                    PlayTurn(m_Player2);
                    if (quit)
                        break;
                    PrintBoard(m_GameBoard.Board);
                }
                else
                {
                    PrintNoMoves(m_Player2);
                }
            } while (m_GameBoard.HasMoves(m_Player1) || m_GameBoard.HasMoves(m_Player2));
            if (!quit)
                PrintHighscore();
        }

        private void PrintHighscore()
        {
            //m_GameBoard.GetNumOfCoins(Player1);
            //m_GameBoard.GetNumOfCoins(Player2);
            throw new NotImplementedException();
        }

        private bool tryParseStrToCoordinates(string i_str, out int o_Row, out int o_Column)
        {
            bool success = true;
            try
            {
                o_Column = i_str[0] - 'A';
                o_Row = i_str[1] - '0';
            }
            catch
            {
                o_Column = -1;
                o_Row = -1;
                success = false;
            }
            return success;
        }

        private void PrintNoMoves(Player m_Player1)
        {
            throw new NotImplementedException();
        }

        private void PlayTurn(Player i_Player)
        {
            string moveStr = string.Empty;
            Point move = new Point(-1, -1);
            do
            {
                if (i_Player.IsComputer)
                    move = m_GameBoard.GetRandomMove(i_Player.WhiteDisc);
                else
                {
                    //move = ask player for his decision
                    Console.WriteLine("It is {0}'s turn, choose a square or Q to exit:", i_Player.Name);
                    moveStr = Console.ReadLine();
                    if(!tryParseStrToCoordinates(out move))
                    {
                        Console.WriteLine("Move request was not in the right format.");
                    }
                    //if player input is 'Q' exit (){ quit = true ; break;}
                }
            } while (!m_GameBoard.UpdateBoard(,,i_Player.WhiteDisc); //if illegal square was chosen try again //update board

        }

        private void PrintBoard(bool?[,] board)
        {
            printBoardColumnsHeader(board);
            printBoardLineSeperator(board);
            for (int i=0;i<board.Length;i++)
            {
                printBoardRow(board, i);
                printBoardLineSeperator(board);
            }
        }

        private void printBoardRow(bool?[,] board, int i)
        {
            Console.Write("{0} |", i); // row number
            for (int j = 0; j < board.Length; j++)
            {
                Console.Write("{0} |", board[i, j] == null ? " " : board[i, j].GetValueOrDefault() ? "o" : "x");
            }
            Console.WriteLine();
        }

        private void printBoardColumnsHeader(bool?[,] board)
        {
            Console.Write(" ");
            for (int i = 0; i < board.Length; i++)
            {
                Console.Write("   {0}", (char)('A' + i)); //column letter
            }
            Console.WriteLine();
        }

        private void printBoardLineSeperator(bool?[,] board)
        {
            for (int i = 0; i < 4 * board.GetLength(0) + 1; i++)
            {
                Console.Write('=');
            }
            Console.WriteLine();
        }

        private int BoardSize()
        {
            int SizeOfBoard;

            do
            {
                Console.WriteLine("Please write the size of the board you want (6 or 8):");
            }
            while (!int.TryParse(Console.ReadLine(), out SizeOfBoard) && SizeOfBoard != 6 && SizeOfBoard != 8);

            return SizeOfBoard;
        }


        private bool TwoPlayers()
        {
            string answer;
            bool twoPlayer = false;
            do
            {
                Console.WriteLine("if you want to play with player please write yes, else (with computer) write no");
                answer = Console.ReadLine();

                if (answer == "yes")
                {
                    twoPlayer = true;
                }
            }
            while (answer != "no" || answer != "yes");

            return twoPlayer;
        }

        private string AskPlayerName()
        {
            string name_Player;

            Console.WriteLine("Please enter your name");
            name_Player=Console.ReadLine();

            return name_Player;
        }
    }
}
