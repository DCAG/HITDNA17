using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public class GarageManager
    {
        private List<VehicleServiceTicket> m_VehiclesInService;

        public GarageManager()
        {
            m_VehiclesInService = new List<VehicleServiceTicket>();
        }

        public void InsertVehicleForTreatment(VehicleServiceTicket i_ServiceTicket)
        {
            int index = m_VehiclesInService.IndexOf(i_ServiceTicket);
            if(index >= 0)
            {
                throw new ValueExistingException();
            }

            m_VehiclesInService.Add(i_ServiceTicket);
        }

        private Dictionary<string, eVehicleServiceStatus> GetVehicleLicenseList()
        {
            Dictionary<string, eVehicleServiceStatus> licenseAndStatusPairs = new Dictionary<string, eVehicleServiceStatus>();
            foreach (VehicleServiceTicket ticket in m_VehiclesInService)
            {
                licenseAndStatusPairs[ticket.Vehicle.LicenseNumber] = ticket.Status;
            }

            return licenseAndStatusPairs;
        }

        public List<string> GetVehicleLicenseList(eVehicleServiceStatus? i_Status)
        {
            List<string> licenses;
            if (i_Status.HasValue)
            {
                licenses = new List<string>();

                foreach (KeyValuePair<string, eVehicleServiceStatus> licenseStatuspair in GetVehicleLicenseList())
                {
                    if(i_Status.Value == licenseStatuspair.Value)
                    {
                        licenses.Add(licenseStatuspair.Key);
                    }
                }
            }
            else
            {
                licenses = new List<string>(GetVehicleLicenseList().Keys);
            }

            return licenses;
        }

        public void ChangeVehicleServiceStatus(VehicleServiceTicket i_ServiceTicket, eVehicleServiceStatus i_NewStatus)
        {
            i_ServiceTicket.Status = i_NewStatus;
        }

        public VehicleServiceTicket GetVehicleServiceTicket(string i_LicenseNumber)
        {
            VehicleServiceTicket serviceTicket = null;
            int index = m_VehiclesInService.IndexOf(new VehicleServiceTicket(new Vehicle(i_LicenseNumber)));
            if(index >= 0)
            {
                serviceTicket = m_VehiclesInService[index];
            }

            return serviceTicket;
        }

        public void InflateAirInWheelsToMax(VehicleServiceTicket i_ServiceTicket)
        {
            foreach(Wheel wheel in i_ServiceTicket.Vehicle.Wheels)
            {
                wheel.Inflate(wheel.MaxAirPressure);
            }
        }

        public void Refuel(VehicleServiceTicket i_ServiceTicket, eFuelType i_FuelType, float i_Amount)
        {
            if (i_ServiceTicket.Vehicle.Engine is FuelEngine)
            {
                (i_ServiceTicket.Vehicle.Engine as FuelEngine).Refuel(i_Amount, i_FuelType);
            }
            else
            {
                throw new Exception("Refuel is not supported for this type of vehicle");
            }
        }

        public void Recharge(VehicleServiceTicket i_ServiceTicket, float i_MinutesToCharge)
        {
            const int k_MinutesInOneHour = 60;
            if (i_ServiceTicket.Vehicle.Engine is ElectricEngine)
            {
                (i_ServiceTicket.Vehicle.Engine as ElectricEngine).RechargeBattery(i_MinutesToCharge / k_MinutesInOneHour);
            }
            else
            {
                throw new Exception("Recharge is not supported for this type of vehicle");
            }
        }
    }
}
