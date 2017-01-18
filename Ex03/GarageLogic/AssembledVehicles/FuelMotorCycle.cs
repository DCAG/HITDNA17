public class FuelMotorCycle : MotorCycle
{
    const int k_NumOfWheels = 2;
    const float k_MaxWheelPressure = 31;
    const eFuelType k_FuelType = eFuelType.Octan98;
    const float k_FuelTankCapacity = 6.2F;

    public FuelMotorCycle(eMotorCycleLicenseType i_LicenseType, int i_EngineCapacity,
        string i_LicenseNumber, string i_Model /*, i_EnergyLeftPercentage*/)
        : base(i_LicenseType, i_EngineCapacity, i_LicenseNumber, i_Model,
            k_NumOfWheels, k_MaxWheelPressure /*, i_EnergyLeftPercentage*/)
    {
        m_Engine = new FuelEngine(k_FuelType, k_FuelTankCapacity);
    }
}

