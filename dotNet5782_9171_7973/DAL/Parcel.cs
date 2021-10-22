using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL.DO
{
    public struct Parcel
    {
        WeightCategory weight;
        Priority priority;
        public int DroneId { get; set; }

        public int Id { get; set; }

        public int SenderId { get; set; }

        public int TargetId { get; set; }

        public WeightCategory Weight
        {
            get
            {
                return weight;
            }

            set
            {
                if ((int)value < 0 || (int)value > 2)
                    throw new ArgumentException("Invalid Weight Value.");
                weight = value;

            }
        }

        public Priority Priority
        {
            get
            {
                return priority;
            }

            set
            {
                if ((int)value < 0 || (int)value > 2)
                    throw new ArgumentException("Invalid Priority Value.");
                priority = value;

            }
        }

        public DateTime Requested { get; set; }

        public DateTime Scheduled { get; set; }

        public DateTime PickedUp { get; set; }

        public DateTime Delivered { get; set; }

        public static Parcel Random(int id)
        { 
            return new Parcel()
            {
                Id = id,
                Requested = DAL.RandomManager.RandomDate(),
                Weight = (WeightCategory)DAL.RandomManager.RandomEnumOption(typeof(WeightCategory)),
                Priority = (Priority)DAL.RandomManager.RandomEnumOption(typeof(Priority)),
                SenderId = DAL.RandomManager.Rand.Next(),
                TargetId = DAL.RandomManager.Rand.Next(),
                DroneId = 0,

                //TODO add dates of several positions
            };
        }

        /// <summary>
        /// Print parcel information
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return (
                $"**********************************\n"+
                $"Parcel #{Id} information: \n" +
                $"Sender: {SenderId} \t Target: {TargetId} \n" +
                $"Whight: {Weight} \t Priority: {Priority} \n " +
                $"Drone in charge: {DroneId} \n" +
                $"====Relevant dates:====\n" +
                $"Requested: {Requested} \n" +
                $"Scheduled: {Scheduled} \n" +
                $"Delivered: {Delivered} \n" +
                $"Picked up: {PickedUp}\n"+
                $"**********************************\n"
            );
        }
    }
}
