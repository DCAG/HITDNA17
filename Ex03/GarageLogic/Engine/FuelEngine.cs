using System;

namespace GarageLogic
{
    public class FuelEngine : Engine
    {
        private eFuelType m_FuelType;

        public FuelEngine(eFuelType i_FuelType, float i_FuelTankCapacity) : base(i_FuelTankCapacity)
        {
            this.m_FuelType = i_FuelType;
        }

        public eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }
        }

        public float CurrentFuelAmount
        {
            get
            {
                return m_CurrentEnergyAmount;
            }
        }

        public float FuelTankCapacity
        {
            get
            {
                return m_MaxEnergyAmount;
            }
        }

        public void Refuel(float i_FuelAmountToAdd, eFuelType i_FuelType)
        {
            if (i_FuelType == m_FuelType)
            {
                if(m_MaxEnergyAmount < m_CurrentEnergyAmount + i_FuelAmountToAdd)
                {
                    throw new ValueOutOfRangeException(0, m_MaxEnergyAmount - m_CurrentEnergyAmount);
                }

                m_CurrentEnergyAmount += i_FuelAmountToAdd;
            }
            else
            {
                throw new ArgumentException("Wrong fuel type");
            }
        }

        public override string ToString()
        {
            string toStringStr = @"
Fuel Amount       : {0:N2} L
Fuel Tank Capacity: {1:N2} L
Fuel Type         : {2}
";
            return string.Format(toStringStr, CurrentFuelAmount, MaxEnergyAmount, FuelType);
        }
    }
}