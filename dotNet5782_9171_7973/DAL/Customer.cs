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
