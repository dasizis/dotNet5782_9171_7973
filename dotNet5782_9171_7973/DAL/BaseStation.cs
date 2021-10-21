using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL.DO
{
    public struct BaseStation
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public int ChargeSlots { get; set; }

        /// <summary>
        /// Return a new base station with random values
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Print base station in a nice format
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return (
                $"*********************************************\n" +
                $"Base Station #{Id} information\n" +
                $"---------------------------------------------\n" +
                $"Name: {Name}\n" +
                $"Location: \n" +
                $"-----Longitude: {Longitude}\n" +
                $"-----Latitude : {Latitude} \n" +
                $"Charge Slots: {ChargeSlots} slot(s).\n" +
                $"*********************************************"
            );
        }
    }
}
