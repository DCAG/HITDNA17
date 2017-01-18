public class VehicleTreatment
{
    private string m_OwnerName;

    private string m_OwnerPhoneNumber;

    private eVehicleTreatmentStatus m_Status;

    public eVehicleTreatmentStatus Status
    {
        get
        {
            return m_Status;
        }
    }

	public VehicleTreatment(string i_OwnerName, string i_OwnerPhoneNumber, eVehicleTreatmentStatus i_Status)
	{
        m_OwnerName = i_OwnerName;
        m_OwnerPhoneNumber = i_OwnerPhoneNumber;
        m_Status = i_Status;
    }

    public static VehicleTreatment InsertVehicleToService(string i_OwnerName, string i_OwnerPhoneNumber)
    {
        return new VehicleTreatment(i_OwnerName, i_OwnerPhoneNumber, eVehicleTreatmentStatus.Repair);
    }

    public override string ToString()
    {
        return string.Format(@"
Owner Name           : {0}
Owner Phone Number   : {1}
Garage Service Status: {2}
", m_OwnerName, m_OwnerPhoneNumber, m_Status);
    }

}

