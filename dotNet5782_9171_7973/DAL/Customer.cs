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

        /// <summary>
        /// creates a random Customer instance
        /// </summary>
        /// <param name="id">the instance id</param>
        /// <returns>the created Customer instance</returns>
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
                $"*********************************************\n" +
                $"Customer #{Id} information\n" +
                $"---------------------------------------------\n" +
                $"Name: {Name}\n" +
                $"Phone number: {Phone} \n" +
                $"Location: \n" +
                $"-----Longitude: {DalObject.DalObject.coordinate.CastFromNum(Longitude)} \t " +
                $"-----Latitude: {DalObject.DalObject.coordinate.CastFromNum(Latitude)} \n" +
                $"*********************************************"
            );
        }
    }
}
