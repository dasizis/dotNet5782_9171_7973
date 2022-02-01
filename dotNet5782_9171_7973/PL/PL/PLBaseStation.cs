using System.Collections.Generic;
using PO;
using System.Linq;

namespace PL
{
    static partial class PLService
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
                              double.Parse(baseStation.Longitude),
                              double.Parse(baseStation.Latitude),
                              (int)baseStation.ChargeSlots);

            PLNotification.BaseStationNotification.NotifyItemChanged();
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

            return new BaseStation()
            {
                Id = baseStation.Id,
                Name = baseStation.Name,
                Location = new Location()
                { 
                    Latitude = baseStation.Location.Latitude,
                    Longitude = baseStation.Location.Longitude 
                },
                EmptyChargeSlots = baseStation.EmptyChargeSlots,
                DronesInCharge = baseStation.DronesInCharge.Select(drone => GetDrone(drone.Id)).ToList(),
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
            PLNotification.BaseStationNotification.NotifyItemChanged(baseStationId);
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
            PLNotification.BaseStationNotification.RemoveHandler(baseStationId);
            PLNotification.BaseStationNotification.NotifyItemChanged();
        }

        /// <summary>
        /// Converts <see cref="BO.BaseStationForList"/> to <see cref="BaseStationForList"/>
        /// </summary>
        /// <param name="boBaseStation">The <see cref="BO.BaseStationForList"/></param>
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
