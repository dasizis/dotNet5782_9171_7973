using System;
using System.Collections.Generic;
using System.Text;
using IBL.BO;

namespace ConsoleUI_BL
{
    partial class Program
    {
        static Converter<string, int> IsValidEnumOption(int max)
        {
            return str =>
            {
                int result = int.Parse(str);
                if (result < max && result > 0) return result;
                throw new FormatException();
            };
        }

        static void AddBaseStation()
        {
            Console.WriteLine("Enter id, name, location (longitude, latitude), number of charge slots");
            
            int id = GetInput(int.Parse);
            string name = GetInput(s => s);
            double longitude = GetInput(double.Parse);
            double latitude = GetInput(double.Parse);
            int chargeSlots = GetInput(int.Parse);

            Bl.AddBaseStation(id, name, new Location { Latitude = latitude, Longitude = longitude }, chargeSlots);
        }

        static void AddCustomer()
        {
            Console.WriteLine("Enter id, name, phone, location (longitude, latitude)");

            int id = GetInput(int.Parse);
            string name = GetInput(s => s);            
            string phone = GetInput(s => s);
            double longitude = GetInput(double.Parse);
            double latitude = GetInput(double.Parse);

            Bl.AddCustomer(id, name, phone, new Location() { Longitude = longitude, Latitude = latitude });
        }

        static void AddParcel()
        {
            Console.WriteLine("Enter sender Id, target Id, weight (0 - 2), priority (0 - 2)");

            int senderId = GetInput(int.Parse);
            int targetId = GetInput(int.Parse);
            WeightCategory weight = (WeightCategory)GetInput(IsValidEnumOption(3));
            Priority priority = (Priority)GetInput(IsValidEnumOption(3));
            double longitude = GetInput(double.Parse);
            double latitude = GetInput(double.Parse);

            Bl.AddParcel(senderId, targetId, weight, priority);
        }

        static void AddDrone()
        {
            Console.WriteLine("Enter Id, model, max weight, station number");

            int id = GetInput(int.Parse);
            string model = GetInput(s => s);            
            WeightCategory weight = (WeightCategory)GetInput(IsValidEnumOption(3));
            int stationNumber = GetInput(int.Parse);

            Bl.AddDrone(id, model, weight, stationNumber);
        }
    }
}
