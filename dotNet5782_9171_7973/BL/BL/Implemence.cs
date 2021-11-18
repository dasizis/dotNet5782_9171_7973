using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    partial class BL
    {
        List<DroneForList> drones = new();
        
        public BL()
        {
            Random rand = new();

            int id;
            string model;
            double battery;
            int? parcelInDeliver = null;
            WeightCategory weight;
            DroneState state;
            Location location = new();
            

            var dlDrones = (List<IDAL.DO.Drone>)Dal.GetList(typeof(IDAL.DO.Drone));
            var parcels = (List<IDAL.DO.Parcel>)Dal.GetList(typeof(IDAL.DO.Parcel));

            foreach (var dlDrone in dlDrones)
            {
                id = dlDrone.Id;
                model = dlDrone.Model;

                var parcel = parcels.FirstOrDefault(parcel => parcel.DroneId == dlDrone.Id);

                if (parcel.Equals(default))
                {
                    state = (DroneState)rand.Next(0, 1);
                    weight = (WeightCategory)rand.Next(0, 2);
                }

                else
                {
                    state = DroneState.DELIVER;
                    weight = (WeightCategory)parcel.Weight;
                    parcelInDeliver = parcel.Id;
                }

                if(state == DroneState.DELIVER)
                { 
                    int electricity = GetElectricity(weight);
                    bool isPickedUp = true;
                    Location targetLocation = parcel.TargetId/*converted to customerInDelivery*/.Location;
                    Location senderLocation = parcel.SenderId /*converted to CustomerInDelivery*/.Location;

                    if (!parcel.Supplied.Equals(DateTime.MinValue)) break;
                    
                    if (parcel.PickedUp.Equals(DateTime.MinValue))
                    { 
                        location = ClosestStation(senderLocation);
                        isPickedUp = false;
                    }   

                    else
                    {
                        location = senderLocation;
                    }
               
                    double minCharge = isPickedUp ?
                        electricity * (Location.Distance(location, targetLocation)
                        + Location.Distance(targetLocation, ClosestStation(targetLocation))) :
                        electricity * (Location.Distance(location, senderLocation) + Location.Distance(senderLocation, targetLocation)
                        + Location.Distance(targetLocation, ClosestStation(targetLocation)));

                    
                    battery = (rand.NextDouble() * 100 + minCharge) % 100;

                }

                if(state == DroneState.MEINTENENCE)
                {
                    var stations = (List<IDAL.DO.BaseStation>)Dal.GetList(typeof(IDAL.DO.BaseStation));
                    int rndStation = rand.Next(stations.Count());

                    location.Longitude = stations[rndStation].Longitude;
                    location.Longitude = stations[rndStation].Latitude;

                    battery = rand.NextDouble()*20;
                }

               else
               {
                    var suppliedParcels = parcels.FindAll(parcel =>
                                                    !parcel.Supplied.Equals(DateTime.MinValue));
                    int rndParcel = rand.Next(suppliedParcels.Count);
                    
                    Location targetCustomerLocation = suppliedParcels[rndParcel].TargetId/*converted to CustomerInDelivery*/.Location;
                    location.Longitude = targetCustomerLocation.Longitude;
                    location.Latitude = targetCustomerLocation.Latitude;

                    double minCharge = Location.Distance(location, closestStation(location)) * ElectricityConfumctiolFree;
                    battery = (rand.NextDouble() * 100 + minCharge) % 100;
               }

                drones.Add(
                        new DroneForList()
                        {
                            Id = id,
                            Model = model,
                            MaxWeight = weight,
                            Battery = battery,
                            State = state,
                            Location = location,
                            DeliveredParcelId = parcelInDeliver
                        }
                    );
            }
        }

        public void AssignParcelToDrone(int droneId)
        {
            Drone drone = GetById<Drone>(droneId);

            if (drone.State == DroneState.DELIVER)
            {
                throw new InValidActionException();
            }

           var parcels = (Dal.GetNotAssignedToDroneParcels() as List<IDAL.DO.Parcel>)
                         .FindAll(parcel => 
                            (int)parcel.Weight < (int)drone.MaxWeight && 
                            IsAbleToPassParcel(drone, GetById<ParcelInDeliver>(parcel.Id)))
                         .OrderBy(p => p.Priority)
                         .ThenBy(p => p.Weight)
                         .ThenBy(p => Location.Distance(GetById<Customer>(p.SenderId).Location, drone.Location))
                         .ToList();

            if (parcels.Count == 0)
            {
                throw new InValidActionException();
            }

            Dal.AssignParcelToDrone(parcels.First().Id, droneId);

            drone.State = DroneState.DELIVER;
        }

        public void FinishCharging(int droneId, double timeInCharge)
        {
            DroneForList drone = GetById<DroneForList>(droneId);

            if (drone.State != DroneState.MEINTENENCE)
            {
                throw new InValidActionException();
            }

            drone.Battery += ChargeRate * timeInCharge;
            drone.State = DroneState.FREE;

            Dal.FinishCharging(drone.Id);
        }

        public void PickUpParcel(int droneId)
        {
            DroneForList drone = GetById<DroneForList>(droneId);

            // Does the drone have a parcel?
            if (!drone.DeliveredParcelId.HasValue)
            {                
                throw new InValidActionException();
            }

            var parcel = Dal.GetById<IDAL.DO.Parcel>(drone.DeliveredParcelId.Value);

            // Was the parcel collected?
            if(parcel.PickedUp != DateTime.MinValue)
            {
                throw new InValidActionException();
            }

            ParcelInDeliver parcelInDeliver = GetById<ParcelInDeliver>(parcel.Id);

            drone.Battery -= Location.Distance(drone.Location, parcelInDeliver.CollectLocation) * ElectricityConfumctiolFree;
            drone.Location = parcelInDeliver.CollectLocation;

            Dal.CollectParcel(parcel.Id);
        }

        public void RenameDrone(int droneId, string model)
        {
            DroneForList drone = GetById<DroneForList>(droneId);
            drone.Model = model;


            var dlDrone = Dal.GetById<IDAL.DO.Drone>(droneId);
            dlDrone.Model = model;

            Dal.Update<IDAL.DO.Drone>(dlDrone);
        }

        //void Update<T>(IDAL.DO.IIdentifiable item)
        //{
        //    if ()
        //}

        public void SendDroneToCharge(int droneId)
        {
            DroneForList drone = GetById<DroneForList>(droneId);

            if (drone.State != DroneState.FREE)
            {
                throw new InValidActionException();
            }

            var availableBaseStations = Dal.GetAvailableBaseStations().Select(b => GetById<BaseStation>(b.Id));

            BaseStation closest = drone.FindClosest( // new implement?

            if (!IsEnoughBattery(drone/*converted*/, closest.Location))
                throw new InValidActionException();

            drone.Location = closest.Location;
            drone.State = DroneState.MEINTENENCE;
            drone.Battery -= ElectricityConfumctiolFree * Location.Distance(closest.Location, drone.Location);

            //what for?
            closest.EmptyChargeSlots -= 1;

            Dal.ChargeDroneAtBaseStation(drone.Id, closest.Id);
        }

        public void SupplyParcel(int droneId)
        {
            DroneForList drone = drones.FirstOrDefault(d => d.Id == droneId);

            if (drone.Equals(default))
                throw new DalObject.ObjectNotFoundException();

            if (!drone.DeliveredParcelId.HasValue)
                throw new InValidActionException();

            var parcel = (IDAL.DO.Parcel)Dal.GetById(typeof(IDAL.DO.Parcel), drone.DeliveredParcelId.Value);

            if (parcel.PickedUp == DateTime.MinValue)
                throw new InValidActionException();

            ParcelInDeliver parcelInDeliver = parcel/*converted*/;

            if(parcelInDeliver.Position == true)
                throw new InValidActionException();
            
            drone.Battery -= Location.Distance(drone.Location, parcelInDeliver.Target) * GetElectricity(parcelInDeliver.Weight);
            drone.Location = parcelInDeliver.Target;
            drone.State = DroneState.FREE;

            Dal.SupplyParcel(parcel.Id);
        }

        public void UpdateBaseStation(int baseStationId, string name = null, int? chargeSlots = null)
        {

            //validation

            var station = (IDAL.DO.BaseStation)Dal.GetById(typeof(IDAL.DO.BaseStation), baseStationId);
            Dal.Remove(station);

            station.Name = name != null ? name : station.Name;
            station.ChargeSlots = chargeSlots.HasValue ? (int)chargeSlots : station.ChargeSlots;

            Dal.Add(station);
        }

        public void UpdateCustomer(int customerId, string name = null, string phone = null)
        {
            //validation

            var customer = (IDAL.DO.Customer)Dal.GetById(typeof(IDAL.DO.Customer), customerId);
            Dal.Remove(customer);

            customer.Name = name != null ? name : customer.Name;
            customer.Phone = name != null ? phone : customer.Phone;

            Dal.Add(customer);
        }

        private bool IsAbleToPassParcel(Drone drone, ParcelInDeliver parcel)
        {
           return drone.Battery >= (Location.Distance(drone.Location, parcel.CollectLocation) * ElectricityConfumctiolFree +
                Location.Distance(parcel.CollectLocation, parcel.Target) * GetElectricity(parcel.Weight) +
                Location.Distance(parcel.Target, drone.closestStation(parcel.Target)) * ElectricityConfumctiolFree);
        }

        private int GetElectricity(WeightCategory weight)
        {
            int electricity = 0;

            switch (weight)
            {
                case WeightCategory.Light:
                    electricity = ElectricityConfumctiolLight;
                    break;
                case WeightCategory.Medium:
                    electricity = ElectricityConfumctiolMedium;
                    break;
                case WeightCategory.Heavy:
                    electricity = ElectricityConfumctiolHeavy;
                    break;
                default:
                    break;
            }

            return electricity;
        }

        private bool IsEnoughBattery(Drone drone, Location location)
        {
            //implement
            return true;
        }
    }
}
