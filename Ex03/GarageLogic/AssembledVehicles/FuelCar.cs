namespace GarageLogic
{
    public class FuelCar : Car
    {
        private const int k_NumOfWheels = 4;
        private const float k_MaxWheelPressure = 32F;
        private const eFuelType k_FuelType = eFuelType.Octan95;
        private const float k_FuelTankCapacity = 46F;

        public FuelCar(eCarColor i_Color, byte i_NumOfDoors, string i_LicenseNumber, string i_Model)
            : base(i_Color, i_NumOfDoors, i_LicenseNumber, i_Model, k_NumOfWheels, k_MaxWheelPressure)
        {
            m_Engine = new FuelEngine(k_FuelType, k_FuelTankCapacity);
        }
    }
}