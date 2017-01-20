namespace GarageLogic
{
    public class VehicleServiceTicket
    {
        private const eVehicleServiceStatus k_DefaultStatus = eVehicleServiceStatus.Repair;

        private string m_OwnerName;
        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }
            set
            {
                m_OwnerName = value;
            }
        }

        private string m_OwnerPhoneNumber;
        public string OwnerPhoneNumber
        {
            get
            {
                return m_OwnerPhoneNumber;
            }
            set
            {
                m_OwnerPhoneNumber = value;
            }
        }
        private eVehicleServiceStatus m_Status;

        public eVehicleServiceStatus Status
        {
            get
            {
                return m_Status;
            }
            set
            {
                m_Status = value;
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

        public VehicleServiceTicket(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            m_Vehicle = i_Vehicle;
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_Status = k_DefaultStatus;
        }

        public VehicleServiceTicket(Vehicle i_Vehicle)
        {
            m_Vehicle = i_Vehicle;
            m_Status = k_DefaultStatus;
        }

        public override string ToString()
        {
            return string.Format(@"
Owner Name           : {0}
Owner Phone Number   : {1}
Garage Service Status: {2}
Vehicle Details      :
{3}
", m_OwnerName, m_OwnerPhoneNumber, m_Status, m_Vehicle);
        }

        public override bool Equals(object obj)
        {
            bool equals = true;
            VehicleServiceTicket toCompareTo = obj as VehicleServiceTicket;

            if (toCompareTo != null)
            {
                equals = m_Vehicle.Equals(toCompareTo.m_Vehicle);
            }

            return equals;
        }

        public override int GetHashCode()
        {
            return m_Vehicle.GetHashCode();
        }
    }
}