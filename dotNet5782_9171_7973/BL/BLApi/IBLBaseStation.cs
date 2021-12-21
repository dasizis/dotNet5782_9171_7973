using BO;
using System.Collections.Generic;


namespace BLApi
{
    public interface IBLBaseStation
    {
        /// <summary>
        /// Adds a base station 
        /// </summary>
        /// <param name="id">The base station id</param>
        /// <param name="name">The base station name</param>
        /// <param name="location">The base station location</param>
        /// <param name="chargeSlots">The base station number of charge slots</param>
        /// <exception cref="InvalidPropertyValueException" />
        /// <exception cref="IdAlreadyExistsException" />
        void AddBaseStation(int id, string name, Location location, int chargeSlots);

        /// <summary>
        /// return specific base station
        /// </summary>
        /// <param name="id">id of requested base station</param>
        /// <returns>base station with id</returns>
        /// <exception cref="ObjectNotFoundException" />
        BaseStation GetBaseStation(int id);

        /// <summary>
        /// return base stations list
        /// </summary>
        /// <returns>base stations list</returns>
        IEnumerable<BaseStationForList> GetBaseStationsList();

        /// <summary>
        /// return list of base stations with empty charge slots
        /// </summary>
        /// <returns>list of base stations with empty charge slots</returns>
        IEnumerable<BaseStationForList> GetAvailableBaseStations();

        /// <summary>
        /// update base station details
        /// </summary>
        /// <param name="baseStationId">base station to update</param>
        /// <param name="name">new name</param>
        /// <param name="chargeSlots">new number of charge slots</param>
        /// <exception cref="ObjectNotFoundException" />
        /// <exception cref="InvalidPropertyValueException" />
        void UpdateBaseStation(int baseStationId, string name = null, int? chargeSlots = null);

        /// <summary>
        /// Deletes a base station
        /// </summary>
        /// <param name="baseStationId">The base station Id</param>
        /// <exception cref="ObjectNotFoundException"></exception>
        void DeleteBaseStation(int baseStationId);
    }
}
