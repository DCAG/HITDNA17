namespace GarageLogic
{
    public class MotorCycle : Vehicle
    {
        private eMotorCycleLicenseType m_LicenseType;

        public eMotorCycleLicenseType LicenseType
        {
            get
            {
                return LicenseType;
            }
        }

        private int m_EngineCapacity;

        public int EngineCapacity
        {
            get
            {
                return m_EngineCapacity;
            }
        }

        public MotorCycle(eMotorCycleLicenseType i_LicenseType, int i_EngineCapacity,
            string i_LicenseNumber, string i_Model, int i_NumOfWheels,
            float i_MaxWheelPressure /*, i_EnergyLeftPercentage*/)
            : base(i_LicenseNumber, i_Model, i_NumOfWheels,
                i_MaxWheelPressure /*, i_EnergyLeftPercentage*/)
        {
            m_EngineCapacity = i_EngineCapacity;
            m_LicenseType = i_LicenseType;
        }

        public override string ToString()
        {
            return string.Format(@"{0}
License Type   : {1}
Engine Capacity: {2}
", base.ToString(), m_LicenseType, m_EngineCapacity);
        }

    }
}