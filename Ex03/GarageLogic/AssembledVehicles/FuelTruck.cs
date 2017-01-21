namespace GarageLogic
{
    public class FuelTruck : Truck
    {
        const int k_NumOfWheels = 12;
        const float k_MaxWheelPressure = 26F;
        const eFuelType k_FuelType = eFuelType.Octan96;
        const float k_FuelTankCapacity = 150F;

        public FuelTruck(bool i_CarryDangerousChemicals,
            float i_MaxCarryWeightAllowed, string i_LicenseNumber, string i_Model)
            : base(i_CarryDangerousChemicals, i_MaxCarryWeightAllowed, i_LicenseNumber,
                  i_Model, k_NumOfWheels, k_MaxWheelPressure)
        {
            m_Engine = new FuelEngine(k_FuelType, k_FuelTankCapacity);
        }
    }
}