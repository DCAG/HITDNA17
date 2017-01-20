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
                Console.WriteLine("{0,14}  {1}", vehicle.LicenseNumber, vehicle.GetType());
            }
        }
        private void insertVehicleForTreatment()
        {
            Console.WriteLine("This is a list of all vehicles that exist in the DB");
            Console.WriteLine();
            printAllVehiclesInDB();
            Console.WriteLine();
            Console.WriteLine("Select a vehicle to insert to the garage.");
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
                m_Manager.InsertVehicleForTreatment(serviceTicket);
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
                switch(selection)
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
            Console.WriteLine("License numbers with {0}",i_Status.HasValue?string.Format("status: {0}",i_Status):"any status");
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
            catch(InvalidCastException)
            {
                Console.WriteLine("Invalid option");
            }
        }

        private void inflateAirInWheelsToMax(VehicleServiceTicket i_ServiceTicket)
        {
            m_Manager.InflateAirInWheelsToMax(i_ServiceTicket);
        }

        private void increaseVehicleEnergySupply(VehicleServiceTicket i_ServiceTicket)
        {
            if (i_ServiceTicket.Vehicle.Engine is ElectricEngine)
            {
                recharge(i_ServiceTicket);
            }
            else if(i_ServiceTicket.Vehicle.Engine is FuelEngine)
            {
                refuel(i_ServiceTicket);
            }
        }

        private void refuel(VehicleServiceTicket i_ServiceTicket)
        {
            try
            {
                Console.WriteLine("Select Fuel Type:");
                ConsoleHelper.PrintEnum(typeof(eFuelType));
                string fuelTypeStr = Console.ReadLine();
                Console.WriteLine("Enter fuel amount (Liters):");
                string fuelAmountStr = Console.ReadLine();
                m_Manager.Refuel(i_ServiceTicket, (eFuelType)int.Parse(fuelTypeStr), float.Parse(fuelAmountStr));
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

        private void recharge(VehicleServiceTicket i_ServiceTicket)
        {
            try
            {
                Console.WriteLine("Enter fuel amount (Liters):");
                string rechargeAmountStr = Console.ReadLine();
                m_Manager.Recharge(i_ServiceTicket, float.Parse(rechargeAmountStr));
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
            if(serviceTicket != null)
            {
                ShowVehicleServiceMenu(serviceTicket);
            }
        }

        public void ShowMainMenu()
        {
            int selectedMenuOption = (int)eGarageMainMenuItems.None;
            bool exit = false;
            do
            {
                Console.WriteLine("{0}# Garage Manager #",Environment.NewLine);
                PrintMenuOptions();
                selectedMenuOption = ConsoleHelper.ReadIntInput();
                switch (selectedMenuOption)
                {
                    case (int)eGarageMainMenuItems.ListVehicles:
                        getVehicleLicenseList();
                        break;
                    case (int)eGarageMainMenuItems.NewVehicle:
                        insertVehicleForTreatment();
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

        private void PrintMenuOptions()
        {
            Console.WriteLine("{0} - Enter a new vehicle to service", (int)eGarageMainMenuItems.NewVehicle);
            Console.WriteLine("{0} - List vehicles in service", (int)eGarageMainMenuItems.ListVehicles);
            Console.WriteLine("{0} - Service a specific vehicle", (int)eGarageMainMenuItems.ServiceSpecificVehicle);
            Console.WriteLine("{0} - Exit", (int)eGarageMainMenuItems.Exit);
        }

        private void ShowVehicleServiceMenu(VehicleServiceTicket i_ServiceTicket)
        {
            int selectedMenuOption = (int)eServiceVehicleMenuOptions.None;
            bool back = false;
            do
            {
                Console.WriteLine("{0}# Garage Manager # > Service Vehicle [License Number: {1}]", Environment.NewLine, i_ServiceTicket.Vehicle.LicenseNumber);
                PrintServiceVehicleMenuOptions(i_ServiceTicket.Vehicle.Engine);
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

        private void PrintServiceVehicleMenuOptions(Engine i_Engine)
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
 