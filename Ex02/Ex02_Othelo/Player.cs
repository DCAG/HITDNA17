using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    class Player
    {
        internal readonly bool IsComputer;
        internal readonly char Symbol;
        private string m_Name;
        public string Name {
            get { return m_Name; }
            set { m_Name = value; }
        }

        internal string GetRandomMove()
        {
            throw new NotImplementedException();
        }
    }
}
