using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    class VehiclesRegistrationDB
    {
        private static List<Vehicle> m_Vehicles;
        public static List<Vehicle> Vehicles
        {
            get
            {
                return Vehicles;
            }
        }

        private static Dictionary<string, Owner> m_Owners;
        public static Dictionary<string, Owner> Owners
        {
            get
                {
                return m_Owners;
            }
        }

        static VehiclesRegistrationDB()
        {
            m_Vehicles = new List<Vehicle>();
            createVehicles();

            m_Owners = new Dictionary<string, Owner>();
            createOwners();
        }

        private static void createVehicles()
        {
            m_Vehicles.Add(new FuelCar(eCarColor.Red, 5, "0000014", "Honda 2017 Clarity Fuel Cell"/*, 88F*/));
            m_Vehicles.Add(new FuelCar(eCarColor.Black, 3, "0000013", "Jaguar F-TYPE S"/*, 60F*/));
            m_Vehicles.Add(new FuelCar(eCarColor.White, 5, "0000012", "Hyundai 2017 Elantra SE"/*, 45F*/));
            m_Vehicles.Add(new FuelCar(eCarColor.Silver, 5, "0000011", "Skoda OCTAVIA"/*, 41F*/));
            m_Vehicles.Add(new ElectricCar(eCarColor.Black, 5, "0000010", "BMW i3"/*, 95F*/));
            m_Vehicles.Add(new ElectricCar(eCarColor.Silver, 3, "0000009", "COURB C-ZEN"/*, 91F*/));
            m_Vehicles.Add(new ElectricCar(eCarColor.White, 5, "0000008", "Tesla Model X"/*, 30F*/));
            m_Vehicles.Add(new FuelMotorCycle(eMotorCycleLicenseType.B, 15, "0000007", "Zero S ZF6.5"/*, 20F*/));
            m_Vehicles.Add(new FuelMotorCycle(eMotorCycleLicenseType.A2, 20, "000006", "Lightning LS-218"/*, 60F*/));
            m_Vehicles.Add(new FuelMotorCycle(eMotorCycleLicenseType.A2, 20, "000005", "Lightning LS-204"/*, 95F*/));
            m_Vehicles.Add(new ElectricMotorCycle(eMotorCycleLicenseType.A, 10, "0000004", "Electra 5F"/*, 93F*/));
            m_Vehicles.Add(new ElectricMotorCycle(eMotorCycleLicenseType.A2, 10, "0000003", "Fusion o3P"/*, 50F*/));
            const bool k_CarryDangerousChemicals = true;
            m_Vehicles.Add(new FuelTruck(!k_CarryDangerousChemicals, 30, "0000002", "Iveco PowerStar 420 E5"/*, 52F*/));
            m_Vehicles.Add(new FuelTruck(k_CarryDangerousChemicals, 35, "0000001", "Peterbilt 281 tanker"/*, 89F*/));
        }

        private static void createOwners()
        {
            m_Owners["0000014"] = new Owner("Adam Bertram", "0507654314");
            m_Owners["0000013"] = new Owner("Bryce Dallas Howard", "0507654313");
            m_Owners["0000012"] = new Owner("Casey Neistat", "0507654312");
            m_Owners["0000011"] = new Owner("Coyote Peterson", "0507654311");
            m_Owners["0000010"] = new Owner("Conan O'Brien", "0507654310");
            m_Owners["0000009"] = new Owner("Don Jones", "0507654309");
            m_Owners["0000008"] = new Owner("Emma Stone", "0507654308");
            m_Owners["0000007"] = new Owner("Felicia Day", "0507654307");
            m_Owners["0000006"] = new Owner("Jeffrey Snover", "0507654306");
            m_Owners["0000005"] = new Owner("LeBron James", "0507654305");
            m_Owners["0000004"] = new Owner("Markiplier", "0507654304");
            m_Owners["0000003"] = new Owner("Nicole Kidman", "0507654303");
            m_Owners["0000002"] = new Owner("Rosana Pansino", "0507654302");
            m_Owners["0000001"] = new Owner("Van Jones", "0507654301");
        }
    }
}
