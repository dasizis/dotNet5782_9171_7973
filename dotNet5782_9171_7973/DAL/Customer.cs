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
        /// Return a new customer with random values
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Print customer data in a nice format
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return (
                $"*********************************************\n" +
                $"Base Station #{Id} information: \n" +
                $"Name: {Name} \n" +
                $"Phone number: {Phone} \n" +
                $"Location:" +
                $"-----Longitude: {Longitude} \t " +
                $"-----Latitude: {Latitude} \n" +
                $"  * ********************************************"
            );
        }

        
    }
}
