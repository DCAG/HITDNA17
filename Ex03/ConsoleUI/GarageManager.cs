using System;
using System.Collections.Generic;

public class GarageManager
{
    private List<Vehicle> m_Vehicles;
    private Dictionary<Vehicle, VehicleTreatment> m_VehiclesInService;

    public GarageManager()
    {
        CreateVehiclesToRoamTheRoads();
    }

    private void CreateVehiclesToRoamTheRoads()
    {
        

        foreach(Vehicle v in m_Vehicles)
        {
            Console.WriteLine(v);
        }
        Console.ReadLine();
    }

	public void InsertVehicleForTreatment(string i_License)
	{
        int index = m_Vehicles.IndexOf(new Vehicle(i_License));
        if(index >= 0)
        {
            m_VehiclesInService.Add(m_Vehicles[index], new VehicleTreatment());
        }
        m_VehiclesInService[m_Vehicles[]] = new VehicleTreatment();

    }

	public virtual void GetVehicleLicenseList(eVehicleTreatmentStatus i_Status)
	{
        Console.WriteLine("All vehicles in ststus {0}", i_Status);
        foreach(VehicleTreatment treatment in m_VehiclesInService)
        {
            if(treatment.Status == i_Status)
            {
                Console.WriteLine(treatment.Vehicle.LicenseNumber);
            }
        }
	}

	public void ChangeVehicleStatus(string i_LicenseNumber, eVehicleTreatmentStatus i_NewStatus)
	{
        Vehicle vehicle = new Vehicle(i_LicenseNumber);
        
        if (vehicle == null)
        {
            Console.WriteLine("Vehicle with License Number {0} was not found", i_LicenseNumber);
        }
        Console.WriteLine("Changng treatment status on vehicle {0} from {1} to {2}",VehicleTreatment);
        Console.WriteLine("Done!");
	}

	public void InflateAirInWheelsToMax(string i_License)
	{
		
	}

    private void printFuelType()
    {
        Console.WriteLine("{0} - {1}", (int)eFuelType.Octan95, eFuelType.Octan95);
        Console.WriteLine("{0} - {1}", (int)eFuelType.Octan96, eFuelType.Octan96);
        Console.WriteLine("{0} - {1}", (int)eFuelType.Octan98, eFuelType.Octan95);
        Console.WriteLine("{0} - {1}", (int)eFuelType.Soler, eFuelType.Soler);
    }

    public void RefuelA()
    {
        try
        {
            Console.WriteLine("Enter license number:");
            string licenseNumberStr = Console.ReadLine();
            Console.WriteLine("Select Fuel Type:");
            printFuelType();
            string fuelTypeStr = Console.ReadLine();
            Console.WriteLine("Enter fuel amount:");
            string fuelAmountStr = Console.ReadLine();
            Refuel(licenseNumberStr, (eFuelType)int.Parse(fuelTypeStr), float.Parse(fuelAmountStr));
        }
        catch(FormatException)
        {
            Console.WriteLine("Your input is not valid");
        }
        catch(ArgumentException)
        {
            Console.WriteLine("Wrong argument");
        }
        catch(ValueOutOfRangeException)
        {
            Console.WriteLine("Value was out of range");
        }
    }

	public void Refuel(string i_License, eFuelType i_FuelType, float i_Amount)
	{
        

    }

	public void Recharge(string i_License, float i_MinutesToCharge)
	{
		
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
            switch(selectedMenuOption)
            {
                case eGarageMainMenuItems.ListVehicles:
                    foreach (object status in Enum.GetValues(typeof(eVehicleTreatmentStatus)))
                    {
                        Console.WriteLine("{0} - {1}", status, status.ToString());
                    }
                    Console.WriteLine("select status");
                    string selectedStatus = Console.ReadLine();
                    GetVehicleLicenseList((eVehicleTreatmentStatus)int.Parse(selectedStatus));
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

