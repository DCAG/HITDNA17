using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    class Player
    {
        private readonly bool r_IsComputer;
        private readonly bool m_WhiteDisc;
        private readonly string m_Name;

        public bool IsComputer
        {
            get
            {
                return r_IsComputer;
            }
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        public bool WhiteDisc
        {
            get
            {
                return m_WhiteDisc;
            }
        }

        public Player(string i_Name, bool i_White, bool i_IsComputer)
        {
            m_Name = i_Name;
            m_WhiteDisc = i_White;
            r_IsComputer = i_IsComputer;
        }

        internal string GetRandomMove()
        {
            throw new NotImplementedException();
        }
    }
}
