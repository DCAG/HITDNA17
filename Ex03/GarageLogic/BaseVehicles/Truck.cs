namespace GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_CarryDangerousChemicals;

        public bool CarryDangerousChemicals
        {
            get
            {
                return m_CarryDangerousChemicals;
            }
        }

        private float m_MaxCarryWeightAllowed;

        public float MaxCarryWeightAllowed
        {
            get
            {
                return m_MaxCarryWeightAllowed;
            }
        }

        public Truck(
            bool i_CarryDangerousChemicals, float i_MaxCarryWeightAllowed, string i_LicenseNumber, string i_Model, int i_NumOfWheels, float i_MaxWheelPressure)
            : base(i_LicenseNumber, i_Model, i_NumOfWheels, i_MaxWheelPressure)
        {
            m_CarryDangerousChemicals = i_CarryDangerousChemicals;
            m_MaxCarryWeightAllowed = i_MaxCarryWeightAllowed;
        }

        public override string ToString()
        {
            string toStringStr = @"{0}
Dangerous Chemicals: {1}
Max Weight Capacity: {2:N2} lb
";

            return string.Format(toStringStr, base.ToString(), m_CarryDangerousChemicals ? "Yes" : "No", m_MaxCarryWeightAllowed);
        }
    }
}