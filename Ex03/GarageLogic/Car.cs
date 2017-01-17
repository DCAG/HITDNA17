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
        string i_Model, int i_NumOfWheels, float i_MaxWheelPressure /*,
         , i_EnergyLeftPercentage*/) : base(i_LicenseNumber, i_Model,
            i_NumOfWheels, i_MaxWheelPressure /*, i_EnergyLeftPercentage*/)
    {
        m_Color = i_Color;
        m_DoorsCount = i_NumOfDoors;
    }
}

