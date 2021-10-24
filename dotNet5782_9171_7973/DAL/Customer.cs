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

        public string Phone
        {
            get
            {
                return phone;
            }

            set
            {
                if (value.Length != 10)
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
