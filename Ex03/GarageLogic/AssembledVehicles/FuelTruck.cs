public class FuelTruck : Truck
{
    const int k_NumOfWheels = 12;
    const float k_MaxWheelPressure = 26;
    const eFuelType k_FuelType = eFuelType.Octan96;
    const float k_FuelTankCapacity = 150;

    public FuelTruck(bool i_CarryDangerousChemicals,
        float i_MaxCarryWeightAllowed, string i_LicenseNumber, string i_Model/*,
        float i_EnergyLeftPercentage*/) : base(i_CarryDangerousChemicals, i_MaxCarryWeightAllowed,
             i_LicenseNumber, i_Model, k_NumOfWheels, k_MaxWheelPressure/*, i_EnergyLeftPercentage*/)
    {
        m_Engine = new FuelEngine(k_FuelType, k_FuelTankCapacity);
    }
}

