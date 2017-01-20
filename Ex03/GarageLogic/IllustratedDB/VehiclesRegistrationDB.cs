﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public class VehiclesRegistrationDB
    {
        private static List<Vehicle> m_Vehicles;
        public static List<Vehicle> Vehicles
        {
            get
            {
                return m_Vehicles;
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
            m_Vehicles.Add(new FuelCar(eCarColor.Red, 5,                         "014", "Honda 2017 Clarity Fuel Cell"/*, 88F*/));
            m_Vehicles.Add(new FuelCar(eCarColor.Black, 3,                       "013", "Jaguar F-TYPE S"/*,              60F*/));
            m_Vehicles.Add(new FuelCar(eCarColor.White, 5,                       "012", "Hyundai 2017 Elantra SE"/*,      45F*/));
            m_Vehicles.Add(new FuelCar(eCarColor.Silver, 5,                      "011", "Skoda OCTAVIA"/*,                41F*/));
            m_Vehicles.Add(new ElectricCar(eCarColor.Black, 5,                   "010", "BMW i3"/*,                       95F*/));
            m_Vehicles.Add(new ElectricCar(eCarColor.Silver, 3,                  "009", "COURB C-ZEN"/*,                  91F*/));
            m_Vehicles.Add(new ElectricCar(eCarColor.White, 5,                   "008", "Tesla Model X"/*,                30F*/));
            m_Vehicles.Add(new FuelMotorCycle(eMotorCycleLicenseType.B, 15,      "007", "Zero S ZF6.5"/*,                 20F*/));
            m_Vehicles.Add(new FuelMotorCycle(eMotorCycleLicenseType.A2, 20,     "006", "Lightning LS-218"/*,             60F*/));
            m_Vehicles.Add(new FuelMotorCycle(eMotorCycleLicenseType.A2, 20,     "005", "Lightning LS-204"/*,             95F*/));
            m_Vehicles.Add(new ElectricMotorCycle(eMotorCycleLicenseType.A, 10,  "004", "Electra 5F"/*,                   93F*/));
            m_Vehicles.Add(new ElectricMotorCycle(eMotorCycleLicenseType.A2, 10, "003", "Fusion o3P"/*,                   50F*/));
            const bool k_CarryDangerousChemicals = true;
            m_Vehicles.Add(new FuelTruck(!k_CarryDangerousChemicals, 30,         "002", "Iveco PowerStar 420 E5"/*,       52F*/));
            m_Vehicles.Add(new FuelTruck(k_CarryDangerousChemicals, 35,          "001", "Peterbilt 281 tanker"/*,         89F*/));
        }

        private static void createOwners()
        {
            m_Owners["014"] = new Owner("Adam Bertram",        "0507654314");
            m_Owners["013"] = new Owner("Bryce Dallas Howard", "0507654313");
            m_Owners["012"] = new Owner("Casey Neistat",       "0507654312");
            m_Owners["011"] = new Owner("Coyote Peterson",     "0507654311");
            m_Owners["010"] = new Owner("Conan O'Brien",       "0507654310");
            m_Owners["009"] = new Owner("Don Jones",           "0507654309");
            m_Owners["008"] = new Owner("Emma Stone",          "0507654308");
            m_Owners["007"] = new Owner("Felicia Day",         "0507654307");
            m_Owners["006"] = new Owner("Jeffrey Snover",      "0507654306");
            m_Owners["005"] = new Owner("LeBron James",        "0507654305");
            m_Owners["004"] = new Owner("Markiplier",          "0507654304");
            m_Owners["003"] = new Owner("Nicole Kidman",       "0507654303");
            m_Owners["002"] = new Owner("Rosana Pansino",      "0507654302");
            m_Owners["001"] = new Owner("Van Jones",           "0507654301");
        }

        public static Vehicle FindVehicle(string i_LicenseNumber)
        {
            Vehicle result = null;
            int index = Vehicles.IndexOf(new Vehicle(i_LicenseNumber));

            if (index >= 0)
            {
                result = Vehicles[index];
            }

            return result;
        }

        public static Owner? FindOwner(string i_LicenseNumber)
        {
            Owner? result = null;
            if(Owners.ContainsKey(i_LicenseNumber))
            {
                result = Owners[i_LicenseNumber];
            }
            return result;
        }
    }
}
