using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    public interface IPlayer
    {
        bool IsComputer
        {
            get;
        }

        eDiscColor Color
        {
            get;
        }

        string Name
        {
            get;
        }

        int DiscsCounter
        {
            get;
        }

        int RoundsWon
        {
            get;
        }
    }
}
