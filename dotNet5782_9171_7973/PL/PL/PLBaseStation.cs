using System.Collections.Generic;

namespace PL
{
    public static partial class PL
    {
        /// <summary>
        /// Adds a base station 
        /// </summary>
        /// <param name="baseStation">The base station to add</param>
        /// <exception cref="InvalidPropertyValueException" />
        /// <exception cref="IdAlreadyExistsException" />
        public static void AddBaseStation(BaseStationToAdd baseStation)
        {

            BaseStationsNotification.NotifyBaseStationChanged();
        }

        /// <summary>
        /// return specific base station
        /// </summary>
        /// <param name="id">id of requested base station</param>
        /// <returns>base station with id</returns>
        /// <exception cref="ObjectNotFoundException" />
        public static BaseStation GetBaseStation(int id);

        /// <summary>
        /// return base stations list
        /// </summary>
        /// <returns>base stations list</returns>
        public static IEnumerable<BaseStationForList> GetBaseStationsList();

        /// <summary>
        /// return list of base stations with empty charge slots
        /// </summary>
        /// <returns>list of base stations with empty charge slots</returns>
        public static IEnumerable<BaseStationForList> GetAvailableBaseStations();

        /// <summary>
        /// update base station details
        /// </summary>
        /// <param name="baseStationId">base station to update</param>
        /// <param name="name">new name</param>
        /// <param name="chargeSlots">new number of charge slots</param>
        /// <exception cref="ObjectNotFoundException" />
        /// <exception cref="InvalidPropertyValueException" />
        public static void UpdateBaseStation(int baseStationId, string name = null, int? chargeSlots = null)
        {
            bl.UpdateBaseStation(baseStationId, name, chargeSlots);
            BaseStationsNotification.NotifyBaseStationChanged();
        }

        /// <summary>
        /// Deletes a base station
        /// </summary>
        /// <param name="baseStationId">The base station Id</param>
        /// <exception cref="ObjectNotFoundException"></exception>
        /// <exception cref="InvalidActionException" />
        public static void DeleteBaseStation(int baseStationId)
        {
            bl.DeleteBaseStation(baseStationId);
            BaseStationsNotification.NotifyBaseStationChanged();
        }
    }
}
