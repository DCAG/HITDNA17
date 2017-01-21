using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public class VehiclesRegistrationDB
    {
        private static List<Vehicle> s_Vehicles;

        public static List<Vehicle> Vehicles
        {
            get
            {
                return s_Vehicles;
            }
        }

        private static Dictionary<string, Owner> s_Owners;

        public static Dictionary<string, Owner> Owners
        {
            get
            {
                return s_Owners;
            }
        }

        static VehiclesRegistrationDB()
        {
            s_Vehicles = new List<Vehicle>();
            createVehicles();

            s_Owners = new Dictionary<string, Owner>();
            createOwners();
        }

        private static void createVehicles()
        {
            s_Vehicles.Add(new FuelCar(eCarColor.Red, 5, "014", "Honda 2017 Clarity Fuel Cell"));
            s_Vehicles.Add(new FuelCar(eCarColor.Black, 3, "013", "Jaguar F-TYPE S"));
            s_Vehicles.Add(new FuelCar(eCarColor.White, 5, "012", "Hyundai 2017 Elantra SE"));
            s_Vehicles.Add(new FuelCar(eCarColor.Silver, 5, "011", "Skoda OCTAVIA"));
            s_Vehicles.Add(new ElectricCar(eCarColor.Black, 5, "010", "BMW i3"));
            s_Vehicles.Add(new ElectricCar(eCarColor.Silver, 3, "009", "COURB C-ZEN"));
            s_Vehicles.Add(new ElectricCar(eCarColor.White, 5, "008", "Tesla Model X"));
            s_Vehicles.Add(new FuelMotorCycle(eMotorCycleLicenseType.B, 15, "007", "Zero S ZF6.5"));
            s_Vehicles.Add(new FuelMotorCycle(eMotorCycleLicenseType.A2, 20, "006", "Lightning LS-218"));
            s_Vehicles.Add(new FuelMotorCycle(eMotorCycleLicenseType.A2, 20, "005", "Lightning LS-204"));
            s_Vehicles.Add(new ElectricMotorCycle(eMotorCycleLicenseType.A, 10, "004", "Electra 5F"));
            s_Vehicles.Add(new ElectricMotorCycle(eMotorCycleLicenseType.A2, 10, "003", "Fusion o3P"));
            const bool k_CarryDangerousChemicals = true;
            s_Vehicles.Add(new FuelTruck(!k_CarryDangerousChemicals, 30, "002", "Iveco PowerStar 420 E5"));
            s_Vehicles.Add(new FuelTruck(k_CarryDangerousChemicals, 35, "001", "Peterbilt 281 tanker"));
        }

        private static void createOwners()
        {
            s_Owners["014"] = new Owner("Adam Bertram", "0507654314");
            s_Owners["013"] = new Owner("Bryce Dallas Howard", "0507654313");
            s_Owners["012"] = new Owner("Casey Neistat", "0507654312");
            s_Owners["011"] = new Owner("Coyote Peterson", "0507654311");
            s_Owners["010"] = new Owner("Conan O'Brien", "0507654310");
            s_Owners["009"] = new Owner("Don Jones", "0507654309");
            s_Owners["008"] = new Owner("Emma Stone", "0507654308");
            s_Owners["007"] = new Owner("Felicia Day", "0507654307");
            s_Owners["006"] = new Owner("Jeffrey Snover", "0507654306");
            s_Owners["005"] = new Owner("LeBron James", "0507654305");
            s_Owners["004"] = new Owner("Markiplier", "0507654304");
            s_Owners["003"] = new Owner("Nicole Kidman", "0507654303");
            s_Owners["002"] = new Owner("Rosana Pansino", "0507654302");
            s_Owners["001"] = new Owner("Van Jones", "0507654301");
        }

        public static Vehicle FindVehicle(string i_LicenseNumber)
        {
            Vehicle result = null;
            int index = s_Vehicles.IndexOf(new Vehicle(i_LicenseNumber));

            if (index >= 0)
            {
                result = s_Vehicles[index];
            }

            return result;
        }

        public static Owner? FindOwner(string i_LicenseNumber)
        {
            Owner? result = null;
            if(s_Owners.ContainsKey(i_LicenseNumber))
            {
                result = s_Owners[i_LicenseNumber];
            }

            return result;
        }
    }
}
