using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public class Vehicle
    {
        private string m_Model;
        public string Model
        {
            get
            {
                return m_Model;
            }
        }

        private string m_LicenseNumber;
        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }
        }

        protected Engine m_Engine;
        public Engine Engine
        {
            get
            {
                return m_Engine;
            }
        }

        private Wheel[] m_Wheels;
        public Wheel[] Wheels
        {
            get
            {
                return m_Wheels;
            }
        }

        public float EnergyLeftPercentage
        {
            get
            {
                return Engine.CurrentEnergyAmount / Engine.MaxEnergyAmount;
            }
        }

        public Vehicle(string i_LicenseNumber, string i_Model, int i_NumOfWheels, float i_MaxWheelPressure)
        {
            m_LicenseNumber = i_LicenseNumber;
            m_Model = i_Model;
            m_Wheels = new Wheel[i_NumOfWheels];
            Random random = new Random();
            for (int i = 0; i < i_NumOfWheels; i++)
            {
                m_Wheels[i] = new Wheel(i_MaxWheelPressure, (float)(i_MaxWheelPressure*random.NextDouble()));
            }
        }

        public Vehicle(string i_LicenseNumber)
        {
            m_LicenseNumber = i_LicenseNumber;
        }

        public override bool Equals(object obj)
        {
            bool equals = true;
            Vehicle toCompareTo = obj as Vehicle;

            if (toCompareTo != null)
            {
                equals = m_LicenseNumber.Equals(toCompareTo.m_LicenseNumber);
            }

            return equals;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat(@"
License Number: {0}
Model         : {1}
", m_LicenseNumber, m_Model);

            str.AppendFormat(@"Energy Consumption Details: {0:P} full
{1}", EnergyLeftPercentage, m_Engine);

            str.AppendFormat("Wheels Details:{0}", Environment.NewLine);
            foreach (Wheel wheel in m_Wheels)
            {
                str.AppendFormat("[{0}]{1}", wheel, Environment.NewLine);
            }

            return str.ToString();
        }

        public override int GetHashCode()
        {
            return m_LicenseNumber.GetHashCode();
        }
    }
}