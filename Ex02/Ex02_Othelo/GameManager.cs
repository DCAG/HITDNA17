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
            m_FirstPlayer = new Player(AskPlayerName(), GameBoard.White, !v_ComputerPlayer);

            if (AskYesNoQuestion("Is opponent player human? (computer is default)"))
            {
                m_Opponent = new Player(AskPlayerName(),GameBoard.Black,!v_ComputerPlayer);
            }
            else
            {
                m_Opponent = new Player(k_ComputerPlayerName, GameBoard.Black, v_ComputerPlayer);
            }

            m_GameBoard = new GameBoard(AskBoardSize());

            m_Quit = false;
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
            bool player1HasMoves = true;
            bool player2HasMoves = true;
            do
            {
                player1HasMoves = PlayerTurn(m_FirstPlayer);
                player2HasMoves = PlayerTurn(m_Opponent);
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
                PlayTurn(i_Player);
                if (!m_Quit)
                {
                    PrintBoard(m_GameBoard.Board);
                }
            }
            else
            {
                PrintNoMoves(i_Player);
            }

            return hasMoves;
        }

        private void PlayTurn(Player i_Player)
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
                move = AskPlayerMoveOrQuit(out m_Quit);
                while (!m_Quit && !m_GameBoard.IsValidMove(move))  //if illegal square was chosen try again //update board
                {
                    Console.WriteLine("Impossible move! try again...");
                    move = AskPlayerMoveOrQuit(out m_Quit);
                }
            }

            if (!m_Quit)
            {
                m_GameBoard.UpdateBoard(move);
            }
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

        private void PrintHighscore()
        {
            int player1Score = m_GameBoard.GetDiscsCounter(m_FirstPlayer.DiscColor);
            int player2Score = m_GameBoard.GetDiscsCounter(m_Opponent.DiscColor);
            string gameFinalResult;
            if(player1Score == player2Score)
            {
                gameFinalResult = "Its a tie!";
            }
            else if (player1Score > player2Score)
            {
                gameFinalResult = string.Format("{0} is the winner", m_FirstPlayer.Name);
            }
            else //if (player1Score < player2Score)
            {
                gameFinalResult = string.Format("{0} is the winner", m_Opponent.Name);
            }
                
            string resultsStr = @"
{0} has {1} discs
{2} has {3} discs
{4}
";
            Console.WriteLine(resultsStr, m_FirstPlayer.Name, player1Score, m_Opponent.Name, player2Score, gameFinalResult);
        }

        private void PrintNoMoves(Player i_Player)
        {
            Console.WriteLine("{0} has no available moves", i_Player.Name);
        }

        private Point AskPlayerMoveOrQuit(out bool o_Quit)
        {
            Regex regex = new Regex("^((?<Column>[A-Za-z]{1})(?<Row>[1-9]{1})|(?<Quit>Q|q))$");
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
            Console.Write("{0} | ", i + 1); // row number
            for (int j = 0; j < board.GetLength(1); j++)
            {
                Console.Write("{0} | ", board[i, j] == null ? ' ' : Player.GetSymbol(board[i, j].GetValueOrDefault()));
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
