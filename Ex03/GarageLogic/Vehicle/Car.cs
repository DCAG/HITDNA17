namespace GarageLogic
{
    public class Car : Vehicle
    {
        private eCarColor m_Color;

        public eCarColor Color
        {
            get
            {
                return m_Color;
            }
        }

        private byte m_DoorsCount;

        public byte DoorsCount
        {
            get
            {
                return m_DoorsCount;
            }
        }

        public Car(eCarColor i_Color, byte i_NumOfDoors, string i_LicenseNumber,
            string i_Model, int i_NumOfWheels, float i_MaxWheelPressure)
            : base(i_LicenseNumber, i_Model, i_NumOfWheels, i_MaxWheelPressure)
        {
            m_Color = i_Color;
            m_DoorsCount = i_NumOfDoors;
        }

        public override string ToString()
        {
            return string.Format(@"{0}
Car Color  : {1}
Doors Count: {2}
", base.ToString(), m_Color, m_DoorsCount);
        }

    }
}