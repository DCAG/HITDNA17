namespace GarageLogic
{
    public class FuelEngine : Engine
    {
        private eFuelType m_FuelType;

        public FuelEngine(eFuelType i_FuelType, float i_FuelTankCapacity) : base(i_FuelTankCapacity)
        {
            m_FuelType = i_FuelType;
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
                if (m_CurrentEnergyAmount > m_MaxEnergyAmount)
                {
                    m_CurrentEnergyAmount = m_MaxEnergyAmount;
                }
            }
        }

        public override string ToString()
        {
            return string.Format(@"
Fuel Amount       : {0}
Fuel Tank Capacity: {1}
Fuel Type         : {2}", CurrentFuelAmount, MaxEnergyAmount, FuelType);
        }
    }
}