public class ElectricEngine : Engine
{
    public ElectricEngine(float i_MaxBatteryTime) : base(i_MaxBatteryTime)
    {

    }

    public float BatteryTimeLeft
	{
        get
        {
            return m_CurrentEnergyAmount;
        }
	}

	public float MaxBatteryTime
	{
        get
        {
            return m_MaxEnergyAmount;
        }
	}

	public void RechargeBattery(float i_HoursToAdd)
	{
        m_CurrentEnergyAmount += i_HoursToAdd;
        if(m_CurrentEnergyAmount > m_MaxEnergyAmount)
        {
            m_CurrentEnergyAmount = m_MaxEnergyAmount;
        }
	}

}

