using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ex02_Othelo
{
    public class GameManager
    {
        GameBoard m_GameBoard;
        Player m_Player1, m_Player2;
        bool m_Quit;
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

            m_GameBoard = new GameBoard(AskBoardSize());
        }

        public void Start()
        {
            do
            {
                m_GameBoard.SetInitialBoard();
                play();
            }
            while (!m_Quit && AskYesNoQuestion("Play another round?"));
        }

        private void play()
        {
            PrintBoard(m_GameBoard.Board);
            do
            {
                if (m_GameBoard.HasMoves(m_Player1.WhiteDisc))
                {
                    PlayTurn(m_Player1);
                    if (m_Quit)
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
                    if (m_Quit)
                        break;
                    PrintBoard(m_GameBoard.Board);
                }
                else
                {
                    PrintNoMoves(m_Player2);
                }
            } while (m_GameBoard.HasMoves(m_Player1.WhiteDisc) || m_GameBoard.HasMoves(m_Player2.WhiteDisc));
            if (!m_Quit)
                PrintHighscore();
        }

        private bool AskYesNoQuestion(string i_Question)
        {
            Console.Write("{0} [y/n]: ", i_Question);
            string answerStr = Console.ReadLine();
            while (!System.Text.RegularExpressions.Regex.IsMatch(answerStr, "^(y|Y|n|N)$"))
            {
                Console.Write("{0} [y/n]: ", i_Question);
                answerStr = Console.ReadLine();
            }
            return System.Text.RegularExpressions.Regex.IsMatch(answerStr, "^(y|Y)$");
        }

        private void PrintHighscore()
        {
            //m_GameBoard.GetNumOfCoins(Player1);
            //m_GameBoard.GetNumOfCoins(Player2);
            throw new NotImplementedException();
        }

        private void PrintNoMoves(Player i_Player)
        {
            Console.WriteLine("{0} has no available moves", i_Player.Name);
        }

        private void PlayTurn(Player i_Player)
        {
            string moveStr = string.Empty;
            Point move = new Point(-1, -1);
            do
            {
                if (i_Player.IsComputer)
                {
                    move = m_GameBoard.GetRandomMove(i_Player.WhiteDisc);
                }
                else
                {
                    Console.WriteLine("It is {0}'s turn, choose a square or Q to exit:", i_Player.Name);
                    move = AskPlayerMoveOrQuit(out m_Quit);
                }
            }
            while (!m_GameBoard.IsValidMove(move, i_Player.WhiteDisc)); //if illegal square was chosen try again //update board
            m_GameBoard.UpdateBoard(move, i_Player.WhiteDisc);
        }

        private Point AskPlayerMoveOrQuit(out bool o_Quit)
        {
            Regex regex = new Regex("^((?<Column>[A-Za-z]{1})(?<Row>[1-9]{1})|(?<Quit>Q|q))$");
            Console.WriteLine("Enter coordinates of youer next move or Q to quit:\nFor coordinates enter [A-Z] letter (column) followed by [1-9] number (row) e.g. A5");
            Match match = regex.Match(Console.ReadLine());
            while (!match.Success)
            {
                Console.WriteLine("Invalid input!");
                match = regex.Match(Console.ReadLine());
            }

            Point result = new Point(-1,-1);
            o_Quit = match.Groups["Quit"].Success;
            if (!o_Quit)
            {
                result = new Point(int.Parse(match.Groups["Row"].Value) - 1, match.Groups["Column"].Value.ToUpper()[0] - 'A');
            }
            return result;
        }

        private void PrintBoard(bool?[,] board)
        {
            printBoardColumnsHeader(board);
            printBoardLineSeperator(board);
            for (int i=0;i<board.GetLength(0); i++)
            {
                printBoardRow(board, i);
                printBoardLineSeperator(board);
            }
        }

        private void printBoardRow(bool?[,] board, int i)
        {
            Console.Write("{0} | ", i); // row number
            for (int j = 0; j < board.GetLength(1); j++)
            {
                Console.Write("{0} | ", board[i, j] == null ? " " : board[i, j].GetValueOrDefault() ? "o" : "x");
            }
            Console.WriteLine();
        }

        private void printBoardColumnsHeader(bool?[,] board)
        {
            Console.Write(" ");
            for (int i = 0; i < board.GetLength(1); i++)
            {
                Console.Write("   {0}", (char)('A' + i)); //column letter
            }
            Console.WriteLine();
        }

        private void printBoardLineSeperator(bool?[,] board)
        {
            Console.Write("  ");
            for (int i = 0; i < 4 * board.GetLength(0) + 1; i++)
            {
                Console.Write('=');
            }
            Console.WriteLine();
        }

        private int AskBoardSize()
        {
            int SizeOfBoard;

            do
            {
                Console.Write("Please write the size of the board you want (6 or 8):");
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
