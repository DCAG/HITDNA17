public class ElectricMotorCycle : MotorCycle
{
    const int k_NumOfWheels = 2;
    const float k_MaxWheelPressure = 32;
    const float k_MaxBatteryTime = 2.7F;

    public ElectricMotorCycle(eMotorCycleLicenseType i_LicenseType, int i_EngineCapacity,
        string i_LicenseNumber, string i_Model /*, i_EnergyLeftPercentage*/)
        : base(i_LicenseType, i_EngineCapacity, i_LicenseNumber, i_Model,
            k_NumOfWheels, k_MaxWheelPressure /*, i_EnergyLeftPercentage*/)
    {
        m_Engine = new ElectricEngine(k_MaxBatteryTime);
    }
}