using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ex02_Othelo
{
    public class GameManager
    {
        GameBoard m_GameBoard;
        Player m_FirstPlayer, m_Opponent;
        bool m_Quit;
        private const bool v_ComputerPlayer = true;
        private const string k_ComputerPlayerName = "Computer";

        public GameManager()
        {
            m_FirstPlayer = new Player(AskPlayerName(), DiscColor.White, !v_ComputerPlayer);

            if (AskYesNoQuestion("Is your opponent human? (Enter 'n' to play against the computer)"))
            {
                m_Opponent = new Player(AskPlayerName(), DiscColor.Black,!v_ComputerPlayer);
            }
            else
            {
                m_Opponent = new Player(k_ComputerPlayerName, DiscColor.Black, v_ComputerPlayer);
            }

            m_GameBoard = new GameBoard(AskBoardSize());

            m_Quit = false;
        }
        public void Start()
        {
            do
            {
                m_GameBoard.SetInitialBoard(m_FirstPlayer.Color);
                play();
            }
            while (!m_Quit && AskYesNoQuestion("Play another round?"));
        }
        private void play()
        {
            PrintBoard();
            bool player1HasMoves = true;
            bool player2HasMoves = true;
            do
            {
                player1HasMoves = PlayerTurn(m_FirstPlayer);
                if (m_Quit)
                {
                    break;
                }

                m_GameBoard.SwitchTurns();
                player2HasMoves = PlayerTurn(m_Opponent);
                if (m_Quit)
                {
                    break;
                }

                m_GameBoard.SwitchTurns();
            }
            while (!m_Quit && (player1HasMoves || player2HasMoves));
            if (!m_Quit)
                PrintHighscore();
        }
        private bool PlayerTurn(Player i_Player)
        {
            bool hasMoves = m_GameBoard.HasMoves();
            if (hasMoves)
            {
                MakeTurn(i_Player);
                if (!m_Quit)
                {
                    PrintBoard();
                }
            }
            else
            {
                PrintNoMoves(i_Player);
            }

            return hasMoves;
        }
        private void MakeTurn(Player i_Player)
        {
            string moveStr = string.Empty;
            Point move = new Point(-1, -1);
            if (i_Player.IsComputer)
            {
                move = m_GameBoard.GetRandomMove();
            }
            else
            {
                Console.Write("[{0}] It is {1}'s turn, choose a square or Q to exit:", i_Player.Symbol, i_Player.Name);
                move = ReadPlayerMoveOrQuit();
                while (!m_Quit && !m_GameBoard.IsValidMove(move))  //if illegal square was chosen try again //update board
                {
                    Console.WriteLine("Impossible move! try again...");
                    move = ReadPlayerMoveOrQuit();
                }
            }

            if (!m_Quit)
            {
                m_GameBoard.UpdateBoard(move);
            }
        }

        #region Printing functions
        private void PrintHighscore()
        {
            int firstPlayerScore = m_GameBoard.GetDiscsCounter(m_FirstPlayer.Color);
            int opponentScore = m_GameBoard.GetDiscsCounter(m_Opponent.Color);

            StringBuilder gameFinalResult = new StringBuilder();
            gameFinalResult.AppendFormat("{0} has {1} discs", m_FirstPlayer.Name, firstPlayerScore);
            gameFinalResult.AppendFormat("{0}{1} has {2} discs", Environment.NewLine, m_Opponent.Name, opponentScore);

            if (firstPlayerScore == opponentScore)
            {
                gameFinalResult.Append("Its a tie!");
            }
            else
            {
                gameFinalResult.AppendFormat("{0}{1} is the winner",Environment.NewLine, firstPlayerScore > opponentScore ? m_FirstPlayer.Name : m_Opponent.Name);
            }

            Console.WriteLine(gameFinalResult.ToString());
        }
        private void PrintNoMoves(Player i_Player)
        {
            Console.WriteLine("{0} has no available moves", i_Player.Name);
        }
        private void PrintBoard()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            printBoardColumnsHeader();
            printBoardLineSeperator();
            for (int i=0;i< m_GameBoard.Board.GetLength(0); i++)
            {
                printBoardRow(i);
                printBoardLineSeperator();
            }
        }
        private void printBoardRow(int i_RowIndex)
        {
            Console.Write("{0} | ", i_RowIndex + 1); // row number
            for (int j = 0; j < m_GameBoard.Board.GetLength(1); j++)
            {
                Console.Write("{0} | ", Player.GetSymbol(m_GameBoard.Board[i_RowIndex, j]));
            }
            Console.WriteLine();
        }
        private void printBoardColumnsHeader()
        {
            Console.Write(" ");
            for (int i = 0; i < m_GameBoard.Board.GetLength(1); i++)
            {
                Console.Write("   {0}", (char)('A' + i)); //column letter
            }
            Console.WriteLine();
        }
        private void printBoardLineSeperator()
        {
            Console.Write("  ");
            for (int i = 0; i < 4 * m_GameBoard.Board.GetLength(0) + 1; i++)
            {
                Console.Write('=');
            }
            Console.WriteLine();
        }
        #endregion

        #region Questions Functions
        private Point ReadPlayerMoveOrQuit()
        {
            Regex regex = new Regex("^((?<Column>[A-Za-z]{1})(?<Row>[1-9]{1})|(?<Quit>Q|q))$");
            Match match = regex.Match(Console.ReadLine());
            while (!match.Success)
            {
                Console.WriteLine("Invalid input!");
                match = regex.Match(Console.ReadLine());
            }

            Point result = new Point(-1, -1);
            m_Quit = match.Groups["Quit"].Success;
            if (!m_Quit)
            {
                result = new Point(int.Parse(match.Groups["Row"].Value) - 1, match.Groups["Column"].Value.ToUpper()[0] - 'A');
            }
            return result;
        }
        private bool AskYesNoQuestion(string i_Question)
        {
            Console.Write("{0} [y/n]: ", i_Question);
            string answerStr = Console.ReadLine();
            while (!Regex.IsMatch(answerStr, "^(y|Y|n|N)$"))
            {
                Console.Write("{0} [y/n]: ", i_Question);
                answerStr = Console.ReadLine();
            }
            return Regex.IsMatch(answerStr, "^(y|Y)$");
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
        #endregion
    }
}
