using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL.DO
{
    public struct BaseStation
    {
        double longitude, latitude;
        int chargeSlots;
        public int Id { get; set; }

        public string Name { get; set; }

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

        public int ChargeSlots
        {
            get
            {
                return chargeSlots;
            }

            set
            {
                if (value <= 0)
                    throw new ArgumentException("There Owe To be At List One ChargeSlot");
                chargeSlots = value;
            }
        }
        /// <summary>
        /// creates a random BaseStation instance
        /// </summary>
        /// <param name="id">the instance id</param>
        /// <returns>the created BaseStation instance</returns>
        public static BaseStation Random(int id)
        {
            return new BaseStation()
            {
                Id          = id,
                Name        = DAL.RandomManager.RandomName(),
                Longitude   = DAL.RandomManager.Rand.NextDouble() * 100,
                Latitude    = DAL.RandomManager.Rand.NextDouble() * 100,
                ChargeSlots = DAL.RandomManager.Rand.Next(1, 10),
            };
        }

        public override string ToString()
        {
            return (
                $"*********************************************\n" +
                $"Base Station #{Id} information\n" +
                $"---------------------------------------------\n" +
                $"Name: {Name}\n" +
                $"Location: \n" +
                 $"-----Longitude: {DalObject.DalObject.coordinate.CastFromNum(Longitude)}\n" +
                $"-----Latitude : {DalObject.DalObject.coordinate.CastFromNum(Latitude)} \n" +
                $"Charge Slots: {ChargeSlots} slot(s).\n" +
                $"*********************************************"
            );
        }
    }
}
