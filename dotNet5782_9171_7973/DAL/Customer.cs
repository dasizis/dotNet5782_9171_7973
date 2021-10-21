using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL.DO
{
    public struct Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public static Customer Random(int id)
        {
            return new Customer()
            {
                Id = id,
                Name = DAL.RandomManager.RandomFullName(),
                Longitude = DAL.RandomManager.Rand.NextDouble() * 100,
                Latitude = DAL.RandomManager.Rand.NextDouble() * 100,
                Phone = DAL.RandomManager.RandomPhone(),
            };
        }

        public override string ToString()
        {
            return (
                $"Base Station number {Id} information: \n" +
                $"Name: {Name} \n" +
                $"Phone number: {Phone} \n" +
                $"Location: Longitude: {Longitude} \t " +
                $"Latitude: {Latitude} \n"
            );
        }
    }
}
