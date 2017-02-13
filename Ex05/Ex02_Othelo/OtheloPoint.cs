namespace Ex02_Othelo
{
    public struct OthelloPoint
    {
        private int m_X;
        private int m_Y;

        public int X
        {
            get
            {
                return m_X;
            }

            set
            {
                m_X = value;
            }
        }

        public int Y
        {
            get
            {
                return m_Y;
            }

            set
            {
                m_Y = value;
            }
        }

        public OthelloPoint(int i_X, int i_Y)
        {
            m_X = i_X;
            m_Y = i_Y;
        }

        public void Add(OthelloPoint i_Addition)
        {
            m_X += i_Addition.m_X;
            m_Y += i_Addition.m_Y;
        }
    }
}