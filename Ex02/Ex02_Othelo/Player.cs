using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    internal struct Player
    {
        private readonly bool r_IsComputer;
        private readonly eDiscColor r_DiscColor;
        private readonly string r_Name;

        public bool IsComputer
        {
            get
            {
                return r_IsComputer;
            }
        }

        public eDiscColor Color
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
                return r_Name;
            }
        }

        public Player(string i_Name, eDiscColor i_DiscColor, bool i_IsComputer)
        {
            r_IsComputer = i_IsComputer;
            r_DiscColor = i_DiscColor;
            r_Name = i_Name;
        }
    }
}
