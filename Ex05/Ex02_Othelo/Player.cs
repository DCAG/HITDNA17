﻿namespace OthelloLogic
{
    internal class Player : IPlayer
    {
        private readonly bool r_IsComputer;
        private readonly eDiscColor r_DiscColor;
        private readonly string r_Name;
        private int m_DiscsCounter;
        private int m_RoundsWon;

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

        public int DiscsCounter
        {
            get
            {
                return m_DiscsCounter;
            }

            set
            {
                m_DiscsCounter = value;
            }
        }

        public int RoundsWon
        {
            get
            {
                return m_RoundsWon;
            }

            set
            {
                m_RoundsWon = value;
            }
        }

        public Player(string i_Name, eDiscColor i_DiscColor, bool i_IsComputer)
        {
            r_IsComputer = i_IsComputer;
            r_DiscColor = i_DiscColor;
            r_Name = i_Name;
            m_DiscsCounter = 0;
            m_RoundsWon = 0;
        }
    }
}
