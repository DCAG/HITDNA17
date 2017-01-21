using System;

namespace GarageLogic
{
    public class Engine
    {
        protected float m_CurrentEnergyAmount;

        public float CurrentEnergyAmount
        {
            get
            {
                return m_CurrentEnergyAmount;
            }
        }

        protected float m_MaxEnergyAmount;

        public float MaxEnergyAmount
        {
            get
            {
                return m_MaxEnergyAmount;
            }
        }

        protected Engine(float i_MaxEnergyAmount)
        {
            m_MaxEnergyAmount = i_MaxEnergyAmount;
            Random random = new Random();
            m_CurrentEnergyAmount = (float)(m_MaxEnergyAmount * random.NextDouble());
        }

        protected Engine(float i_MaxEnergyAmount, float i_CurrentEnergyAmount)
        {
            m_MaxEnergyAmount = i_MaxEnergyAmount;
            m_CurrentEnergyAmount = i_CurrentEnergyAmount;
        }
    }
}