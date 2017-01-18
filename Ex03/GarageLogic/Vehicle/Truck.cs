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

    public Truck(bool i_CarryDangerousChemicals,
        float i_MaxCarryWeightAllowed,
        string i_LicenseNumber,
        string i_Model, int i_NumOfWheels,
        float i_MaxWheelPressure/*, float i_EnergyLeftPercentage*/)
        : base(i_LicenseNumber, i_Model, i_NumOfWheels, i_MaxWheelPressure/*, i_EnergyLeftPercentage*/)
    {
        m_CarryDangerousChemicals = i_CarryDangerousChemicals;
        m_MaxCarryWeightAllowed = i_MaxCarryWeightAllowed;
    }

    public override string ToString()
    {
        return string.Format(@"{0}
Dangerous Chemicals: {1}
Max Weight Capacity: {2}", base.ToString(), m_CarryDangerousChemicals, m_MaxCarryWeightAllowed);
    }

}

