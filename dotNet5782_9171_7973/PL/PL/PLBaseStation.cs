using System.Collections.Generic;
using PO;

namespace PL
{
    static partial class PL
    {
        /// <summary>
        /// Adds a base station 
        /// </summary>
        /// <param name="baseStation">The base station to add</param>
        /// <exception cref="InvalidPropertyValueException" />
        /// <exception cref="IdAlreadyExistsException" />
        public static void AddBaseStation(BaseStationToAdd baseStation)
        {
            bl.AddBaseStation((int)baseStation.Id,
                              baseStation.Name,
                              (double)baseStation.Location.Longitude,
                              (double)baseStation.Location.Latitude,
                              (int)baseStation.ChargeSlots);
               
            BaseStationsNotification.NotifyBaseStationChanged();
        }

        /// <summary>
        /// return specific base station
        /// </summary>
        /// <param name="id">id of requested base station</param>
        /// <returns>base station with id</returns>
        /// <exception cref="ObjectNotFoundException" />
        public static BaseStation GetBaseStation(int id)
        {
            BO.BaseStation baseStation = bl.GetBaseStation(id);

            List<Drone> drones = null;
            foreach(var drone in baseStation.DronesInCharge)
            {
                drones.Add(GetDrone(drone.Id));
            }


            return new BaseStation()
            {
                Id = baseStation.Id,
                Name = baseStation.Name,
                Location = new Location() { Latitude = baseStation.Location.Latitude, Longitude = baseStation.Location.Longitude },
                EmptyChargeSlots = baseStation.EmptyChargeSlots,
                DronesInCharge = drones,
            };
        }

        /// <summary>
        /// return base stations list
        /// </summary>
        /// <returns>base stations list</returns>
        public static IEnumerable<BaseStationForList> GetBaseStationsList()
        {
            List<BaseStationForList> stationsList = new();

            foreach (var station in bl.GetBaseStationsList())
            {
                stationsList.Add(ConvertBaseStation(station));
            }

            return stationsList;
        }

        /// <summary>
        /// return list of base stations with empty charge slots
        /// </summary>
        /// <returns>list of base stations with empty charge slots</returns>
        public static IEnumerable<BaseStationForList> GetAvailableBaseStations()
        {
            List<BaseStationForList> stationsList = new();

            foreach (var station in bl.GetAvailableBaseStations())
            {
                stationsList.Add(ConvertBaseStation(station));
            }

            return stationsList;
        }

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

        /// <summary>
        /// Converts <see cref="BO.BaseStationForList"/> to <see cref="BaseStationForList"/>
        /// </summary>
        /// <param name="boBaseStation">The BO base station</param>
        /// <returns>A <see cref="BaseStationForList"/></returns>
        private static BaseStationForList ConvertBaseStation(BO.BaseStationForList boBaseStation)
        {
            return new BaseStationForList()
            {
                Id = boBaseStation.Id,
                Name = boBaseStation.Name,
                BusyChargeSlots = boBaseStation.BusyChargeSlots,
                EmptyChargeSlots = boBaseStation.EmptyChargeSlots,
            };
        }
    }
}
