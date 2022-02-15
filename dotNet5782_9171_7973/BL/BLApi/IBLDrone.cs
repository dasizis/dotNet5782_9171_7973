using BO;
using System.Collections.Generic;

namespace BLApi
{
    /// <summary>
    /// Declares all <see cref="IBL"/> methods related to <see cref="Drone"/>
    /// </summary>
    public interface IBLDrone
    {
        /// <summary>
        /// Adds a drone
        /// </summary>
        /// <param name="id">The drone id</param>
        /// <param name="model">The drone model </param>
        /// <param name="maxWeight">The drone max weight to carry</param>
        /// <param name="stationId">A station id for first loading</param>
        /// <exception cref="IdAlreadyExistsException" />
        /// <exception cref="InvalidPropertyValueException" />
        void AddDrone(int id, string model, WeightCategory maxWeight, int stationId);

        /// <summary>
        /// Returns a drone with the spesific id
        /// </summary>
        /// <param name="id">The drone id</param>
        /// <returns>A <see cref="Drone"/> with the given id</returns>
        /// <exception cref="ObjectNotFoundException" />
        Drone GetDrone(int id);

        /// <summary>
        /// Returns a converted drone to drone for list
        /// </summary>
        /// <param name="id">The id of requested drone</param>
        /// <returns>A clone of <see cref="DroneForList"/></returns>
        /// <exception cref="ObjectNotFoundException" />
        DroneForList GetDroneForList(int id);

        /// <summary>
        /// Returns the drones list
        /// </summary>
        /// <returns>An <see cref="IEnumerable{DroneForList}"/> of the drones list</returns>
        IEnumerable<DroneForList> GetDronesList();
        
        /// <summary>
        /// Finds the base station id that the drone is being charged in
        /// </summary>
        /// <param name="droneId">The drone id</param>
        /// <returns>The found base station id</returns>
        /// <exception cref="ObjectNotFoundException" />
        int GetDroneBaseStation(int droneId);

        /// <summary>
        /// Renames drone
        /// </summary>
        /// <param name="droneId">Drone Id to rename</param>
        /// <param name="model">The new name</param>
        /// <exception cref="ObjectNotFoundException" />
        /// <exception cref="InvalidPropertyValueException" />
        void RenameDrone(int droneId, string newName);

        /// <summary>
        /// Send a drone to charge
        /// </summary>
        /// <param name="droneId">The drone Id to charge</param>
        /// <returns>The Id of the station which the drone is charging in</returns>
        /// <exception cref="ObjectNotFoundException" />
        /// <exception cref="InvalidActionException" />
        int ChargeDrone(int droneId);

        /// <summary>
        /// Releases a drone from charging
        /// </summary>
        /// <param name="droneId">The drone to release id</param>
        /// <returns>The Id of the station which the drone is charging in</returns>
        /// <exception cref="ObjectNotFoundException" />
        /// <exception cref="InvalidActionException" />
        int FinishCharging(int droneId);

        /// <summary>
        /// Find a suitable parcel and assigns it to the drone
        /// </summary>
        /// <param name="droneId">The drone Id to assign a parcel to</param>
        /// <returns>The parcel Id</returns>
        /// <exception cref="ObjectNotFoundException" />
        /// <exception cref="InvalidActionException" />
        int AssignParcelToDrone(int droneId);

        /// <summary>
        /// Picks a parcel up by a drone
        /// </summary>
        /// <param name="droneId">The drone id</param>        
        /// <returns>The parcel Id</returns>
        /// <exception cref="ObjectNotFoundException" />
        /// <exception cref="InvalidActionException" />
        int PickUpParcel(int droneId);

        /// <summary>
        /// Supply a parcel by a drone
        /// </summary>
        /// <param name="droneId">The drone Id</param>
        /// <returns>The parcel Id</returns>
        /// <exception cref="ObjectNotFoundException" />
        /// <exception cref="InvalidActionException" />
        int SupplyParcel(int droneId);

        /// <summary>
        /// Deletes a drone
        /// </summary>
        /// <param name="customerId">The customer Id</param>
        /// <exception cref="ObjectNotFoundException"></exception>
        /// <exception cref="InvalidActionException" />
        void DeleteDrone(int droneId);
    }
}
