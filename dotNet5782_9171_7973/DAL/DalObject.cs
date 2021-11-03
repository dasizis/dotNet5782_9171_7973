using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using IDAL.DO;
using System.Linq;


namespace DalObject
{
    public partial class DalObject
    {
        public static DAL.Coordinate coordinate => new DAL.Coordinate();

        //------------Adding functions----------

        

        


        

        // ----------- Update functions ---------
        /// <summary>
        /// assigns parcel to suitable drone
        /// </summary>
        /// <param name="parcelId"></param>
        public void AssignParcelToDrone(int parcelId)
        {
            Parcel parcel = DataSource.parcels.First(item => item.Id == parcelId);

            Drone drone = DataSource.drones.FirstOrDefault(
               drone => (drone.Status == DroneStatus.Free)
               && (parcel.Weight <= drone.MaxWeight)
               );
            if (drone.Equals(default))
            {
                Console.WriteLine("No Suitable Drone Found");
                return;
            }

            DataSource.parcels.Remove(parcel);
            DataSource.drones.Remove(drone);

            parcel.DroneId = drone.Id;
            drone.Status = DroneStatus.Deliver;
            parcel.Scheduled = DateTime.Now;

            DataSource.parcels.Add(parcel);
            DataSource.drones.Add(drone);

        }

        /// <summary>
        /// supplys parcel to customer
        /// </summary>
        /// <param name="parcelId"></param>
        public void SupplyParcel(int parcelId)
        {

            Parcel parcel = DataSource.parcels.First(item => item.Id == parcelId);
            if (parcel.DroneId == 0 || parcel.PickedUp == new DateTime())
                throw new Exception("Parcel Is Not In Supplying step.");

            Drone drone = DataSource.drones.Find(d => d.Id == parcel.DroneId);

            DataSource.drones.Remove(drone);
            DataSource.parcels.Remove(parcel);

            parcel.Delivered = DateTime.Now;
            drone.Status = DroneStatus.Free;

            DataSource.parcels.Add(parcel);
            DataSource.drones.Add(drone);

        }

        

        

        /// <summary>
        /// releases drone from charging
        /// </summary>
        /// <param name="droneId"></param>
        public void FinishCharging(int droneId)
        {
            Drone drone = DataSource.drones.First(drone => drone.Id == droneId);
            DataSource.drones.Remove(drone);
            drone.Status = DroneStatus.Free;
            drone.Battery = 100;

            //remove chargeslot which charged the drone
            DataSource.droneCharges.RemoveAll(d => d.DroneId == drone.Id);
            DataSource.drones.Add(drone);
        }


        //---------return list functions---------------

        



    }
}
