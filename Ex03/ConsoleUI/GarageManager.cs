using System;
using System.Collections.Generic;

public class GarageManager
{
    private List<Vehicle> m_Vehicles;

    public GarageManager()
    {
        CreateVehiclesToRoamTheRoads();
    }

    private void CreateVehiclesToRoamTheRoads()
    {
        const bool k_CarryDangerousChemicals = true;
        m_Vehicles = new List<Vehicle>();
        m_Vehicles.Add(new FuelCar(eCarColor.Red, 5, "0000014", "Honda 2017 Clarity Fuel Cell"/*, 88F*/));
        m_Vehicles.Add(new FuelCar(eCarColor.Black, 3, "0000013", "Jaguar F-TYPE S"/*, 60F*/));
        m_Vehicles.Add(new FuelCar(eCarColor.White, 5, "0000012", "Hyundai 2017 Elantra SE"/*, 45F*/));
        m_Vehicles.Add(new FuelCar(eCarColor.Silver, 5, "0000011", "Skoda OCTAVIA"/*, 41F*/));
        m_Vehicles.Add(new ElectricCar(eCarColor.Black,  5, "0000010", "BMW i3"/*, 95F*/));
        m_Vehicles.Add(new ElectricCar(eCarColor.Silver, 3, "0000009", "COURB C-ZEN"/*, 91F*/));
        m_Vehicles.Add(new ElectricCar(eCarColor.White,  5, "0000008", "Tesla Model X"/*, 30F*/));
        m_Vehicles.Add(new FuelMotorCycle(eMotorCycleLicenseType.B, 15, "0000007", "Zero S ZF6.5"/*, 20F*/));
        m_Vehicles.Add(new FuelMotorCycle(eMotorCycleLicenseType.A2, 20, "000006", "Lightning LS-218"/*, 60F*/));
        m_Vehicles.Add(new FuelMotorCycle(eMotorCycleLicenseType.A2, 20, "000005", "Lightning LS-204"/*, 95F*/));
        m_Vehicles.Add(new ElectricMotorCycle(eMotorCycleLicenseType.A, 10, "0000004", ""/*, 93F*/));
        m_Vehicles.Add(new ElectricMotorCycle(eMotorCycleLicenseType.A2, 10, "0000003", ""/*, 50F*/));
        m_Vehicles.Add(new FuelTruck(!k_CarryDangerousChemicals, 30,  "0000002", "Iveco PowerStar 420 E5"/*, 52F*/));
        m_Vehicles.Add(new FuelTruck(k_CarryDangerousChemicals, 35, "0000001", " Peterbilt 281 tanker"/*, 89F*/));
        foreach(Vehicle v in m_Vehicles)
        {
            Console.WriteLine(v);
        }
        Console.ReadLine();
    }

    private List<VehicleTreatment> m_VehiclesInService;

	public virtual void InsertVehicleForTreatment(string i_License)
	{
        //VehicleTreatment vehicleTreatment = VehicleTreatment.InsertVehicleToService();
        //m_CarsInService

    }

	public virtual void GetVehicleLicenseList(eVehicleTreatmentStatus i_Status)
	{
		
	}

	public virtual void ChangeVehicleStatus(string i_License, eVehicleTreatmentStatus i_NewStatus)
	{
		
	}

	public virtual void InflateAirInWheelsToMax(string i_License)
	{
		
	}

	public virtual void Refuel(string i_License, eFuelType i_FuelType, float i_Amount)
	{
		
	}

	public virtual void Recharge(string i_License, float i_MinutesToCharge)
	{
		
	}

	public virtual void PrintVehicleDetails(string i_License)
	{
		
	}

	public virtual void Start()
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

	public virtual void PrintMenu()
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

