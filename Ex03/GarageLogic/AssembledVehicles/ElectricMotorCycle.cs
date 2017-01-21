namespace GarageLogic
{
    public class ElectricMotorCycle : MotorCycle
    {
        private const int k_NumOfWheels = 2;
        private const float k_MaxWheelPressure = 32F;
        private const float k_MaxBatteryTime = 2.7F;

        public ElectricMotorCycle(
            eMotorCycleLicenseType i_LicenseType, int i_EngineCapacity, string i_LicenseNumber, string i_Model)
            : base(i_LicenseType, i_EngineCapacity, i_LicenseNumber, i_Model, k_NumOfWheels, k_MaxWheelPressure)
        {
            m_Engine = new ElectricEngine(k_MaxBatteryTime);
        }
    }
}