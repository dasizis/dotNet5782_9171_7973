using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    partial class BL
    {
        /// <summary>
        /// add a base station 
        /// </summary>
        /// <param name="id">the base station id</param>
        /// <param name="name">the base station name</param>
        /// <param name="location">the base station location</param>
        /// <param name="chargeSlots">the base station number of charge slots</param>
        public void AddBaseStation(int id, string name, Location location, int chargeSlots)
        {
            var station = new BaseStation()
            {
                Id = id,
                Name = name,
                Location = location,
                EmptyChargeSlots = chargeSlots,
                DronesInChargeList = new(),
            };

            try
            {

                dal.Add(
                    new DO.BaseStation()
                    {
                        Id = station.Id,
                        Name = station.Name,
                        Latitude = station.Location.Latitude,
                        Longitude = station.Location.Longitude,
                        ChargeSlots = station.EmptyChargeSlots + station.DronesInChargeList.Count,
                    }
                );
            }
            catch
            {
                throw new IdAlreadyExistsException(typeof(BaseStation), id);
            }
        }
        /// <summary>
        /// return list of base stations with empty charge slots
        /// </summary>
        /// <returns>list of base stations with empty charge slots</returns>
        public IEnumerable<BaseStationForList> GetAvailableBaseStations()
        {
            return from station in dal.GetList<DO.BaseStation>()
                   let dronesCount = (from charge in dal.GetList<DO.DroneCharge>()
                                      where charge.StationId == station.Id && charge.IsDeleted == false
                                      select charge).Count()
                   where station.ChargeSlots > dronesCount
                   select GetBaseStationForList(station.Id);
           
        }
        /// <summary>
        /// return base stations list
        /// </summary>
        /// <returns>base stations list</returns>
        public IEnumerable<BaseStationForList> GetBaseStationsList()
        {
            return from baseStation in dal.GetList<DO.BaseStation>()
                   select GetBaseStationForList(baseStation.Id);
        }
        /// <summary>
        /// update base station details
        /// </summary>
        /// <param name="baseStationId">base station to update</param>
        /// <param name="name">new name</param>
        /// <param name="chargeSlots">new number of charge slots</param>
        public void UpdateBaseStation(int baseStationId, string name = null, int? chargeSlots = null)
        {
            DO.BaseStation station;

            try
            {
                station = dal.GetById<DO.BaseStation>(baseStationId);
            }
            catch
            {
                throw new ObjectNotFoundException(typeof(BaseStation), baseStationId);
            }

            dal.Update<DO.BaseStation>(baseStationId, nameof(station.Name), name ?? station.Name);
            dal.Update<DO.BaseStation>(baseStationId, nameof(station.ChargeSlots), chargeSlots ?? station.ChargeSlots);
        }
    }
}
