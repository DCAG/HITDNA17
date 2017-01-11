using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ex02_Othelo
{
    enum DiscColor
    {
        None = 0,
        Black = 1,
        White = 2
    }
    class GameBoard
    {
        private const bool v_White = true; //Black = false
        static public bool White { get { return v_White; } }
        static public bool Black { get { return !v_White; } }
        private DiscColor[,] m_GridBoard;
        //private Dictionary<bool,List<Point>> m_AvailableMoves;
        private List<Point> m_AvailableMoves = new List<Point>();
        private Dictionary<Point, List<Point>> m_RequiredFlips = new Dictionary<Point, List<Point>>();

        private Dictionary<bool,int> m_DiscsCounter;

        public int BlackDiscsCounter { get { return GetDiscsCounter(Black); } }
        public int WhiteDiscsCounter { get { return GetDiscsCounter(White); } }

        public int GetDiscsCounter(DiscColor i_DiscsColor)
        {
            return m_DiscsCounter[i_DiscsColor];
        }

        private bool m_Turn = White;
        public bool ThisTurn
        {
            get
            {
                return m_Turn;
            }
        }

        public bool?[,] Board
        {
            get
            {
                return m_GridBoard;
            }
        }

        public GameBoard(int i_Size)
        {
            if ((2 < i_Size) && (i_Size % 2 == 0) && (i_Size == 6 || i_Size == 8))
            {
                m_GridBoard = new bool?[i_Size, i_Size];
            }
            else
            {
                throw new Exception("Illegal GameBoard size");
            }
            m_AvailableMoves = new List<Point>();
            m_RequiredFlips = new Dictionary<Point, List<Point>>();
            m_DiscsCounter = new Dictionary<bool, int>();
            m_DiscsCounter[Black] = 0;
            m_DiscsCounter[White] = 0;
        }

        private void updateAvailableMoves()
        {
            /* inspired by:
             * https://user.xmission.com/~sgigo/elec/ocreversi/legalmoves.html
             */

            for (int i = 0; i < m_GridBoard.GetLength(0); i++)
            {
                for (int j = 0; j < m_GridBoard.GetLength(1); j++)
                {
                    tryMove(new Point(i, j));   
                }
            }
        }

        private void tryMove(Point i_Square)
        {
            const int k_Up = -1, k_Left = -1, k_Down = 1, k_Right = 1, k_Stay = 0;
            /*
            Point UpLeft  = new Point(-1,-1),
                Left      = new Point(0, -1),
                DownLeft  = new Point(1, -1),
                Down      = new Point(1,  0),
                DownRight = new Point(1,  1),
                Right     = new Point(0,  1),
                UpRight   = new Point(-1, 1),
                Up        = new Point(-1, 0);
            */
            List<Point> toFlip = new List<Point>();
            toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, new Point(k_Up, k_Left)));   //North-West 
            toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, new Point(k_Stay, k_Left))); //|| //North
            toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, new Point(k_Down, k_Left))); //|| //North-East 
            toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, new Point(k_Down, k_Stay))); //|| //East 
            toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, new Point(k_Down, k_Right)));//|| //South-East
            toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, new Point(k_Stay, k_Right)));//|| //South
            toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, new Point(k_Up,   k_Right)));//|| //South-West
            toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, new Point(k_Up,   k_Stay))); //West

            if(toFlip.Count > 0)
            {
                m_AvailableMoves.Add(i_Square);
                m_RequiredFlips[i_Square] = toFlip;
            }
        }

        private List<Point> GetDiscsToFlipInDirection(Point i_Square, Point i_Delta)
        {
            List<Point> toFlip = new List<Point>();

            do
            {
                i_Square.Add(i_Delta);

                if (IsOutOfBounds(i_Square) || isEmptySquare(i_Square))
                {
                    toFlip.Clear();
                    break;
                }
                else if(m_GridBoard[i_Square.X, i_Square.Y] == !m_Turn)
                {
                    toFlip.Add(i_Square);
                }
            }
            while (m_GridBoard[i_Square.X, i_Square.Y] != m_Turn);

            return toFlip;
        }

        private bool isEmptySquare(Point i_Square)
        {
            return m_GridBoard[i_Square.X, i_Square.Y] == null;
        }

        public bool IsOutOfBounds(Point i_Square)
        {
            return i_Square.X < 0 || m_GridBoard.GetLength(0) <= i_Square.X ||
                   i_Square.Y < 0 || m_GridBoard.GetLength(1) <= i_Square.Y;
        }

        public bool HasMoves()
        {
            return m_AvailableMoves.Count > 0;
        }
        
        public void SetInitialBoard()
        {
            for (int i = 0; i < m_GridBoard.GetLength(0); i++)
            {
                for (int j = 0; j < m_GridBoard.GetLength(1); j++)
                {
                    m_GridBoard[i, j] = null;
                }
            }

            int boardCenterPosition = m_GridBoard.GetLength(0) / 2;

            m_GridBoard[boardCenterPosition - 1, boardCenterPosition - 1] =  v_White; //up,left
            m_GridBoard[boardCenterPosition - 1, boardCenterPosition    ] = !v_White; //left
            m_GridBoard[boardCenterPosition    , boardCenterPosition    ] =  v_White; //base square
            m_GridBoard[boardCenterPosition    , boardCenterPosition - 1] = !v_White; //up

            updateAvailableMoves();
        }

        public void UpdateBoard(Point i_Square)
        {
            m_GridBoard[i_Square.X, i_Square.Y] = m_Turn;
            m_DiscsCounter[ThisTurn]++;
            foreach (Point toFlip in m_RequiredFlips[i_Square])
            {
                m_GridBoard[toFlip.X, toFlip.Y] = m_Turn;
                m_DiscsCounter[ThisTurn]++;
                m_DiscsCounter[!ThisTurn]--;
            }

            m_Turn = !m_Turn;
            updateAvailableMoves();
        }

        /*
        public void UpdateBoard(Point i_Square, bool i_WhitePlayer)
        {
            const int k_Up = -1, k_Left = -1, k_Down = 1, k_Right = 1, k_Stay = 0;
            if (IsValidMove(i_Square, i_WhitePlayer))
            {
                m_GridBoard[i_Square.X, i_Square.Y] = i_WhitePlayer;
                flipInDirection(i_Square, new Point(k_Up, k_Left),    i_WhitePlayer); //North-West 
                flipInDirection(i_Square, new Point(k_Stay, k_Left),  i_WhitePlayer); //North
                flipInDirection(i_Square, new Point(k_Down, k_Left),  i_WhitePlayer); //North-East 
                flipInDirection(i_Square, new Point(k_Down, k_Stay),  i_WhitePlayer); //East 
                flipInDirection(i_Square, new Point(k_Down, k_Right), i_WhitePlayer); //South-East
                flipInDirection(i_Square, new Point(k_Stay, k_Right), i_WhitePlayer); //South
                flipInDirection(i_Square, new Point(k_Up, k_Right),   i_WhitePlayer); //South-West
                flipInDirection(i_Square, new Point(k_Up, k_Stay),    i_WhitePlayer); //West

                updateAvailableMoves();
            }
        }
        */

        public bool IsValidMove(Point i_Move)
        {
            return m_AvailableMoves.Contains(i_Move);
        }

        public Point GetRandomMove()
        {
            Random random = new Random();
            return m_AvailableMoves[random.Next(m_AvailableMoves.Count)];
        }
    }
}
