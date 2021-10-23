using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace ConsuleUI
{
    partial class Program
    {
        public static void ShowDetailsTitle()
        {
            printHeader("Enter Details, Please.");
        }

        public static void AddBaseStation()
        {
            ShowDetailsTitle();
            Console.WriteLine("-name, -longitude, -latitude, -number of charge slots");
            string name = Console.ReadLine();
            double.TryParse(Console.ReadLine(), out double longitude);
            double.TryParse(Console.ReadLine(), out double latitude);
            int chargeSlots = getInput();

            dalObject.AddBaseStation(name, longitude, latitude, chargeSlots);
        }

        public static void AddParcel()
        {
            ShowDetailsTitle();
            Console.WriteLine("-sender ID, -target ID, -whight (0-2), -priority (0-2)");
            int senderId = getInput();
            int targetId = getInput();
            WeightCategory weight = (WeightCategory)getInput();
            Priority priority = (Priority)getInput();

            dalObject.AddParcel(senderId, targetId, weight, priority);
        }

        public static void AddCustomer()
        {
            ShowDetailsTitle();
            Console.WriteLine("-name, -longitude, -latitude, -phone number");
            string name = Console.ReadLine();
            double.TryParse(Console.ReadLine(), out double longitude);
            double.TryParse(Console.ReadLine(), out double latitude);
            string phone = Console.ReadLine();

            dalObject.AddCustomer(name, longitude, latitude, phone);
        }

        public static void AddDrone()
        {
            ShowDetailsTitle();
            Console.WriteLine("-model, -weight, -status");
            string model = Console.ReadLine();
            WeightCategory weight = (WeightCategory)getInput();
            DroneStatus status = (DroneStatus)getInput();

            dalObject.AddDrone(model, weight, status);
        }
    }
}
