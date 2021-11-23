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
            var customer = Dal.GetById<IDAL.DO.Customer>(id);

            var sendParcels = from parcel in Dal.GetList<IDAL.DO.Parcel>()
                                       where parcel.SenderId == id
                                       select GetParcel(parcel.Id);

            var targetParcels = from parcel in Dal.GetList<IDAL.DO.Parcel>()
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
            var parcel = Dal.GetById<IDAL.DO.Parcel>(id);

            return new Parcel()
            {
                Id = parcel.Id,
                Drone = GetDrone((int)parcel.DroneId),
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
            var baseStation = Dal.GetById<IDAL.DO.BaseStation>(id);

            var chargeSlots = Dal.GetDroneCharges().Where(charge => charge.StationId == id).ToList();
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
            DroneForList drone = drones.Find(d => d.Id == id);
            ParcelInDeliver parcelInDeliver = drone.State == DroneState.DELIVER ?
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
            var customer = Dal.GetById<IDAL.DO.Customer>(id);
            var parcels = Dal.GetList<IDAL.DO.Parcel>();

            return new CustomerForList()
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                ParcelsSendAndSupplied = parcels.Where(parcel => parcel.SenderId == id && parcel.Supplied != null).Count(),
                ParcelsSendAndNotSupplied = parcels.Where(parcel => parcel.SenderId == id && parcel.Supplied == null).Count(),
                ParcelsRecieved = parcels.Where(parcel => parcel.TargetId == id && parcel.Supplied != null).Count(),
                ParcelsOnWay = parcels.Where(parcel => parcel.TargetId == id && parcel.Supplied == null).Count(),
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
                Priority = (Priority)parcel.Priority,
                Weight = (WeightCategory)parcel.Weight,
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
            var customer = Dal.GetById<IDAL.DO.Customer>(id);

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
            var parcel = GetParcel(id);
            var targetCustomer = GetCustomer(parcel.Target.Id);
            var senderCustomer = GetCustomer(parcel.Sender.Id);

            return new ParcelInDeliver()
            {
                Id = id,
                Weight = parcel.Weight,
                Priority = parcel.Priority,
                Target = targetCustomer.Location,
                CollectLocation = senderCustomer.Location,
                Position = parcel.Supplied != null,
                DeliveryDistance = Location.Distance(senderCustomer.Location, targetCustomer.Location),
            };
        }
    }
}
