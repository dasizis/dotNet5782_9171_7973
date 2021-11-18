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

            Bl.AddBaseStation(
                new BaseStation()
                {
                    Id = id,
                    Name = name,
                    Location = new Location() { Longitude = longitude, Latitude = latitude },
                    EmptyChargeSlots = 0,
                }
            );
        }

        static void AddCustomer()
        {
            Console.WriteLine("Enter id, name, phone, location (longitude, latitude)");

            int id = GetInput(int.Parse);
            string name = GetInput(s => s);            
            string phone = GetInput(s => s);
            double longitude = GetInput(double.Parse);
            double latitude = GetInput(double.Parse);

            Bl.AddCustomer(
                new Customer()
                {
                    Id = id,
                    Name = name,
                    Phone = phone,
                    Location = new Location() { Longitude = longitude, Latitude = latitude },
                }
            );
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

            Bl.AddParcel(
                new Parcel()
                {
                    Sender = senderId,
                    Target = target
                }
            );
        }

        static void AddDrone()
        {
            Console.WriteLine("Enter Id, model, max weight, station number");

            int id = GetInput(int.Parse);
            string model = GetInput(s => s);            
            WeightCategory weight = (WeightCategory)GetInput(IsValidEnumOption(3));
            int stationNumber = GetInput(int.Parse);

            Bl.AddDrone(
                new Drone()
                {
                    Id = id,
                    Model = model,
                    MaxWeight = weight,
                }
            );
        }
    }
}
