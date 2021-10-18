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

        public override string ToString()
        {
            return (
                $"Base Station number {Id} information: /n" +
                $"Name: {Name} /n" +
                $"Location: Longitude: {Longitude} /t " +
                $"Latitude: {Latitude} /n" +
                $"ChargeSlots: {ChargeSlots} slots."
            );
        }
    }
}
