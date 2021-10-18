using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL.DO
{
    public struct Parcel
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public int TargetId { get; set; }

        public WeightCategory Weight { get; set; }

        public Priority Priority { get; set; }

        public DateTime Requested { get; set; }

        public int DroneId { get; set; }

        public DateTime Scheduled { get; set; }

        public DateTime PickedUp { get; set; }


        public DateTime Delivered { get; set; }

        public override string ToString()
        {
            return (
                $"Parcel number {Id} information: \n" +
                $"Sender: {SenderId} \t Target: {TargetId} \n" +
                $"Whight: {Weight} \t Priority: {Priority} \n " +
                $"Drone in charge: {DroneId} \n" +
                $"====Relevant dates:====\n" +
                $"Requested: {Requested} \n" +
                $"Scheduled: {Scheduled} \n" +
                $"Delivered: {Delivered} \n" +
                $"Picked up: {PickedUp}\n"
            );
        }
    }
}
