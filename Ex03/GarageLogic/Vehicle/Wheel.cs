namespace GarageLogic
{
    public class Wheel
    {
        private const string k_DefaultManufacturer = "Generic";
        private string m_Manufacturer;
        public string Manufacturer
        {
            get
            {
                return m_Manufacturer;
            }
            set
            {
                m_Manufacturer = value;
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
                return m_MaxAirPressure;
            }
        }

        public Wheel(float i_MaxAirPressure)
        {
            m_MaxAirPressure = i_MaxAirPressure;
            m_Manufacturer = k_DefaultManufacturer;
        }

        public Wheel(float i_MaxAirPressure, float i_AirPressure)
        {
            m_MaxAirPressure = i_MaxAirPressure;
            m_AirPressure = i_AirPressure;
            m_Manufacturer = k_DefaultManufacturer;
        }

        public Wheel(float i_MaxAirPressure, string i_Manufacturer)
        {
            m_MaxAirPressure = i_MaxAirPressure;
            m_Manufacturer = i_Manufacturer;
        }

        public Wheel(float i_MaxAirPressure, string i_Manufacturer, float i_AirPressure)
        {
            m_MaxAirPressure = i_MaxAirPressure;
            m_Manufacturer = i_Manufacturer;
            m_AirPressure = i_AirPressure;
        }

        public void Inflate(float i_AirPressure)
        {
            m_AirPressure += i_AirPressure;
            if (m_AirPressure > m_MaxAirPressure)
            {
                m_AirPressure = m_MaxAirPressure;
            }
        }

        public override string ToString()
        {
            return string.Format("Air Pressure: {0:N2}PSI /{1:N2}PSI , Manufacturer: {2}", m_AirPressure, m_MaxAirPressure, m_Manufacturer);
        }
    }
}