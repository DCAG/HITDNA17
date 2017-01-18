public class Wheel
{
    private string m_Manufacturer;
    public string Manufacturer
    {
        get
        {
            return m_Manufacturer;
        }
    }

    private float m_AirPressure;
    public float AirPressure
    {
        get
        {
            return m_AirPressure;
        }
    }

    private float m_MaxAirPressure;
    public float MaxAirPressure
    {
        get
        {
            return m_AirPressure;
        }
    }

    public Wheel(float i_MaxAirPressure)
    {
        m_MaxAirPressure = i_MaxAirPressure;
        m_Manufacturer = "Generic";
    }

    public void Inflation(float i_AirPressure)
	{
        m_AirPressure += i_AirPressure;
        if(m_AirPressure > m_MaxAirPressure)
        {
            m_AirPressure = m_MaxAirPressure;
        }
	}

    public override string ToString()
    {
        return string.Format("Air Pressure: {0:N2}/{1} , Manufacturer: {2}", m_AirPressure, m_MaxAirPressure, m_Manufacturer);
    }
}

