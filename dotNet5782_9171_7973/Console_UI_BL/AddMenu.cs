using System;
using System.Collections.Generic;
using System.Text;
using BO;

namespace ConsoleUI_BL
{
    partial class Program
    {
        static void AddBaseStation()
        {
            Console.WriteLine("Enter id, name, location (longitude, latitude), number of charge slots");
            
            int id = GetInput(int.Parse);
            string name = GetInput(s => s, Validation.IsValidName);
            double longitude = GetInput(double.Parse, Validation.IsValidLongitude);
            double latitude = GetInput(double.Parse, Validation.IsValidLatitude);
            int chargeSlots = GetInput(int.Parse, input => input > 0);

            bl.AddBaseStation(id, name, longitude, latitude, chargeSlots);
        }

        static void AddCustomer()
        {
            Console.WriteLine("Enter id, name, phone, location (longitude, latitude)");

            int id = GetInput(int.Parse);
            string name = GetInput(s => s, Validation.IsValidName);            
            string phone = GetInput(s => s, Validation.IsValidPhone);
            double longitude = GetInput(double.Parse, Validation.IsValidLongitude);
            double latitude = GetInput(double.Parse, Validation.IsValidLatitude);

            bl.AddCustomer(id, name, phone, longitude, latitude);
        }

        static void AddParcel()
        {
            Console.WriteLine("Enter sender Id, target Id, weight (0 - 2), priority (0 - 2), Location(longitude, latitude)");

            int senderId = GetInput(int.Parse);
            int targetId = GetInput(int.Parse);
            WeightCategory weight = (WeightCategory)GetInput(int.Parse, Validation.IsValidEnumOption<WeightCategory>);
            Priority priority = (Priority)GetInput(int.Parse, Validation.IsValidEnumOption<Priority>);
            double longitude = GetInput(double.Parse, Validation.IsValidLongitude);
            double latitude = GetInput(double.Parse, Validation.IsValidLatitude);

            bl.AddParcel(senderId, targetId, weight, priority);
        }

        static void AddDrone()
        {
            Console.WriteLine("Enter Id, model, max weight, station number");

            int id = GetInput(int.Parse);
            string model = GetInput(s => s);            
            WeightCategory weight = (WeightCategory)GetInput(int.Parse, Validation.IsValidEnumOption<WeightCategory>);
            int stationNumber = GetInput(int.Parse);

            bl.AddDrone(id, model, weight, stationNumber);
        }
    }
}
