using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    struct Player
    {
        private readonly bool r_IsComputer;
        private readonly bool r_IsWhiteDisc;
        private readonly string m_Name;
        private const char k_WhiteSymbol = 'o';
        private const char k_BlackSymbol = 'x';

        public char Symbol
        {
            get
            {
                return GetSymbol(r_IsWhiteDisc); 
            }
        }

        public bool IsComputer
        {
            get
            {
                return r_IsComputer;
            }
        }

        public bool DiscColor
        {
            get
            {
                return r_IsWhiteDisc;
            }
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        public Player(string i_Name, bool i_White, bool i_IsComputer)
        {
            r_IsComputer = i_IsComputer;
            r_IsWhiteDisc = i_White;
            m_Name = i_Name;
        }
        static public char GetSymbol(bool discColor)
        {
            return discColor ? k_WhiteSymbol : k_BlackSymbol;
        }

    }
}
