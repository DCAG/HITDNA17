using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ex02_Othelo
{
    enum DiscColor
    {
        None = -1,
        Black = 0,
        White = 1,
    }
    class GameBoard
    {
        private DiscColor[,] m_GridBoard;
        public DiscColor[,] Board
        {
            get
            {
                return m_GridBoard;
            }
        }

        private List<Point> m_AvailableMoves = new List<Point>();
        private Dictionary<Point, List<Point>> m_RequiredFlips = new Dictionary<Point, List<Point>>();

        private const int k_NumOfColors = 2;
        private int[] m_DiscsCounter;

        private DiscColor m_Turn = DiscColor.White;
        public DiscColor ThisTurn
        {
            get
            {
                return m_Turn;
            }
        }

        public GameBoard(int i_Size)
        {
            if ((2 < i_Size) && (i_Size % 2 == 0))
            {
                m_GridBoard = new DiscColor[i_Size, i_Size];
            }
            else
            {
                throw new Exception("Illegal GameBoard size");
            }

            m_AvailableMoves = new List<Point>();
            m_RequiredFlips  = new Dictionary<Point, List<Point>>();
            m_DiscsCounter   = new int[k_NumOfColors];
        }

        public int GetDiscsCounter(DiscColor i_DiscsColor)
        {
            return m_DiscsCounter[(int)i_DiscsColor];
        }
        static public DiscColor GetOppositeDiscColor(DiscColor i_DiscColor)
        {
            DiscColor result = DiscColor.None;
            if (i_DiscColor == DiscColor.White)
            {
                result = DiscColor.Black;
            }
            else if(i_DiscColor == DiscColor.Black)
            {
                result = DiscColor.White;
            }

            return result;
        }

        private void updateAvailableMoves()
        {
            m_AvailableMoves.Clear();
            m_RequiredFlips.Clear();
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
            if (isEmptySquare(i_Square))
            {
                List<Point> toFlip = new List<Point>();
                toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, getPointDelta(Delta.Down)));
                toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, getPointDelta(Delta.DownLeft)));
                toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, getPointDelta(Delta.DownRight)));
                toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, getPointDelta(Delta.Left)));
                toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, getPointDelta(Delta.Right)));
                toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, getPointDelta(Delta.Up)));
                toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, getPointDelta(Delta.UpLeft)));
                toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, getPointDelta(Delta.UpRight)));

                if (toFlip.Count > 0)
                {
                    m_AvailableMoves.Add(i_Square);
                    m_RequiredFlips[i_Square] = toFlip;
                }
            }
        }

        private List<Point> GetDiscsToFlipInDirection(Point i_Square, Point i_Delta)
        {
            List<Point> toFlip = new List<Point>();

            do
            {
                i_Square.Add(i_Delta);

                if (isOutOfBounds(i_Square) || isEmptySquare(i_Square))
                {
                    toFlip.Clear();
                    break;
                }
                else if(m_GridBoard[i_Square.X, i_Square.Y] == GetOppositeDiscColor(m_Turn))
                {
                    toFlip.Add(i_Square);
                }
            }
            while (m_GridBoard[i_Square.X, i_Square.Y] != m_Turn);

            return toFlip;
        }

        public void SetInitialBoard(DiscColor i_FirstTurn)
        {
            for (int i = 0; i < m_GridBoard.GetLength(0); i++)
            {
                for (int j = 0; j < m_GridBoard.GetLength(1); j++)
                {
                    m_GridBoard[i, j] = DiscColor.None;
                }
            }

            int boardCenterPosition = m_GridBoard.GetLength(0) / 2;

            m_GridBoard[boardCenterPosition - 1, boardCenterPosition - 1] = DiscColor.White; //up,left
            m_GridBoard[boardCenterPosition - 1, boardCenterPosition    ] = DiscColor.Black; //left
            m_GridBoard[boardCenterPosition    , boardCenterPosition    ] = DiscColor.White; //base square
            m_GridBoard[boardCenterPosition    , boardCenterPosition - 1] = DiscColor.Black; //up

            m_DiscsCounter[(int)DiscColor.Black] = 2;
            m_DiscsCounter[(int)DiscColor.White] = 2;

            m_Turn = i_FirstTurn;
            updateAvailableMoves();
        }

        public void UpdateBoard(Point i_Square)
        {
            m_GridBoard[i_Square.X, i_Square.Y] = m_Turn;
            m_DiscsCounter[(int)ThisTurn]++;
            foreach (Point toFlip in m_RequiredFlips[i_Square])
            {
                m_GridBoard[toFlip.X, toFlip.Y] = m_Turn;
                m_DiscsCounter[(int)ThisTurn]++;
                m_DiscsCounter[(int)GetOppositeDiscColor(ThisTurn)]--;
            }
        }
        public void SwitchTurns()
        {
            m_Turn = GetOppositeDiscColor(m_Turn);
            updateAvailableMoves();
        }
        public bool IsValidMove(Point i_Move)
        {
            return m_AvailableMoves.Contains(i_Move);
        }

        public Point GetRandomMove()
        {
            Random random = new Random();
            return m_AvailableMoves[random.Next(m_AvailableMoves.Count)];
        }

        #region Private Boolean Tests
        private bool isEmptySquare(Point i_Square)
        {
            return m_GridBoard[i_Square.X, i_Square.Y] == DiscColor.None;
        }

        private bool isOutOfBounds(Point i_Square)
        {
            return i_Square.X < 0 || m_GridBoard.GetLength(0) <= i_Square.X ||
                   i_Square.Y < 0 || m_GridBoard.GetLength(1) <= i_Square.Y;
        }

        public bool HasMoves()
        {
            return m_AvailableMoves.Count > 0;
        }
        #endregion

        private Point getPointDelta(Delta i_Delta)
        {
            const int k_Up = -1, k_Left = -1, k_Down = 1, k_Right = 1, k_Stay = 0;

            Point desiredDeltaPoint = new Point();
            switch (i_Delta)
            {
                case Delta.Down:
                    desiredDeltaPoint = new Point(k_Stay, k_Down);
                    break;
                case Delta.DownLeft:
                    desiredDeltaPoint = new Point(k_Left, k_Down);
                    break;
                case Delta.DownRight:
                    desiredDeltaPoint = new Point(k_Right, k_Down);
                    break;
                case Delta.Left:
                    desiredDeltaPoint = new Point(k_Left, k_Stay);
                    break;
                case Delta.Right:
                    desiredDeltaPoint = new Point(k_Right, k_Stay);
                    break;
                case Delta.Up:
                    desiredDeltaPoint = new Point(k_Stay, k_Up);
                    break;
                case Delta.UpLeft:
                    desiredDeltaPoint = new Point(k_Left, k_Up);
                    break;
                case Delta.UpRight:
                    desiredDeltaPoint = new Point(k_Right, k_Up);
                    break;
            }

            return desiredDeltaPoint;
        }
    }
    enum Delta
    {
        UpLeft,
        Left,
        DownLeft,
        Down,
        DownRight,
        Right,
        UpRight,
        Up
    }
}