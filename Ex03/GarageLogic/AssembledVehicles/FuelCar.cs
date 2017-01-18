public class FuelCar : Car
{
    const int k_NumOfWheels = 4;
    const float k_MaxWheelPressure = 32;
    const eFuelType k_FuelType = eFuelType.Octan95;
    const float k_FuelTankCapacity = 46;

    public FuelCar(eCarColor i_Color, byte i_NumOfDoors, string i_LicenseNumber,
        string i_Model /*, i_EnergyLeftPercentage*/) : base(i_Color,
            i_NumOfDoors, i_LicenseNumber, i_Model, k_NumOfWheels,
            k_MaxWheelPressure /*, i_EnergyLeftPercentage*/)
    {
        m_Engine = new FuelEngine(k_FuelType, k_FuelTankCapacity);
    }
}

