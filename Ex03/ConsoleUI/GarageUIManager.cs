using System;
using System.Collections.Generic;
using GarageLogic;
using ConsoleUI.HelperClasses;

namespace ConsoleUI
{
    public class GarageUIManager
    {
        private GarageManager m_Manager = new GarageManager();
        
        public GarageUIManager()
        {
            Console.WriteLine(@"
             ________
            /.--..--,\
           //___||___\\___,
   #%     / ,_,    -  ,_,  \  jgs
  #%@#  =(_/(_)\_____/(_)\__)
");
        }
        
        private void printAllVehiclesInDB()
        {
            Console.WriteLine("License Number  Vehicle Type");
            Console.WriteLine("--------------  ------------");
            foreach (Vehicle vehicle in VehiclesRegistrationDB.Vehicles)
            {
                Console.WriteLine("{0,14}  {1}", vehicle.LicenseNumber, vehicle.GetType().Name);
            }
        }

        private void insertVehicleToService()
        {
            Console.WriteLine("This is a list of all vehicles that exist in the DB{0}", Environment.NewLine);
            printAllVehiclesInDB();
            Console.WriteLine("{0}Select a vehicle to insert to the garage.", Environment.NewLine);
            Console.Write("Enter a vehicle license number: ");
            string userInputLicenseNumber = Console.ReadLine();
            Vehicle vehicleToAddToService = VehiclesRegistrationDB.FindVehicle(userInputLicenseNumber);
            if (vehicleToAddToService != null)
            {
                VehicleServiceTicket serviceTicket = new VehicleServiceTicket(vehicleToAddToService);
                Owner? ownerToAddToService = VehiclesRegistrationDB.FindOwner(userInputLicenseNumber);
                if (ownerToAddToService.HasValue)
                {
                    serviceTicket.OwnerName = ownerToAddToService.Value.Name;
                    serviceTicket.OwnerPhoneNumber = ownerToAddToService.Value.PhoneNumber;
                }

                try
                {
                    m_Manager.InsertVehicleForTreatment(serviceTicket);
                }
                catch(ValueOutOfRangeException)
                {
                    Console.WriteLine("Vehicle is already in the garage");
                }
            }
            else
            {
                Console.WriteLine("Vehicle with license number [{0}] does not exist", userInputLicenseNumber);
            }
        }

        #region Print Vehicle Licenses
        private void getVehicleLicenseList()
        {
            eVehicleServiceStatus? filter = null;
            Console.Write(@"To filter select one of the following:
Repair (1), Fixed (2), Paid (3), or any
other key to print all the license numbers:");                  
            string selectionStr = Console.ReadLine();
            int selection;
            if (int.TryParse(selectionStr, out selection))
            {
                switch (selection)
                {
                    case (int)eVehicleServiceStatus.Fixed:
                    case (int)eVehicleServiceStatus.Paid:
                    case (int)eVehicleServiceStatus.Repair:
                        filter = (eVehicleServiceStatus)selection;
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine();
            printVehicleLicenseList(filter);
        }

        private void printVehicleLicenseList(eVehicleServiceStatus? i_Status)
        {
            Console.WriteLine("License numbers with {0}", i_Status.HasValue ? string.Format("status: {0}", i_Status) : "any status");
            foreach (string license in m_Manager.GetVehicleLicenseList(i_Status))
            {
                Console.WriteLine(license);
            }
        }
        #endregion

        private void changeVehicleServiceStatus(VehicleServiceTicket i_ServiceTicket)
        {
            Console.WriteLine("Select a new status:");
            ConsoleHelper.PrintEnum(typeof(eVehicleServiceStatus));
            try
            {
                int selection = ConsoleHelper.ReadIntInput();
                eVehicleServiceStatus newStatus;
                switch (selection)
                {
                    case (int)eVehicleServiceStatus.Fixed:
                    case (int)eVehicleServiceStatus.Paid:
                    case (int)eVehicleServiceStatus.Repair:
                        newStatus = (eVehicleServiceStatus)selection;
                        break;
                    default:
                        throw new InvalidCastException();
                }

                m_Manager.ChangeVehicleServiceStatus(i_ServiceTicket, newStatus);
                Console.WriteLine("Service status of vehicle [{0}] is now set to: {1}", i_ServiceTicket.Vehicle.LicenseNumber, i_ServiceTicket.Status);
            }
            catch (InvalidCastException)
            {
                Console.WriteLine("Invalid option");
            }
        }

        private void inflateAirInWheelsToMax(VehicleServiceTicket i_ServiceTicket)
        {
            m_Manager.InflateAirInWheelsToMax(i_ServiceTicket);
            Console.WriteLine("Vehicle wheels were inflated.");
        }

        private void increaseVehicleEnergySupply(VehicleServiceTicket i_ServiceTicket)
        {
            if (i_ServiceTicket.Vehicle.Engine is ElectricEngine)
            {
                recharge(i_ServiceTicket);
            }
            else if (i_ServiceTicket.Vehicle.Engine is FuelEngine)
            {
                refuel(i_ServiceTicket);
            }
        }

        private void refuel(VehicleServiceTicket i_ServiceTicket)
        {
            try
            {
                Console.WriteLine("Here is a list of fuel types:");
                ConsoleHelper.PrintEnum(typeof(eFuelType));
                Console.Write("Enter selected fuel type:");
                string fuelTypeStr = Console.ReadLine();
                Console.Write("Enter fuel amount (Liters):");
                string fuelAmountStr = Console.ReadLine();
                m_Manager.Refuel(i_ServiceTicket, (eFuelType)int.Parse(fuelTypeStr), float.Parse(fuelAmountStr));
                Console.WriteLine("Vehicle was refueled.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine("Refuel amount must be between [{0}, {1}] L", ex.MinValue, ex.MaxValue);
            }
        }

        private void recharge(VehicleServiceTicket i_ServiceTicket)
        {
            try
            {
                Console.Write("Enter number of minutes to charge:");
                string rechargeMinutesStr = Console.ReadLine();
                m_Manager.Recharge(i_ServiceTicket, float.Parse(rechargeMinutesStr));
                Console.WriteLine("Vehicle was recharged.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ValueOutOfRangeException ex)
            {
                const int k_minutesInHour = 60;
                Console.WriteLine("Charge time must be between [{0}, {1}] minutes", ex.MinValue, ex.MaxValue * k_minutesInHour);
            }
        }

        private void printVehicleDetails(VehicleServiceTicket i_ServiceTicket)
        {
            Console.WriteLine(i_ServiceTicket.ToString());
        }

        private VehicleServiceTicket getVehicleServiceTicket()
        {
            Console.WriteLine("Select a vehicle from the garage.");
            Console.Write("Enter a vehicle License Number: ");
            VehicleServiceTicket serviceTicket = m_Manager.GetVehicleServiceTicket(Console.ReadLine());

            if (serviceTicket == null)
            {
                Console.WriteLine("Vehicle was not found in the garage");
            }

            return serviceTicket;
        }

        private void serviceSpecificVehicle()
        {
            VehicleServiceTicket serviceTicket = getVehicleServiceTicket();
            if (serviceTicket != null)
            {
                showVehicleServiceMenu(serviceTicket);
            }
        }

        public void ShowMainMenu()
        {
            int selectedMenuOption = (int)eGarageMainMenuItems.None;
            bool exit = false;
            do
            {
                Console.WriteLine("{0}# Garage Manager #", Environment.NewLine);
                printMenuOptions();
                selectedMenuOption = ConsoleHelper.ReadIntInput();
                switch (selectedMenuOption)
                {
                    case (int)eGarageMainMenuItems.ListVehicles:
                        getVehicleLicenseList();
                        break;
                    case (int)eGarageMainMenuItems.NewVehicle:
                        insertVehicleToService();
                        break;
                    case (int)eGarageMainMenuItems.ServiceSpecificVehicle:
                        serviceSpecificVehicle();
                        break;
                    case (int)eGarageMainMenuItems.Exit:
                        exit = true;
                        Console.WriteLine("Bye!");
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
            while (!exit);
        }

        private void printMenuOptions()
        {
            Console.WriteLine("{0} - Enter a new vehicle to service", (int)eGarageMainMenuItems.NewVehicle);
            Console.WriteLine("{0} - List vehicles in service", (int)eGarageMainMenuItems.ListVehicles);
            Console.WriteLine("{0} - Service a specific vehicle", (int)eGarageMainMenuItems.ServiceSpecificVehicle);
            Console.WriteLine("{0} - Exit", (int)eGarageMainMenuItems.Exit);
        }

        private void showVehicleServiceMenu(VehicleServiceTicket i_ServiceTicket)
        {
            int selectedMenuOption = (int)eServiceVehicleMenuOptions.None;
            bool back = false;
            do
            {
                Console.WriteLine("{0}# Garage Manager # > Service Vehicle [License Number: {1}]", Environment.NewLine, i_ServiceTicket.Vehicle.LicenseNumber);
                printServiceVehicleMenuOptions(i_ServiceTicket.Vehicle.Engine);
                selectedMenuOption = ConsoleHelper.ReadIntInput();
                switch (selectedMenuOption)
                {
                    case (int)eServiceVehicleMenuOptions.PrintDetails:
                        printVehicleDetails(i_ServiceTicket);
                        break;
                    case (int)eServiceVehicleMenuOptions.ChangeStatus:
                        changeVehicleServiceStatus(i_ServiceTicket);
                        break;
                    case (int)eServiceVehicleMenuOptions.InflateWheelsToMax:
                        inflateAirInWheelsToMax(i_ServiceTicket);
                        break;
                    case (int)eServiceVehicleMenuOptions.IncreaseEnergySupply:
                        increaseVehicleEnergySupply(i_ServiceTicket);
                        break;
                    case (int)eServiceVehicleMenuOptions.SelectOtherVehicle:
                        VehicleServiceTicket otherVehicle = getVehicleServiceTicket();
                        if (otherVehicle != null)
                        {
                            i_ServiceTicket = otherVehicle;
                            Console.WriteLine("Focus shifted to vehicle [{0}]", i_ServiceTicket.Vehicle.LicenseNumber);
                        }

                        break;
                    case (int)eServiceVehicleMenuOptions.BackToPreviousMenu:
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
            while (!back);
        }

        private void printServiceVehicleMenuOptions(Engine i_Engine)
        {
            Console.WriteLine("{0} - Show vehicle details", (int)eServiceVehicleMenuOptions.PrintDetails);
            Console.WriteLine("{0} - Change vehicle status", (int)eServiceVehicleMenuOptions.ChangeStatus);
            Console.WriteLine("{0} - Inflate vehicle wheels to maximum pressure", (int)eServiceVehicleMenuOptions.InflateWheelsToMax);
            if (i_Engine is ElectricEngine)
            {
                Console.WriteLine("{0} - Recharge vehicle battery", (int)eServiceVehicleMenuOptions.IncreaseEnergySupply);
            }
            else if (i_Engine is FuelEngine)
            {
                Console.WriteLine("{0} - Refuel vehicle", (int)eServiceVehicleMenuOptions.IncreaseEnergySupply);
            }

            Console.WriteLine();
            Console.WriteLine("{0} - Select other vehicle", (int)eServiceVehicleMenuOptions.SelectOtherVehicle);
            Console.WriteLine("{0} - Back", (int)eServiceVehicleMenuOptions.BackToPreviousMenu);
        }
    }
}
