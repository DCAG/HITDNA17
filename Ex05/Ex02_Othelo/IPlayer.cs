using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloLogic
{
    /// <summary>
    /// This is a Public interface that reveals all the properties of Player class for *readonly access* (get)
    /// and is intended to be used by GUI implemenations.
    /// Player class will stay interal with change access (set) also where and if it is required.
    /// </summary>
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
