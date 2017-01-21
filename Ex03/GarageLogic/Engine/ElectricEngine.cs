namespace GarageLogic
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxBatteryTime) : base(i_MaxBatteryTime)
        {
        }

        public float BatteryTimeLeft
        {
            get
            {
                return m_CurrentEnergyAmount;
            }
        }

        public float MaxBatteryTime
        {
            get
            {
                return m_MaxEnergyAmount;
            }
        }

        public void RechargeBattery(float i_HoursToAdd)
        {
            if (m_MaxEnergyAmount < m_CurrentEnergyAmount + i_HoursToAdd)
            {
                throw new ValueOutOfRangeException(0, m_MaxEnergyAmount - m_CurrentEnergyAmount);
            }

            m_CurrentEnergyAmount += i_HoursToAdd;
        }

        public override string ToString()
        {
            string toStringStr = @"
Battery Time Left: {0:N2}H
Max Battery Time : {1:N2}H
";

            return string.Format(toStringStr, BatteryTimeLeft, MaxBatteryTime);
        }
    }
}