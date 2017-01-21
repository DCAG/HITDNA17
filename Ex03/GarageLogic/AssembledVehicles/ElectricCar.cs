namespace GarageLogic
{
    public class ElectricCar : Car
    {
        private const int k_NumOfWheels = 4;
        private const float k_MaxWheelPressure = 32F;
        private const float k_MaxBatteryTime = 2.7F;

        public ElectricCar(
            eCarColor i_Color, byte i_NumOfDoors, string i_LicenseNumber, string i_Model)
            : base(i_Color, i_NumOfDoors, i_LicenseNumber, i_Model, k_NumOfWheels, k_MaxWheelPressure)
        {
            m_Engine = new ElectricEngine(k_MaxBatteryTime);
        }
    }
}