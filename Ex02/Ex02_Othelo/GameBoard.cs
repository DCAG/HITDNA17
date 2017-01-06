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

        private string[] GetAvailableMoves(char i_char)
        {
            
        }

        internal bool HasMoves(Player i_Player)
        {
            return GetAvailableMoves(i_Player.Symbol).Length > 0;
        }

        internal void SetInitialBoard()
        {
            throw new NotImplementedException();
        }

        internal bool UpdateBoard(string move)
        {
            throw new NotImplementedException();
        }
    }
}
