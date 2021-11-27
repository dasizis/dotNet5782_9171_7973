using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

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
                    new IDAL.DO.BaseStation()
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
            return dal.GetAvailableBaseStations().Select(station => GetBaseStationForList(station.Id));
        }
        /// <summary>
        /// return base stations list
        /// </summary>
        /// <returns>base stations list</returns>
        public IEnumerable<BaseStationForList> GetBaseStationsList()
        {
            return dal.GetList<IDAL.DO.BaseStation>().Select(baseStation => GetBaseStationForList(baseStation.Id));
        }
        /// <summary>
        /// update base station details
        /// </summary>
        /// <param name="baseStationId">base station to update</param>
        /// <param name="name">new name</param>
        /// <param name="chargeSlots">new number of charge slots</param>
        public void UpdateBaseStation(int baseStationId, string name = null, int? chargeSlots = null)
        {
            IDAL.DO.BaseStation station;

            try
            {
                station = dal.GetById<IDAL.DO.BaseStation>(baseStationId);
            }
            catch
            {
                throw new ObjectNotFoundException(typeof(BaseStation), baseStationId);
            }

            dal.Remove<IDAL.DO.BaseStation>(station.Id);

            station.Name = name ?? station.Name;
            station.ChargeSlots = chargeSlots ?? station.ChargeSlots;

            dal.Add(station);
        }
    }
}
