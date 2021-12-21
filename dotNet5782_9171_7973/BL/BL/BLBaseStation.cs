using System.Collections.Generic;
using System.Linq;
using BO;

namespace BL
{
    partial class BL
    {
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
            catch (DO.IdAlreadyExistsException)
            {
                throw new IdAlreadyExistsException(typeof(BaseStation), id);
            }
        }

        public BaseStation GetBaseStation(int id)
        {
            DO.BaseStation baseStation;
            try
            {
                baseStation = dal.GetById<DO.BaseStation>(id);
            }
            catch (DO.ObjectNotFoundException e)
            {
                throw new ObjectNotFoundException(typeof(BaseStation), e);
            }

            var charges = dal.GetFilteredList<DO.DroneCharge>(charge => charge.StationId == id);
            var dronesInChargeList = charges.Select(charge => GetDrone(charge.DroneId)).ToList();

            return new BaseStation()
            {
                Id = baseStation.Id,
                Name = baseStation.Name,
                Location = new Location() { Latitude = baseStation.Latitude, Longitude = baseStation.Longitude },
                EmptyChargeSlots = baseStation.ChargeSlots - charges.Count(),
                DronesInChargeList = dronesInChargeList,
            };
        }
        
        public IEnumerable<BaseStationForList> GetBaseStationsList()
        {
            return from baseStation in dal.GetList<DO.BaseStation>()
                   select GetBaseStationForList(baseStation.Id);
        }

        public IEnumerable<BaseStationForList> GetAvailableBaseStations()
        {
            return from station in dal.GetList<DO.BaseStation>()
                   let dronesCount = dal.GetFilteredList<DO.DroneCharge>(charge => charge.StationId == station.Id)
                                        .Count()
                   where station.ChargeSlots > dronesCount
                   select GetBaseStationForList(station.Id);

        }

        public void UpdateBaseStation(int baseStationId, string name = null, int? chargeSlots = null)
        {
            const string nameProperty = "Name";
            const string chargeSlotsProperty = "ChargeSlots";

            if (name != null)
            {
                if (!Validation.IsValidName(name))
                    throw new InvalidPropertyValueException(nameProperty, name);
                
                try
                {
                    dal.Update<DO.BaseStation>(baseStationId, nameProperty, name);
                }
                catch (DO.ObjectNotFoundException e)
                {
                    throw new ObjectNotFoundException(typeof(BaseStation), e);
                }
            }

            if (chargeSlots != null)
            {
                if (chargeSlots < 0)
                    throw new InvalidPropertyValueException(chargeSlotsProperty, chargeSlots);

                try
                {
                    dal.Update<DO.BaseStation>(baseStationId, chargeSlotsProperty, chargeSlots);
                }
                catch (DO.ObjectNotFoundException e)
                {
                    throw new ObjectNotFoundException(typeof(BaseStation), e);
                }
            }
        }

        public void DeleteBaseStation(int baseStationId)
        {
            try
            {
                dal.Delete<DO.BaseStation>(baseStationId);
            }
            catch (DO.ObjectNotFoundException e)
            {
                throw new ObjectNotFoundException(typeof(BaseStation), e);
            }
        }

        /// <summary>
        /// return converted base station to base staion for list
        /// </summary>
        /// <param name="id">id of requested base station</param>
        /// <returns>base station for list</returns>
        /// <exception cref="ObjectNotFoundException" />
        internal BaseStationForList GetBaseStationForList(int id)
        {
            var baseStation = GetBaseStation(id);

            return new BaseStationForList()
            {
                Id = id,
                Name = baseStation.Name,
                EmptyChargeSlots = baseStation.EmptyChargeSlots,
                BusyChargeSlots = baseStation.DronesInChargeList.Count,
            };
        }
    }
}
