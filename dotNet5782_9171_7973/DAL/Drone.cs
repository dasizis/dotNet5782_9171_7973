using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL.DO
{
    public struct Drone
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public WeightCategory MaxWeight { get; set; }

        public DroneStatus Status { get; set; }

        public double Battery { get; set; }

        public override string ToString()
        {
            return (
                $"Drone number {Id} information: \n" +
                $"Model: {Model} \n" +
                $"Weight: {MaxWeight} \t " +
                $"Status: {Status} \n" +
                $"Battery: {Battery}%"
            );
        }
    }
}
