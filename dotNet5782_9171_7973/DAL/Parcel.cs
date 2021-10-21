using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL.DO
{
    public struct Parcel
    {
        public int DroneId { get; set; }

        public int Id { get; set; }

        public int SenderId { get; set; }

        public int TargetId { get; set; }

        public WeightCategory Weight { get; set; }

        public Priority Priority { get; set; }

        public DateTime Requested { get; set; }

        public DateTime Scheduled { get; set; }

        public DateTime PickedUp { get; set; }

        public DateTime Delivered { get; set; }

        public static Parcel Random(int id)
        {
            return new Parcel()
            {
                Id = id,
                Model = DAL.RandomManager.RandomName(),
                Status = (DroneStatus)DAL.RandomManager.RandomEnumOption(typeof(DroneStatus)),
                MaxWeight = (WeightCategory)DAL.RandomManager.RandomEnumOption(typeof(WeightCategory)),
                Battery = DAL.RandomManager.Rand.NextDouble() * 100,
            };
        }


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
