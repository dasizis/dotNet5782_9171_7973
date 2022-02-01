using PO;
using System.Collections.Generic;

namespace PL
{
    partial class PLService
    {
        static readonly BLApi.IBL bl = BLApi.BLFactory.GetBL();

        /// <summary>
        /// Adds a drone
        /// </summary>
        /// <param name="drone">The drone to add</param>
        /// <exception cref="IdAlreadyExistsException" />
        /// <exception cref="InvalidPropertyValueException" />
        public static void AddDrone(DroneToAdd drone)
        {
            bl.AddDrone((int)drone.Id,
                        drone.Model,
                        (BO.WeightCategory)drone.MaxWeight,
                        (int)drone.StationId);

            PLNotification.DroneNotification.NotifyItemChanged();
            PLNotification.BaseStationNotification.NotifyItemChanged();
        }

        /// <summary>
        /// Returns a drone with the spesific id
        /// </summary>
        /// <param name="id">The drone id</param>
        /// <returns>A <see cref="Drone"/> with the given id</returns>
        /// <exception cref="ObjectNotFoundException" />
        public static Drone GetDrone(int id)
        {
            BO.Drone boDrone = bl.GetDrone(id);

            return new()
            {
                Id = id,
                Model = boDrone.Model,
                Battery = boDrone.Battery,
                State = (DroneState)boDrone.State,
                Location = new() { Longitude = boDrone.Location.Longitude, Latitude = boDrone.Location.Latitude },
                MaxWeight = (WeightCategory)boDrone.MaxWeight,
                ParcelInDeliver = boDrone.ParcelInDeliver == null
                                  ? null
                                  : new()
                                  {
                                      Id = boDrone.ParcelInDeliver.Id,
                                      WasPickedUp = boDrone.ParcelInDeliver.WasPickedUp,
                                      DeliveryDistance = boDrone.ParcelInDeliver.DeliveryDistance,
                                  }
                
            };
        }

        /// <summary>
        /// Returns the drones list
        /// </summary>
        /// <returns>An <see cref="IEnumerable{DroneForList}"/> of the drones list</returns>
        public static IEnumerable<DroneForList> GetDronesList()
        {
            List<DroneForList> dronesList = new();

            foreach (var drone in bl.GetDronesList())
            {
                dronesList.Add(new DroneForList()
                {
                    Id = drone.Id,
                    Model = drone.Model,
                    Battery = drone.Battery,
                    State = (DroneState)drone.State,
                    MaxWeight = (WeightCategory)drone.MaxWeight,
                    Location = new Location() { Longitude = drone.Location.Longitude, Latitude = drone.Location.Latitude },
                    DeliveredParcelId = drone.DeliveredParcelId,
                });
            }

            return dronesList;
        }

        /// <summary>
        /// Finds the base station id that the drone is being charged in
        /// </summary>
        /// <param name="droneId">The drone id</param>
        /// <returns>The found base station id</returns>
        /// <exception cref="ObjectNotFoundException" />
        public static int GetDroneBaseStation(int droneId)
        {
            return bl.GetDroneBaseStation(droneId);
        }

        /// <summary>
        /// Renames drone
        /// </summary>
        /// <param name="droneId">Drone Id to rename</param>
        /// <param name="model">The new name</param>
        /// <exception cref="ObjectNotFoundException" />
        /// <exception cref="InvalidPropertyValueException" />
        public static void RenameDrone(int droneId, string newName)
        {
            bl.RenameDrone(droneId, newName);
            PLNotification.DroneNotification.NotifyItemChanged(droneId);
        }

        /// <summary>
        /// Send a drone to charge
        /// </summary>
        /// <param name="droneId">The drone Id to charge</param>
        /// <exception cref="ObjectNotFoundException" />
        /// <exception cref="InvalidActionException" />
        public static void ChargeDrone(int droneId)
        {
            bl.ChargeDrone(droneId);
            PLNotification.DroneNotification.NotifyItemChanged(droneId);
            PLNotification.BaseStationNotification.NotifyItemChanged();
        }

        /// <summary>
        /// Releases a drone from charging
        /// </summary>
        /// <param name="droneId">The drone to release id</param>
        /// <exception cref="ObjectNotFoundException" />
        /// <exception cref="InvalidActionException" />
        public static void FinishCharging(int droneId)
        {
            bl.FinishCharging(droneId);
            PLNotification.DroneNotification.NotifyItemChanged(droneId);
            PLNotification.BaseStationNotification.NotifyItemChanged();
        }

        /// <summary>
        /// Find a suitable parcel and assigns it to the drone
        /// </summary>
        /// <param name="droneId">The drone Id to assign a parcel to</param>
        /// <exception cref="ObjectNotFoundException" />
        /// <exception cref="InvalidActionException" />
        public static void AssignParcelToDrone(int droneId)
        {
            bl.AssignParcelToDrone(droneId);
            PLNotification.DroneNotification.NotifyItemChanged(droneId);
            PLNotification.ParcelNotification.NotifyItemChanged();
        }

        /// <summary>
        /// Deletes a drone
        /// </summary>
        /// <param name="customerId">The customer Id</param>
        /// <exception cref="ObjectNotFoundException"></exception>
        public static void DeleteDrone(int droneId)
        {
            bl.DeleteDrone(droneId);
            PLNotification.DroneNotification.RemoveHandler(droneId);
            PLNotification.DroneNotification.NotifyItemChanged();
        }
    }
}
