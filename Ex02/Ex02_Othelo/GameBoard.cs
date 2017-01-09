using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ex02_Othelo
{
    class GameBoard
    {
        private const bool v_White = true; //Black = false
        private bool?[,] m_GridBoard;
        private List<string> m_BlackAvailableMoves;
        private List<string> m_WhiteAvailableMoves;

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

        private List<string> getAvailableMoves(bool i_WhitePlayer)
        {
            /* inspired by:
             * https://user.xmission.com/~sgigo/elec/ocreversi/legalmoves.html
             */
            List<string> availableMoves = new List<string>();
            for (int i = 0; i < m_GridBoard.GetLength(0); i++)
            {
                for (int j = 0; i < m_GridBoard.GetLength(1); j++)
                {
                    if(isLegalMove(i, j, i_WhitePlayer))
                    {
                        availableMoves.Add(ParseCoordinatesToStr(i,j));
                    }
                }
            }
            return availableMoves;
        }

        private string ParseCoordinatesToStr(int i_Row, int i_Column)
        {
            return string.Format("{1}{0}", i_Row, (char)('A' + i_Column));
        }

        private bool isLegalMove(int i_X, int i_Y, bool i_WhitePlayer)
        {
            const int k_Up = -1, k_Left = -1, k_Down = 1, k_Right = 1, k_Stay = 0;

            return TestMoveDirection(i_X, i_Y, k_Up,   k_Left,  i_WhitePlayer) || //North-West 
                   TestMoveDirection(i_X, i_Y, k_Stay, k_Left,  i_WhitePlayer) || //North
                   TestMoveDirection(i_X, i_Y, k_Down, k_Left,  i_WhitePlayer) || //North-East 
                   TestMoveDirection(i_X, i_Y, k_Down, k_Stay,  i_WhitePlayer) || //East 
                   TestMoveDirection(i_X, i_Y, k_Down, k_Right, i_WhitePlayer) || //South-East
                   TestMoveDirection(i_X, i_Y, k_Stay, k_Right, i_WhitePlayer) || //South
                   TestMoveDirection(i_X, i_Y, k_Up,   k_Right, i_WhitePlayer) || //South-West
                   TestMoveDirection(i_X, i_Y, k_Up,   k_Stay,  i_WhitePlayer);   //West
        }

        private bool TestMoveDirection(int i_X, int i_Y, int i_DeltaX, int i_DeltaY, bool i_WhitePlayer)
        {
            bool validDirection = true;
            int numFlipped = 0;
            do
            {
                i_X += i_DeltaX;
                i_Y += i_DeltaY;

                if (isOutOfBounds(i_X, i_Y) || isEmptySquare(i_X, i_Y))
                {
                    validDirection = false;
                    break;
                }
                else if(m_GridBoard[i_X, i_Y] == !i_WhitePlayer)
                {
                    numFlipped++;
                }
            }
            while (m_GridBoard[i_X, i_Y] != i_WhitePlayer);

            validDirection = validDirection && numFlipped > 0;

            return validDirection;
        }

        private bool isEmptySquare(int i_X,int i_Y)
        {
            return m_GridBoard[i_X, i_Y] == null;
        }

        private bool isOutOfBounds(int i_X,int i_Y)
        {
            return i_X < 0 || m_GridBoard.GetLength(0) < i_X ||
                   i_Y < 0 || m_GridBoard.GetLength(1) < i_Y;
        }

        public bool HasMoves(Player i_Player)
        {
            return getAvailableMoves(i_Player.WhiteDisc).Count > 0;
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

        public bool UpdateBoard(string i_Square, bool i_WhitePlayer)
        {
            int row, column;
            bool result = tryParseStrToCoordinates(i_Square, out row, out column) && UpdateBoard(row, column, i_WhitePlayer);

            m_BlackAvailableMoves = getAvailableMoves(!v_White);
            m_WhiteAvailableMoves = getAvailableMoves(v_White);

            return result;
        }

        public bool UpdateBoard(int i_X, int i_Y, bool i_WhitePlayer)
        {
            const int k_Up = -1, k_Left = -1, k_Down = 1, k_Right = 1, k_Stay = 0;

            return tryFlipInDirection(i_X, i_Y, k_Up,   k_Left,  i_WhitePlayer) || //North-West 
                   tryFlipInDirection(i_X, i_Y, k_Stay, k_Left,  i_WhitePlayer) || //North
                   tryFlipInDirection(i_X, i_Y, k_Down, k_Left,  i_WhitePlayer) || //North-East 
                   tryFlipInDirection(i_X, i_Y, k_Down, k_Stay,  i_WhitePlayer) || //East 
                   tryFlipInDirection(i_X, i_Y, k_Down, k_Right, i_WhitePlayer) || //South-East
                   tryFlipInDirection(i_X, i_Y, k_Stay, k_Right, i_WhitePlayer) || //South
                   tryFlipInDirection(i_X, i_Y, k_Up,   k_Right, i_WhitePlayer) || //South-West
                   tryFlipInDirection(i_X, i_Y, k_Up,   k_Stay,  i_WhitePlayer);   //West
        }

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

        private void flipInDirection(int i_X, int i_Y, int i_DeltaX, int i_DeltaY, bool i_WhitePlayer)
        {
            i_X += i_DeltaX;
            i_Y += i_DeltaY;
            while (m_GridBoard[i_X, i_Y] != i_WhitePlayer)
            {
                m_GridBoard[i_X, i_Y] = i_WhitePlayer; //flip
                i_X += i_DeltaX;
                i_Y += i_DeltaY;
            }
        }

        public string GetRandomMove(bool i_White)
        {
            Random random = new Random();

            return string.Format("{1}{0}", "12345678".ToCharArray().GetValue(random.Next()), "ABCDEFGH".ToCharArray().GetValue(random.Next()));
        }
    }
}
