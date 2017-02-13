using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Ex02_Othelo
{
    public class GameManager
    { 
        private GameService m_GameService;
        private Player m_FirstPlayer, m_Opponent;
        private bool m_Quit;

        public static char GetSymbol(eDiscColor i_DiscColor)
        {
            const char k_BlackSymbol = 'x';
            const char k_EmptySymbol = ' ';
            const char k_WhiteSymbol = 'o';

            return i_DiscColor == eDiscColor.None ? k_EmptySymbol : i_DiscColor == eDiscColor.White ? k_WhiteSymbol : k_BlackSymbol;
        }

        public GameManager()
        {
            const bool v_ComputerPlayer = true;
            const string k_ComputerPlayerName = "Computer";

            m_FirstPlayer = new Player(readPlayerName(), eDiscColor.White, !v_ComputerPlayer);

            if (askYesNoQuestion("Is your opponent human? (Enter 'n' to play against the computer)"))
            {
                m_Opponent = new Player(readPlayerName(), eDiscColor.Black, !v_ComputerPlayer);
            }
            else
            {
                m_Opponent = new Player(k_ComputerPlayerName, eDiscColor.Black, v_ComputerPlayer);
            }

            m_GameService = new GameService(readBoardSize());

            m_Quit = false;
        }

        public void Start()
        {
            do
            {
                m_GameService.SetInitialBoard(m_FirstPlayer.Color);
                playRound();
            }
            while (!m_Quit && askYesNoQuestion("Play another round?"));
        }

        private void playRound()
        {
            printBoard();
            bool firstPlayerMovedInLastTurn = true;
            bool opponentPlayerMovedInLastTurn = true;

            do
            {
                firstPlayerMovedInLastTurn = tryPlayTurn(m_FirstPlayer);
                if (m_Quit || (!firstPlayerMovedInLastTurn && !opponentPlayerMovedInLastTurn))
                {
                    break;
                }

                m_GameService.SwitchTurns();
                opponentPlayerMovedInLastTurn = tryPlayTurn(m_Opponent);
                if (m_Quit)
                {
                    break;
                }

                m_GameService.SwitchTurns();
            }
            while (!m_Quit && (firstPlayerMovedInLastTurn || opponentPlayerMovedInLastTurn));

            if (!m_Quit)
            {
                printHighscore();
            }
        }

        private bool tryPlayTurn(Player i_Player)
        {
            bool hasMoves = m_GameService.HasMoves();

            if (hasMoves)
            {
                OtheloPoint move = getValidPlayerMove(i_Player);
                if (!m_Quit)
                {
                    m_GameService.UpdateBoard(move);
                    printBoard();
                }
            }
            else
            {
                Console.WriteLine("{0} has no available moves", i_Player.Name);
            }

            return hasMoves;
        }

        private OtheloPoint getValidPlayerMove(Player i_Player)
        {
            OtheloPoint move;
            if (i_Player.IsComputer)
            {
                move = m_GameService.GetRandomMove();
            }
            else
            {
                Console.Write("It is {1}'s turn [{0}], choose a square or Q to exit (e.g. A5):", GetSymbol(i_Player.Color), i_Player.Name);
                move = readPlayerMoveOrQuit();

                while (!m_Quit && !m_GameService.IsValidMove(move))
                {
                    Console.WriteLine("Impossible move! try again...");
                    move = readPlayerMoveOrQuit();
                }
            }

            return move;
        }

        #region Printing functions
        private void printHighscore()
        {
            int firstPlayerScore = m_GameService.GetDiscsCounter(m_FirstPlayer.Color);
            int opponentScore = m_GameService.GetDiscsCounter(m_Opponent.Color);

            StringBuilder gameFinalResult = new StringBuilder();
            gameFinalResult.AppendFormat("{0} has {1} discs", m_FirstPlayer.Name, firstPlayerScore);
            gameFinalResult.AppendFormat("{0}{1} has {2} discs", Environment.NewLine, m_Opponent.Name, opponentScore);

            if (firstPlayerScore == opponentScore)
            {
                gameFinalResult.Append("Its a tie!");
            }
            else
            {
                gameFinalResult.AppendFormat("{0}{1} is the winner", Environment.NewLine, firstPlayerScore > opponentScore ? m_FirstPlayer.Name : m_Opponent.Name);
            }

            Console.WriteLine(gameFinalResult.ToString());
        }

        private void printBoard()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            printBoardColumnsHeader();
            printBoardLineSeperator();
            for (int i = 0; i < m_GameService.Board.GetLength(0); i++)
            {
                printBoardRow(i);
                printBoardLineSeperator();
            }
        }

        private void printBoardRow(int i_RowIndex)
        {
            Console.Write("{0} | ", i_RowIndex + 1);
            for (int j = 0; j < m_GameService.Board.GetLength(1); j++)
            {
                Console.Write("{0} | ", GetSymbol(m_GameService.Board[i_RowIndex, j]));
            }

            Console.WriteLine();
        }

        private void printBoardColumnsHeader()
        {
            Console.Write(" ");
            for (int i = 0; i < m_GameService.Board.GetLength(1); i++)
            {
                Console.Write("   {0}", (char)('A' + i));
            }

            Console.WriteLine();
        }

        private void printBoardLineSeperator()
        {
            Console.Write("  ");
            for (int i = 0; i < (4 * m_GameService.Board.GetLength(0)) + 1; i++)
            {
                Console.Write('=');
            }

            Console.WriteLine();
        }
        #endregion

        #region Read Input From User Functions
        private OtheloPoint readPlayerMoveOrQuit()
        {
            Regex moveOrQuitRegexPattern = new Regex("^((?<Column>[A-Za-z]{1})(?<Row>[1-9]{1})|(?<Quit>Q|q))$");
            Match match = moveOrQuitRegexPattern.Match(Console.ReadLine());

            while (!match.Success)
            {
                Console.WriteLine("Invalid input!");
                match = moveOrQuitRegexPattern.Match(Console.ReadLine());
            }

            OtheloPoint move = new OtheloPoint(-1, -1);

            m_Quit = match.Groups["Quit"].Success;
            if (!m_Quit)
            {
                move = new OtheloPoint(int.Parse(match.Groups["Row"].Value) - 1, match.Groups["Column"].Value.ToUpper()[0] - 'A');
            }

            return move;
        }

        private bool askYesNoQuestion(string i_Question)
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

        private int readBoardSize()
        {
            const int k_SizeOption0 = 6;
            const int k_SizeOption1 = 8;
            int SizeOfBoard;
            
            do
            {
                Console.Write("Please write the size of the board you want ({0} or {1}):", k_SizeOption0, k_SizeOption1);
            }
            while (!int.TryParse(Console.ReadLine(), out SizeOfBoard) || !(SizeOfBoard == k_SizeOption0 || SizeOfBoard == k_SizeOption1));

            return SizeOfBoard;
        }

        private string readPlayerName()
        {
            string name_Player;

            Console.WriteLine("Please enter your name");
            name_Player = Console.ReadLine();

            return name_Player;
        }
        #endregion
    }
}
