using System;
using System.Collections.Generic;
using GarageLogic;

namespace ConsoleUI {
    public class GarageManager
    {
        private Dictionary<Vehicle, VehicleServiceTicket> m_VehiclesInService;
        GarageLogic.GarageManager m_Manager = new GarageLogic.GarageManager();
        public GarageManager()
        {
            CreateVehiclesToRoamTheRoads();
        }

        private void CreateVehiclesToRoamTheRoads()
        {
            foreach (Vehicle v in VehiclesRegistrationDB.Vehicles)
            {
                Console.WriteLine(v);
            }
            Console.ReadLine();
        }

        public void InsertVehicleForTreatment(string i_LicenseNumber)
        {
            int index = VehiclesRegistrationDB.Vehicles.IndexOf(new Vehicle(i_LicenseNumber));
            if (index >= 0)
            {
                m_VehiclesInService.Add(VehiclesRegistrationDB.Vehicles[index], new VehicleServiceTicket(VehiclesRegistrationDB.Owners[i_LicenseNumber]));
            }
            else
            {
                Console.WriteLine("Vehicle with License Number {0} does not exist",i_LicenseNumber);
            }
        }

        #region Print Vehicle Licenses
        public void GetVehicleLicenseList()
        {
            try
            {
                string selectionStr = Console.ReadLine();
                int selection = int.Parse(selectionStr);
                eVehicleServiceStatus? status = null;
                if(selection == 0)
                {
                    status = (eVehicleServiceStatus)selection;
                }
                PrintVehicleLicenseList(status);
            }
            catch(FormatException)
            {
                Console.WriteLine("Error: Invalid Selection");
            }
            catch(InvalidCastException)
            {
                Console.WriteLine("Error: Invalid Selection");
            }
        }

        private void PrintVehicleLicenseList(eVehicleServiceStatus? i_Status)
        {
            Console.WriteLine("License numbers with {0}",i_Status.HasValue?string.Format("status {0}",i_Status):"any status");
            foreach (string license in m_Manager.GetVehicleLicenseList(i_Status))
            {
                Console.WriteLine(license);
            }
        }
        #endregion

        private void PrintEnum(Type i_EnumType)
        {
            foreach(var value in Enum.GetValues(i_EnumType))
            {
                Console.WriteLine("{0} - {1}", value, Enum.GetName(i_EnumType, value));
            }
        }

        public void ChangeVehicleServiceStatus()
        {
            VehicleServiceTicket ticket = m_Manager.GetVehicleServiceTicket();
            m_Manager.ChangeVehicleServiceStatus(ticket,);

           /*
            if (index >= 0)
            {
                Console.WriteLine("Changng treatment status on vehicle {0} from {1} to {2}", VehicleServiceTicket);
                Console.WriteLine("Done!");
            }
            else
            {
                Console.WriteLine("Vehicle with License Number {0} was not found", i_LicenseNumber);
            }
            */
        }

        public void InflateAirInWheelsToMax()
        {
            VehicleServiceTicket ticket = m_Manager.GetVehicleServiceTicket();
            m_Manager.InflateAirInWheelsToMax(ticket);
        }

        public void RefuelA()
        {
            try
            {
                Console.WriteLine("Enter license number:");
                string licenseNumberStr = Console.ReadLine();
                Console.WriteLine("Select Fuel Type:");
                PrintEnum(typeof(eFuelType));
                string fuelTypeStr = Console.ReadLine();
                Console.WriteLine("Enter fuel amount:");
                string fuelAmountStr = Console.ReadLine();
                Refuel(licenseNumberStr, (eFuelType)int.Parse(fuelTypeStr), float.Parse(fuelAmountStr));
            }
            catch (FormatException)
            {
                Console.WriteLine("Your input is not valid");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Wrong argument");
            }
            catch (ValueOutOfRangeException)
            {
                Console.WriteLine("Value was out of range");
            }
        }

        public void Refuel()
        {
            VehicleServiceTicket ticket = m_Manager.GetVehicleServiceTicket();
            m_Manager.Refuel(ticket,);
        }

        public void Recharge()
        {
            VehicleServiceTicket ticket = m_Manager.GetVehicleServiceTicket();
            m_Manager.Recharge(ticket,);
        }

        public void PrintVehicleDetails(string i_License)
        {

        }

        public void Start()
        {
            eGarageMainMenuItems selectedMenuOption = eGarageMainMenuItems.None;
            bool exit = false;
            do
            {
                PrintMenu();
                //selectedMenuOption = SelectMenuItem();
                switch (selectedMenuOption)
                {
                    case eGarageMainMenuItems.ListVehicles:
                        foreach (object status in Enum.GetValues(typeof(eVehicleServiceStatus)))
                        {
                            Console.WriteLine("{0} - {1}", status, status.ToString());
                        }
                        Console.WriteLine("select status");
                        string selectedStatus = Console.ReadLine();
                        GetVehicleLicenseList((eVehicleServiceStatus)int.Parse(selectedStatus));
                        break;
                    case eGarageMainMenuItems.NewVehicle:
                        InsertVehicleForTreatment(Console.ReadLine());
                        break;
                    case eGarageMainMenuItems.ServiceSpecificVehicle:
                        //StartVehicleServiceOperationsSubMenu();
                        break;
                    default:
                        exit = selectedMenuOption == eGarageMainMenuItems.Exit;
                        break;
                }

            }
            while (!exit);
        }

        public void PrintMenu()
        {
            Console.WriteLine("{0} - Enter a new vehicle to service", eGarageMainMenuItems.NewVehicle);
            Console.WriteLine("{0} - List vehicles in service", eGarageMainMenuItems.ListVehicles);
            Console.WriteLine("{0} - Service a specific vehicle", eGarageMainMenuItems.ServiceSpecificVehicle);
            Console.WriteLine("{0} - Exit", eGarageMainMenuItems.Exit);
        }
        /*
        public void PrintServiceSpecificVehicleSubMenu(string i_VehicleLicenseNumber)
        {
            Console.WriteLine("{0} - Show vehicle details", eSpecificVehiclesOperationsSubMenu.ShowDetails);
            Console.WriteLine("{0} - Change vehicle status", eSpecificVehiclesOperationsSubMenu.ChangeStatus);
            Console.WriteLine("{0} - Inflate vehicle wheels to maximum pressure", eSpecificVehiclesOperationsSubMenu.InflateWheelsToMax);

            Vehicle m_Vehicle = new Vehicle();

            if(m_Vehicle.Engine.GetType() == typeof(FuelEngine))
            {
                Console.WriteLine("{0} - Fuel vehicle", eSpecificVehiclesOperationsSubMenu.IncreaseEnergySupply);
            }
            else if (m_Vehicle.Engine.GetType() == typeof(ElectricEngine))
            {
                Console.WriteLine("{0} - Charge vehicle battery", eSpecificVehiclesOperationsSubMenu.IncreaseEnergySupply);
            }
        }
        */
    }
}