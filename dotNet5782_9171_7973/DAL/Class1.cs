using System;
using IDAL.DO;

 namespace IDAL
{
    namespace DO
    {
        public enum WeightCategories
        {
            LOW,
            MEDIUM,
            HEAVY,
        }

        public enum DroneStatuses
        {
            FREE,
            MEINTENENCE,
            DELIVER,

        }

        public enum Priorities
        {
            REGULAR,
            FAST,
            URGENT,
        }

       
        public struct BaseStation
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public int ChargeSlots { get; set; }

            public override string ToString()
            {
                return(
                    $"Base Station number {Id} information: /n" +
                    $"Name: {Name} /n" +
                    $"Location: Longitude: {Longitude} /t " +
                    $"Latitude: {Latitude} /n" +
                    $"ChargeSlots: {ChargeSlots} slots."
                );
            }

        }

        public struct DroneCharge
        {
            public int StationId { get; set; }
            public int DroneId { get; set; }

            public override string ToString()
            {
                return (
                    $"Drone charge in station number  {StationId} " +
                    $"charges drone number {DroneId} /n"                   
                );
            }
        }

        public struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public DroneStatuses Status { get; set; }
            public double Battery { get; set; }

            public override string ToString()
            {
                return (
                    $"Drone number {Id} information: /n" +
                    $"Model: {Model} /n" +
                    $"Weight: {MaxWeight} /t " +
                    $"Status: {Status} /n" +
                    $"Battery: {Battery}%"
                );
            }
        }

        public struct Parcel
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public DateTime Requested { get; set; }
            public int DroneId { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }

            public override string ToString()
            {
                return (
                    $"Parcel number {Id} information: /n" +
                    $"Sender: {SenderId} /t Target: {TargetId} /n" +
                    $"Whight: {Weight} /t Priority: {Priority} /n " +
                    $"Drone in charge: {DroneId} /n" +
                    $"====Relevant dates:====/n" +
                    $"Requested: {Requested} /n" +
                    $"Scheduled: {Scheduled} /n" +
                    $"Delivered: {Delivered} /n" +
                    $"Picked up: {PickedUp}/n"
                );
            }
        }

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
                    $"Base Station number {Id} information: /n" +
                    $"Name: {Name} /n" +
                    $"Phone number: {Phone} /n" +
                    $"Location: Longitude: {Longitude} /t " +
                    $"Latitude: {Latitude} /n" 
                   
                );
            }
        }
    }
}

namespace DalObject
{
    public class DataSource
    {
        const int DRONES = 10;
        const int BASESTATION = 5;
        const int CUSTOMERS = 100;
        const int PARCELS = 1000;

        internal static Drone[] drones = new Drone[DRONES];
        internal static BaseStation[] baseStations = new BaseStation[BASESTATION];
        internal static Customer[] customers = new Customer[CUSTOMERS];
        internal static Parcel[] parcels = new Parcel[PARCELS];

        internal class Config
        {
            internal static int FreeDrone = 0;
            internal static int FreeStation = 0;
            //internal static int FreeCustomer = 0;
            //internal static int FreeParcel = 0;
            internal static int IdForParcel = 0;

            



        }

        //TODO
        internal static void Initialize() { }
    }

    public class DalObject
    {
        DalObject()
        {
            DataSource.Initialize();
        }

        //adding methods




    }
}
