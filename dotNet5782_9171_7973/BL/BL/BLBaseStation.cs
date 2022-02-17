using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BO;

namespace BL
{
    partial class BL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddBaseStation(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            var station = new BaseStation()
            {
                Id = id,
                Name = name,
                Location = new Location() { Longitude = longitude, Latitude = latitude },
                EmptyChargeSlots = chargeSlots,
                DronesInCharge = new(),
            };

            lock (Dal) try
            {
                Dal.Add(
                    new DO.BaseStation()
                    {
                        Id = station.Id,
                        Name = station.Name,
                        Latitude = station.Location.Latitude,
                        Longitude = station.Location.Longitude,
                        ChargeSlots = station.EmptyChargeSlots + station.DronesInCharge.Count,
                    }
                );
            }
            catch (DO.IdAlreadyExistsException)
            {
                throw new IdAlreadyExistsException(typeof(BaseStation), id);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public BaseStation GetBaseStation(int id)
        {
            DO.BaseStation baseStation;
            try
            {
                baseStation = Dal.GetById<DO.BaseStation>(id);
            }
            catch (DO.ObjectNotFoundException e)
            {
                throw new ObjectNotFoundException(typeof(BaseStation), e);
            }

            var charges = Dal.GetFilteredList<DO.DroneCharge>(charge => charge.StationId == id);
            var dronesInChargeList = charges.Select(charge => GetDrone(charge.DroneId)).ToList();

            return new BaseStation()
            {
                Id = baseStation.Id,
                Name = baseStation.Name,
                Location = new Location() { Latitude = baseStation.Latitude, Longitude = baseStation.Longitude },
                EmptyChargeSlots = baseStation.ChargeSlots - charges.Count(),
                DronesInCharge = dronesInChargeList,
            };
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStationForList> GetBaseStationsList()
        {
            return from baseStation in Dal.GetList<DO.BaseStation>()
                   select GetBaseStationForList(baseStation.Id);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<int> GetAvailableBaseStationsId()
        {
            return from station in Dal.GetList<DO.BaseStation>()
                   let dronesCount = Dal.GetFilteredList<DO.DroneCharge>(charge => charge.StationId == station.Id)
                                        .Count()
                   where station.ChargeSlots > dronesCount
                   select station.Id;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStationForList> GetAvailableBaseStations()
        {
            return GetAvailableBaseStationsId().Select(id => GetBaseStationForList(id));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateBaseStation(int baseStationId, string name = null, int? emptyChargeSlots = null)
        {
            if (name != null)
            {
                if (!Validation.IsValidName(name))
                    throw new InvalidPropertyValueException(name, nameof(DO.BaseStation.Name));
                
                try
                {
                    Dal.Update<DO.BaseStation>(baseStationId, nameof(DO.BaseStation.Name), name);
                }
                catch (DO.ObjectNotFoundException e)
                {
                    throw new ObjectNotFoundException(typeof(BaseStation), e);
                }
            }

            if (emptyChargeSlots != null)
            {
                if (emptyChargeSlots < 0)
                    throw new InvalidPropertyValueException(emptyChargeSlots, nameof(DO.BaseStation.ChargeSlots));

                int sumChargeSlots = (int)emptyChargeSlots + Dal.GetFilteredList<DO.DroneCharge>(d => d.StationId == baseStationId)
                                                                .Count();

                lock (Dal) try 
                {
                    Dal.Update<DO.BaseStation>(baseStationId, nameof(DO.BaseStation.ChargeSlots), sumChargeSlots);
                }
                catch (DO.ObjectNotFoundException e)
                {
                    throw new ObjectNotFoundException(typeof(BaseStation), e);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteBaseStation(int baseStationId)
        {
            BaseStationForList baseStation; 
            try
            {
                baseStation = GetBaseStationForList(baseStationId);
            }
            catch (DO.ObjectNotFoundException e)
            {
                throw new ObjectNotFoundException(typeof(BaseStation), e);
            }

            if (baseStation.BusyChargeSlots > 0)
                throw new InvalidActionException("Can not delete a not empty base station.");

            lock (Dal)
            {
                Dal.Delete<DO.BaseStation>(baseStationId);
            }
        }

        #region Helpers

        /// <summary>
        /// return converted base station to base staion for list
        /// </summary>
        /// <param name="id">id of requested base station</param>
        /// <returns>base station for list</returns>
        /// <exception cref="ObjectNotFoundException" />
        public BaseStationForList GetBaseStationForList(int id)
        {
            var baseStation = GetBaseStation(id);

            return new BaseStationForList()
            {
                Id = id,
                Name = baseStation.Name,
                EmptyChargeSlots = baseStation.EmptyChargeSlots,
                BusyChargeSlots = baseStation.DronesInCharge.Count,
            };
        }

        #endregion
    }
}
