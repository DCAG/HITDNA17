namespace GarageLogic
{
    public class VehicleServiceTicket
    {
        private const eVehicleServiceStatus k_DefaultStatus = eVehicleServiceStatus.Repair;

        private string m_OwnerName;

        private string m_OwnerPhoneNumber;

        private eVehicleServiceStatus m_Status;

        public eVehicleServiceStatus Status
        {
            get
            {
                return m_Status;
            }
        }

        private Vehicle m_Vehicle;

        public Vehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }
        }

        public VehicleServiceTicket(string i_OwnerName, string i_OwnerPhoneNumber, eVehicleServiceStatus i_Status)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_Status = i_Status;
        }

        public VehicleServiceTicket(Vehicle i_Vehicle)
        {
            m_Vehicle = i_Vehicle;
            m_Status = k_DefaultStatus;
        }

        public static VehicleServiceTicket InsertVehicleToService(string i_OwnerName, string i_OwnerPhoneNumber)
        {
            return new VehicleServiceTicket(i_OwnerName, i_OwnerPhoneNumber, eVehicleServiceStatus.Repair);
        }

        public override string ToString()
        {
            return string.Format(@"
Owner Name           : {0}
Owner Phone Number   : {1}
Garage Service Status: {2}
", m_OwnerName, m_OwnerPhoneNumber, m_Status);
        }

        public override bool Equals(object obj)
        {
            bool equals = true;
            VehicleServiceTicket toCompareTo = obj as VehicleServiceTicket;

            if (toCompareTo != null)
            {
                equals = m_Vehicle == toCompareTo.m_Vehicle;
            }

            return equals;
        }

        public override int GetHashCode()
        {
            return m_Vehicle.GetHashCode();
        }
    }
}