using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    class GameBoard
    {
        struct Square
        {
            byte m_column;
            byte m_row;
            static Square Parse(string i_str)
            {
                return new Square() { m_column = (byte)i_str[0], m_row = (byte)i_str[1] };
            }
        }
        private byte m_Size;
        private Coin[,] m_Coin;

        GameBoard(byte i_Size)
        {
            m_Size = i_Size;
            m_Coin = new Coin[m_Size,m_Size];
        }

        void SetSquare(byte i_Column, byte i_Row, CoinSideColor i_CoinSide)
        {
            m_Coin[i_Column, i_Row].Flip();// i_CoinSide;
        }

        public void SetBoard()
        {

        }
    }
}
