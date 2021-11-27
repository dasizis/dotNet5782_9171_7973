using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace BL
{
    public partial class BL
    {
        /// <summary>
        /// return specific customer
        /// </summary>
        /// <param name="id">id of requested customer</param>
        /// <returns>customer with id</returns>
        public Customer GetCustomer(int id)
        {
            IDAL.DO.Customer customer;
            try
            {
                customer = dal.GetById<IDAL.DO.Customer>(id);
            }
            catch
            {
                throw new ObjectNotFoundException(typeof(Customer), id);
            }

            var sendParcels = from parcel in dal.GetList<IDAL.DO.Parcel>()
                                       where parcel.SenderId == id
                                       select GetParcel(parcel.Id);

            var targetParcels = from parcel in dal.GetList<IDAL.DO.Parcel>()
                                         where parcel.TargetId == id
                                         select GetParcel(parcel.Id);

            return new Customer()
            {
                Id = customer.Id,
                Location = new Location() { Latitude = customer.Latitude, Longitude = customer.Longitude },
                Name = customer.Name,
                Phone = customer.Phone,
                Send = sendParcels.ToList(),
                Recieve = targetParcels.ToList(),
            };
        }
        /// <summary>
        /// return specific parcel
        /// </summary>
        /// <param name="id">id of requested parcel</param>
        /// <returns>parcel with id</returns>
        public Parcel GetParcel(int id)
        {
            IDAL.DO.Parcel parcel;
            try
            {
                parcel = dal.GetById<IDAL.DO.Parcel>(id);
            }
            catch
            {
                throw new ObjectNotFoundException(typeof(Parcel), id);
            }

            return new Parcel()
            {
                Id = parcel.Id,
                Drone = parcel.DroneId.HasValue? GetDrone(parcel.DroneId.Value) : null,
                Sender = GetCustomerInDelivery(parcel.SenderId),
                Target = GetCustomerInDelivery(parcel.TargetId),
                Weight = (WeightCategory)parcel.Weight,
                Priority = (Priority)parcel.Priority,
                Requested = parcel.Requested,
                Scheduled = parcel.Scheduled,
                PickedUp = parcel.PickedUp,
                Supplied = parcel.Supplied,
            };
        }
        /// <summary>
        /// return specific base station
        /// </summary>
        /// <param name="id">id of requested base station</param>
        /// <returns>base station with id</returns>
        public BaseStation GetBaseStation(int id)
        {
            IDAL.DO.BaseStation baseStation;
            try
            {
                baseStation = dal.GetById<IDAL.DO.BaseStation>(id);
            }
            catch
            {
                throw new ObjectNotFoundException(typeof(BaseStation), id);
            }

            var chargeSlots = dal.GetDroneCharges().Where(charge => charge.StationId == id).ToList();
            var dronesInChargeList = chargeSlots.Select(charge => GetDrone(charge.DroneId)).ToList();

            return new BaseStation()
            {
                Id = baseStation.Id,
                Name = baseStation.Name,
                Location = new Location() { Latitude = baseStation.Latitude, Longitude = baseStation.Longitude },
                EmptyChargeSlots = baseStation.ChargeSlots - chargeSlots.Count,
                DronesInChargeList = dronesInChargeList,
            };
        }

        /// <summary>
        /// return specific drone
        /// </summary>
        /// <param name="id">id of requested drone</param>
        /// <returns>drone with id</returns>
        public Drone GetDrone(int id)
        {
            
            DroneForList drone = drones.FirstOrDefault(d => d.Id == id);

            if(drone.Equals(default(DroneForList)))
            {
                throw new ObjectNotFoundException(typeof(Drone), id);
            }

            ParcelInDeliver parcelInDeliver = drone.State == DroneState.Deliver ?
                                              GetParcelInDeliver((int)drone.DeliveredParcelId) :
                                              null;
            return new Drone()
            {
                Id = drone.Id,
                Battery = drone.Battery,
                Location = new Location() { Latitude = drone.Location.Latitude, Longitude = drone.Location.Longitude },
                MaxWeight = drone.MaxWeight,
                Model = drone.Model,
                State = drone.State,
                ParcelInDeliver = parcelInDeliver,

            };
        }
        /// <summary>
        /// return specific customer for list
        /// </summary>
        /// <param name="id">id of requested customer</param>
        /// <returns>customer with id</returns>
        public CustomerForList GetCustomerForList(int id)
        {
            IDAL.DO.Customer customer;

            try
            {
                customer = dal.GetById<IDAL.DO.Customer>(id);
            }
            catch
            {
                throw new ObjectNotFoundException(typeof(Customer), id);
            }

            var parcels = dal.GetList<IDAL.DO.Parcel>();

            return new CustomerForList()
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                ParcelsSendAndSupplied = parcels.Count(parcel => parcel.SenderId == id && parcel.Supplied != null),
                ParcelsSendAndNotSupplied = parcels.Count(parcel => parcel.SenderId == id && parcel.Supplied == null),
                ParcelsRecieved = parcels.Count(parcel => parcel.TargetId == id && parcel.Supplied != null),
                ParcelsOnWay = parcels.Count(parcel => parcel.TargetId == id && parcel.Supplied == null),
            };
        }
        /// <summary>
        /// return converted drone to drone for list
        /// </summary>
        /// <param name="id">id of requested drone</param>
        /// <returns>drone for list</returns>
        public DroneForList GetDroneForList(int id)
        {
            var drone = drones.FirstOrDefault(d => d.Id == id);

            if (drone == default)
            {
                throw new ObjectNotFoundException(typeof(DroneForList), id);
            }

            return drone;
        }
        /// <summary>
        /// return converted parcel to parcel for list
        /// </summary>
        /// <param name="id">id of requested parcel</param>
        /// <returns>parcel for list</returns>
        public ParcelForList GetParcelForList(int id)
        {
            var parcel = GetParcel(id);

            return new ParcelForList()
            {
                Id = parcel.Id,
                Priority = parcel.Priority,
                Weight = parcel.Weight,
                SenderName = parcel.Sender.Name,
                TargetName = parcel.Target.Name,
            };
        }
        /// <summary>
        /// return converted drone to drone in charge
        /// </summary>
        /// <param name="id">id of requested drone</param>
        /// <returns>drone in charge</returns>
        public DroneInCharge GetDroneInCharge(int id)
        {
            return new DroneInCharge()
            {
                Id = id,
                BatteryState = GetDroneForList(id).Battery,
            };
        }
        /// <summary>
        /// return converted drone to drone in delivery
        /// </summary>
        /// <param name="id">id of requested drone</param>
        /// <returns>drone in delivery</returns>
        public DroneInDelivery GetDroneInDelivery(int id)
        {
            var drone = GetDrone(id);

            return new DroneInDelivery()
            {
                Id = id,
                BatteryState = drone.Battery,
                Location = drone.Location,
            };
        }
        /// <summary>
        /// return converted base station to base staion for list
        /// </summary>
        /// <param name="id">id of requested base station</param>
        /// <returns>base station for list</returns>
        public BaseStationForList GetBaseStationForList(int id)
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
        /// <summary>
        /// return converted customer to customer in delivery
        /// </summary>
        /// <param name="id">id of requested customer</param>
        /// <returns>customer in delivery</returns>
        public CustomerInDelivery GetCustomerInDelivery(int id)
        {
            IDAL.DO.Customer customer;

            try
            {
                customer = dal.GetById<IDAL.DO.Customer>(id);
            }
            catch
            {
                throw new ObjectNotFoundException(typeof(Customer), id);
            }

            return new CustomerInDelivery()
            {
                Id = id,
                Name = customer.Name,
            };
        }
        /// <summary>
        /// return converted parcel to parcel at customer
        /// </summary>
        /// <param name="id">id of requested parcel</param>
        /// <returns>parcel at customer</returns>
        public ParcelAtCustomer GetParcelAtCustomer(int id)
        {
            var parcel = GetParcel(id);

            var state = parcel.Requested == null ? ParcelState.Associated
                        : parcel.Scheduled == null ? ParcelState.Defined
                        : parcel.PickedUp == null ? ParcelState.PickedUp
                        : ParcelState.Provided;

            return new ParcelAtCustomer()
            {
                Id = id,
                Priority = parcel.Priority,
                Weight = parcel.Weight,
                OtherCustomer = parcel.Supplied != null? parcel.Sender: parcel.Target,
                State = state,
            };
        }
        /// <summary>
        /// return converted parcel to parcel in delivery
        /// </summary>
        /// <param name="id">id of requested parcel</param>
        /// <returns>parcel in delivery</returns>
        public ParcelInDeliver GetParcelInDeliver(int id)
        {
            IDAL.DO.Parcel parcel;
            try
            {
                parcel = dal.GetById<IDAL.DO.Parcel>(id);
            }
            catch
            {
                throw new ObjectNotFoundException(typeof(Parcel), id);
            }

            IDAL.DO.Customer targetCustomer;
            try
            {
                targetCustomer = dal.GetById<IDAL.DO.Customer>(parcel.SenderId);  
            }
            catch
            {
                throw new ObjectNotFoundException(typeof(Customer), parcel.SenderId);
            }

            IDAL.DO.Customer senderCustomer;
            try
            {
                senderCustomer = dal.GetById<IDAL.DO.Customer>(parcel.TargetId);
            }
            catch
            {
                throw new ObjectNotFoundException(typeof(Customer), parcel.TargetId);
            }

            var targetLocation = new Location() { Latitude = targetCustomer.Latitude, Longitude = targetCustomer.Longitude };
            var senderLocation = new Location() { Latitude = senderCustomer.Latitude, Longitude = senderCustomer.Longitude };

            return new ParcelInDeliver()
            {
                Id = id,
                Weight = (WeightCategory)parcel.Weight,
                Priority = (Priority)parcel.Priority,
                TargetLocation = targetLocation,
                CollectLocation = senderLocation,
                Position = parcel.Supplied != null,
                DeliveryDistance = Location.Distance(senderLocation, targetLocation),
                Sender = GetCustomerInDelivery(senderCustomer.Id),
                Target = GetCustomerInDelivery(targetCustomer.Id),
            };
        }
    }
}
