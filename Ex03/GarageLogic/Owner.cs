namespace GarageLogic
{
    internal class Owner
    {
        private string m_Name;
        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        private string m_PhoneNumber;
        public string PhoneNumber
        {
            get
            {
                return m_PhoneNumber;
            }
        }

        public Owner(string i_Name, string i_PhoneNumber)
        {
            m_Name = i_Name;
            m_PhoneNumber = i_PhoneNumber;
        }
    }
}