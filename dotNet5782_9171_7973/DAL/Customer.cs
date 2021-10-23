using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL.DO
{
    public struct Customer
    {
        double longitude, latitude;
        string phone;
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone {
            get
            {
                return phone;
            }

            set
            {
                if(value.Length != 10)
                    throw new ArgumentException("Invalid Phone Number");
                phone = value;
            }
        }

        public double Longitude
        {
            get
            {
                return longitude;
            }
            set
            {
                if (value < 0 || value > 180)
                    throw new ArgumentException("Invalid Longitude values");
                longitude = value;
            }
        }

        public double Latitude
        {
            get
            {
                return latitude;
            }
            set
            {
                if (value < 0 || value > 180)
                    throw new ArgumentException("Invalid Latitude values");
                latitude = value;
            }
        }


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
                Longitude = DAL.RandomManager.Rand.NextDouble() % 180,
                Latitude = DAL.RandomManager.Rand.NextDouble() % 180,
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
                $"-----Longitude: {DalObject.DalObject.coordinate.CoordinateCast(Longitude)} \t " +
                $"-----Latitude: {DalObject.DalObject.coordinate.CoordinateCast(Latitude)} \n" +
                $"  * ********************************************"
            );
        }

        
    }
}
