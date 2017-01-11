using System.Collections.Generic;

namespace Ex02_Othelo
{
    internal class Move
    {
        private Point m_NewDiscPosition;
        private List<Point> m_DiscsToFlip;

        public Move(Point i_NewDiscPosition)
        {
            m_NewDiscPosition = i_NewDiscPosition;
            m_DiscsToFlip = new List<Point>();
        }

        public Point NewDiscPosition
        {
            get
            {
                return m_NewDiscPosition;
            }
        }

        public List<Point> DiscsToFlip
        {
            get
            {
                return m_DiscsToFlip;
            }
            set
            {
                m_DiscsToFlip = value;
            }
        }

        public int GetScoreGain()
        {
            return 1 + m_DiscsToFlip.Count;
        }
    }
}