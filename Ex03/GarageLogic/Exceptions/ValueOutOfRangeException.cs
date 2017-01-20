using System;

namespace GarageLogic
{
    class ValueOutOfRangeException : Exception
    {
        private float m_MinValue;
        public float MinValue
        {
            get
            {
                return MinValue;
            }
        }
        private float m_MaxValue;
        public float MaxValue
        {
            get
            {
                return m_MaxValue;
            }
        }

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, Exception i_InnerException) : base(string.Format("Value is out of range [{0}, {1}]", i_MinValue, i_MaxValue), i_InnerException)
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }
    }
}
