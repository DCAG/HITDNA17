﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ex02_Othelo
{
    class GameBoard
    {
        private const bool v_White = true; //Black = false
        private bool?[,] m_GridBoard;
        private List<Point> m_BlackAvailableMoves;
        private List<Point> m_WhiteAvailableMoves;

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
        }

        private List<Point> getAvailableMoves(bool i_WhitePlayer)
        {
            /* inspired by:
             * https://user.xmission.com/~sgigo/elec/ocreversi/legalmoves.html
             */
            List<Point> availableMoves = new List<Point>();
            for (int i = 0; i < m_GridBoard.GetLength(0); i++)
            {
                for (int j = 0; i < m_GridBoard.GetLength(1); j++)
                {
                    if(isLegalMove(new Point(i, j), i_WhitePlayer))
                    {
                        availableMoves.Add(new Point(i,j));
                    }
                }
            }
            return availableMoves;
        }

        private string ParseCoordinatesToStr(int i_Row, int i_Column)
        {
            return string.Format("{1}{0}", i_Row, (char)('A' + i_Column));
        }

        private bool isLegalMove(Point i_Square, bool i_WhitePlayer)
        {
            const int k_Up = -1, k_Left = -1, k_Down = 1, k_Right = 1, k_Stay = 0;
            Point UpLeft  = new Point(-1,-1),
                Left      = new Point(0, -1),
                DownLeft  = new Point(1, -1),
                Down      = new Point(1,  0),
                DownRight = new Point(1,  1),
                Right     = new Point(0,  1),
                UpRight   = new Point(-1, 1),
                Up        = new Point(-1, 0);

            return TestMoveDirection(i_Square, k_Up,   k_Left,  i_WhitePlayer) || //North-West 
                   TestMoveDirection(i_Square, k_Stay, k_Left,  i_WhitePlayer) || //North
                   TestMoveDirection(i_Square, k_Down, k_Left,  i_WhitePlayer) || //North-East 
                   TestMoveDirection(i_Square, k_Down, k_Stay,  i_WhitePlayer) || //East 
                   TestMoveDirection(i_Square, k_Down, k_Right, i_WhitePlayer) || //South-East
                   TestMoveDirection(i_Square, k_Stay, k_Right, i_WhitePlayer) || //South
                   TestMoveDirection(i_Square, k_Up,   k_Right, i_WhitePlayer) || //South-West
                   TestMoveDirection(i_Square, k_Up,   k_Stay,  i_WhitePlayer);   //West
        }

        private bool TestMoveDirection(Point i_Square, int i_DeltaX, int i_DeltaY, bool i_WhitePlayer)
        {
            bool validDirection = true;
            int numFlipped = 0;
            do
            {
                i_Square.X += i_DeltaX;
                i_Square.Y += i_DeltaY;

                if (isOutOfBounds(i_Square) || isEmptySquare(i_Square))
                {
                    validDirection = false;
                    break;
                }
                else if(m_GridBoard[i_Square.X, i_Square.Y] == !i_WhitePlayer)
                {
                    numFlipped++;
                }
            }
            while (m_GridBoard[i_Square.X, i_Square.Y] != i_WhitePlayer);

            validDirection = validDirection && numFlipped > 0;

            return validDirection;
        }

        private bool isEmptySquare(Point i_Square)
        {
            return m_GridBoard[i_Square.X, i_Square.Y] == null;
        }

        private bool isOutOfBounds(Point i_Square)
        {
            return i_Square.X < 0 || m_GridBoard.GetLength(0) < i_Square.X ||
                   i_Square.Y < 0 || m_GridBoard.GetLength(1) < i_Square.Y;
        }

        public bool HasMoves(bool i_WhiteDisc)
        {
            return getAvailableMoves(i_WhiteDisc).Count > 0;
        }

        public void SetInitialBoard()
        {
            for (int i = 0; i < m_GridBoard.GetLength(0); i++)
            {
                for (int j = 0; i < m_GridBoard.GetLength(1); j++)
                {
                    m_GridBoard[i, j] = null;
                }
            }

            int boardCenterPosition = m_GridBoard.GetLength(0) / 2;

            m_GridBoard[boardCenterPosition - 1, boardCenterPosition - 1] =  v_White; //up,left
            m_GridBoard[boardCenterPosition - 1, boardCenterPosition    ] = !v_White; //left
            m_GridBoard[boardCenterPosition    , boardCenterPosition    ] =  v_White; //base square
            m_GridBoard[boardCenterPosition    , boardCenterPosition - 1] = !v_White; //up
        }

        public void UpdateBoard(Point i_Square, bool i_WhitePlayer)
        {
            const int k_Up = -1, k_Left = -1, k_Down = 1, k_Right = 1, k_Stay = 0;

            flipInDirection(i_Square, k_Up,   k_Left,  i_WhitePlayer); //North-West 
            flipInDirection(i_Square, k_Stay, k_Left,  i_WhitePlayer); //North
            flipInDirection(i_Square, k_Down, k_Left,  i_WhitePlayer); //North-East 
            flipInDirection(i_Square, k_Down, k_Stay,  i_WhitePlayer); //East 
            flipInDirection(i_Square, k_Down, k_Right, i_WhitePlayer); //South-East
            flipInDirection(i_Square, k_Stay, k_Right, i_WhitePlayer); //South
            flipInDirection(i_Square, k_Up,   k_Right, i_WhitePlayer); //South-West
            flipInDirection(i_Square, k_Up,   k_Stay,  i_WhitePlayer); //West

            m_BlackAvailableMoves = getAvailableMoves(!v_White);
            m_WhiteAvailableMoves = getAvailableMoves(v_White);
        }
        /*
        private bool tryFlipInDirection(int i_X, int i_Y, int i_DeltaX, int i_DeltaY, bool i_WhitePlayer)
        {
            bool success = false;
            if (TestMoveDirection(i_X, i_Y, i_DeltaX, i_DeltaY, i_WhitePlayer))
            {
                flipInDirection(i_X, i_Y, i_DeltaX, i_DeltaY, i_WhitePlayer); //West
                success = true;
            }

            return success;
        }
        */
        private void flipInDirection(Point i_Square, int i_DeltaX, int i_DeltaY, bool i_WhitePlayer)
        {
            i_Square.X += i_DeltaX;
            i_Square.Y += i_DeltaY;
            while (m_GridBoard[i_Square.X, i_Square.Y] != i_WhitePlayer)
            {
                m_GridBoard[i_Square.X, i_Square.Y] = i_WhitePlayer; //flip
                i_Square.X += i_DeltaX;
                i_Square.Y += i_DeltaY;
            }
        }

        public Point GetRandomMove(bool i_White)
        {
            Point result;
            Random random = new Random();
            if (i_White)
            {
                result = m_WhiteAvailableMoves[random.Next(m_WhiteAvailableMoves.Count)];
            }
            else
            {
                result = m_BlackAvailableMoves[random.Next(m_BlackAvailableMoves.Count)];
            }
            return result;
        }
    }
}
