namespace GarageLogic
{
    public class FuelMotorCycle : MotorCycle
    {
        private const int k_NumOfWheels = 2;
        private const float k_MaxWheelPressure = 31F;
        private const eFuelType k_FuelType = eFuelType.Octan98;
        private const float k_FuelTankCapacity = 6.2F;

        public FuelMotorCycle(
            eMotorCycleLicenseType i_LicenseType, int i_EngineCapacity, string i_LicenseNumber, string i_Model)
            : base(i_LicenseType, i_EngineCapacity, i_LicenseNumber, i_Model, k_NumOfWheels, k_MaxWheelPressure)
        {
            m_Engine = new FuelEngine(k_FuelType, k_FuelTankCapacity);
        }
    }
}