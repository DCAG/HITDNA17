using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    enum PlayerType
    {
        Human,
        Computer
    }
    class Player
    {
        string m_Name;
        PlayerType m_PlayerType;
        int m_NumberOfGamesWon;

        public string Name {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        public PlayerType Type {
            get { return m_PlayerType; }
            set { m_PlayerType = value; }
        }

        Square PlayTurn(List<OtheloMove> i_AvailabeMoves)
        {
            return "";
        }
    }
}
