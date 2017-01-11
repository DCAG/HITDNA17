﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ex02_Othelo
{
    public enum eDiscColor
    {
        None = -1,
        Black = 0,
        White = 1,
    }

    internal enum eDirection
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

    public class GameBoard
    {
        private eDiscColor[,] m_GridBoard;

        public eDiscColor[,] Board
        {
            get
            {
                return m_GridBoard;
            }
        }

        private List<Point> m_AvailableMoves = new List<Point>();
        private Dictionary<Point, List<Point>> m_RequiredFlips = new Dictionary<Point, List<Point>>();
        private int[] m_DiscsCounter;
        private eDiscColor m_Turn = eDiscColor.White;

        public eDiscColor ThisTurn
        {
            get
            {
                return m_Turn;
            }
        }

        public static eDiscColor GetOppositeDiscColor(eDiscColor i_DiscColor)
        {
            eDiscColor result = eDiscColor.None;
            if (i_DiscColor == eDiscColor.White)
            {
                result = eDiscColor.Black;
            }
            else if (i_DiscColor == eDiscColor.Black)
            {
                result = eDiscColor.White;
            }

            return result;
        }

        public GameBoard(int i_Size)
        {
            const int k_NumOfColors = 2;

            if ((i_Size > 2) && (i_Size % 2 == 0))
            {
                m_GridBoard = new eDiscColor[i_Size, i_Size];
            }
            else
            {
                throw new Exception("Illegal GameBoard size");
            }

            m_AvailableMoves = new List<Point>();
            m_RequiredFlips  = new Dictionary<Point, List<Point>>();
            m_DiscsCounter   = new int[k_NumOfColors];
        }

        public int GetDiscsCounter(eDiscColor i_DiscsColor)
        {
            return m_DiscsCounter[(int)i_DiscsColor];
        }

        private void updateAvailableMoves()
        {
            m_AvailableMoves.Clear();
            m_RequiredFlips.Clear();
            for (int i = 0; i < m_GridBoard.GetLength(0); i++)
            {
                for (int j = 0; j < m_GridBoard.GetLength(1); j++)
                {
                    testAndAddAvailableMoves(new Point(i, j));
                }
            }
        }

        private void testAndAddAvailableMoves(Point i_Square)
        {
            if (isEmptySquare(i_Square))
            {
                List<Point> toFlip = new List<Point>();
                toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, getDirectionDeltaPoint(eDirection.Down)));
                toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, getDirectionDeltaPoint(eDirection.DownLeft)));
                toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, getDirectionDeltaPoint(eDirection.DownRight)));
                toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, getDirectionDeltaPoint(eDirection.Left)));
                toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, getDirectionDeltaPoint(eDirection.Right)));
                toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, getDirectionDeltaPoint(eDirection.Up)));
                toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, getDirectionDeltaPoint(eDirection.UpLeft)));
                toFlip.AddRange(GetDiscsToFlipInDirection(i_Square, getDirectionDeltaPoint(eDirection.UpRight)));

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

        public void SetInitialBoard(eDiscColor i_FirstTurn)
        {
            for (int i = 0; i < m_GridBoard.GetLength(0); i++)
            {
                for (int j = 0; j < m_GridBoard.GetLength(1); j++)
                {
                    m_GridBoard[i, j] = eDiscColor.None;
                }
            }

            int boardCenterPosition = m_GridBoard.GetLength(0) / 2;

            m_GridBoard[boardCenterPosition - 1, boardCenterPosition - 1] = eDiscColor.White; // up,left
            m_GridBoard[boardCenterPosition - 1, boardCenterPosition] = eDiscColor.Black; // left
            m_GridBoard[boardCenterPosition, boardCenterPosition] = eDiscColor.White; // base square
            m_GridBoard[boardCenterPosition, boardCenterPosition - 1] = eDiscColor.Black; // up

            m_DiscsCounter[(int)eDiscColor.Black] = 2;
            m_DiscsCounter[(int)eDiscColor.White] = 2;

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
            return m_GridBoard[i_Square.X, i_Square.Y] == eDiscColor.None;
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

        private Point getDirectionDeltaPoint(eDirection i_Direction)
        {
            const int k_Up = -1, k_Left = -1, k_Down = 1, k_Right = 1, k_Stay = 0;
            Point desiredDeltaPoint = new Point();

            switch (i_Direction)
            {
                case eDirection.Down:
                    desiredDeltaPoint = new Point(k_Stay, k_Down);
                    break;
                case eDirection.DownLeft:
                    desiredDeltaPoint = new Point(k_Left, k_Down);
                    break;
                case eDirection.DownRight:
                    desiredDeltaPoint = new Point(k_Right, k_Down);
                    break;
                case eDirection.Left:
                    desiredDeltaPoint = new Point(k_Left, k_Stay);
                    break;
                case eDirection.Right:
                    desiredDeltaPoint = new Point(k_Right, k_Stay);
                    break;
                case eDirection.Up:
                    desiredDeltaPoint = new Point(k_Stay, k_Up);
                    break;
                case eDirection.UpLeft:
                    desiredDeltaPoint = new Point(k_Left, k_Up);
                    break;
                case eDirection.UpRight:
                    desiredDeltaPoint = new Point(k_Right, k_Up);
                    break;
            }

            return desiredDeltaPoint;
        }
    }
}