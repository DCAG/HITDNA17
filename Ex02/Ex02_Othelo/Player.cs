using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    class Player
    {
        private readonly bool r_IsComputer;
        private readonly bool r_WhiteDisc;
        private readonly string m_Name;

        public bool IsComputer
        {
            get
            {
                return r_IsComputer;
            }
        }

        public bool WhiteDisc
        {
            get
            {
                return r_WhiteDisc;
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
            r_WhiteDisc = i_White;
            m_Name = i_Name;
        }
    }
}
