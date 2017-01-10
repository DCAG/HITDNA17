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
        private const bool v_ComputerPlayer = true;
        private const string k_ComputerPlayerName = "Computer";
        public GameManager()
        {
            m_Player1 = new Player(AskPlayerName(), GameBoard.White, !v_ComputerPlayer);

            if (AskYesNoQuestion("Is 2nd player human? (computer is default)"))
            {
                m_Player2 = new Player(AskPlayerName(),GameBoard.Black,!v_ComputerPlayer);
            }
            else
            {
                m_Player2 = new Player(k_ComputerPlayerName, GameBoard.Black, v_ComputerPlayer);
            }

            m_GameBoard = new GameBoard(BoardSize());
            do
            {
                m_GameBoard.SetInitialBoard();
                Play();
            }
            while(!quit && AskYesNoQuestion("Play another round?"));
        }

        
        private bool AskYesNoQuestion(string i_Question)
        {
            Console.Write("{0} [y/n]: ",i_Question);
            string answerStr = Console.ReadLine();
            while (System.Text.RegularExpressions.Regex.IsMatch(answerStr, "^(y|Y|n|N)$"))
            {
                Console.Write("{0} [y/n]: ", i_Question);
                answerStr = Console.ReadLine();
            }
            return System.Text.RegularExpressions.Regex.IsMatch(answerStr, "^(y|Y)$");
        }

        private void Play()
        {
            PrintBoard(m_GameBoard.Board);
            do
            {
                if (m_GameBoard.HasMoves(m_Player1.WhiteDisc))
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

                if (m_GameBoard.HasMoves(m_Player2.WhiteDisc))
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
            } while (m_GameBoard.HasMoves(m_Player1.WhiteDisc) || m_GameBoard.HasMoves(m_Player2.WhiteDisc));
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

        private void PrintNoMoves(Player i_Player)
        {
            Console.WriteLine("{0} has available moves", i_Player);
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

        private string AskPlayerName()
        {
            string name_Player;

            Console.WriteLine("Please enter your name");
            name_Player=Console.ReadLine();

            return name_Player;
        }
    }
}
