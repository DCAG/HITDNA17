namespace Ex02_Othelo
{
    public struct OtheloPoint
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

        public OtheloPoint(int i_X, int i_Y)
        {
            m_X = i_X;
            m_Y = i_Y;
        }

        public void Add(OtheloPoint i_Addition)
        {
            m_X += i_Addition.m_X;
            m_Y += i_Addition.m_Y;
        }
    }
}