using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    struct Player
    {
        private readonly bool r_IsComputer;
        private readonly DiscColor r_DiscColor;
        private readonly string m_Name;
        private const char k_WhiteSymbol = 'o';
        private const char k_BlackSymbol = 'x';

        public char Symbol
        {
            get
            {
                return GetSymbol(r_DiscColor); 
            }
        }

        public bool IsComputer
        {
            get
            {
                return r_IsComputer;
            }
        }

        public DiscColor Color
        {
            get
            {
                return r_DiscColor;
            }
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        public Player(string i_Name, DiscColor i_DiscColor, bool i_IsComputer)
        {
            r_IsComputer = i_IsComputer;
            r_DiscColor = i_DiscColor;
            m_Name = i_Name;
        }
        static public char GetSymbol(DiscColor i_DiscColor)
        {
            return i_DiscColor == DiscColor.None ? ' ' : i_DiscColor == DiscColor.White ? k_WhiteSymbol : k_BlackSymbol;
        }

    }
}
