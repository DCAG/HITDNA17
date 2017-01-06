using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    class GameBoard
    {
        char[,] m_Board;
        public char[,] Board
        {
            get { return m_Board; }
        }

        public GameBoard(int size)
        {
            m_Board = new char[size, size];
        }

        internal bool HasMoves(Player m_Player1)
        {
            throw new NotImplementedException();
        }

        internal void SetInitialBoard()
        {
            throw new NotImplementedException();
        }
    }
}
