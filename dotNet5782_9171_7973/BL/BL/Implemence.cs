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

                    if (!parcel.Delivered.Equals(DateTime.MinValue)) break;
                    
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
                                                    !parcel.Delivered.Equals(DateTime.MinValue));
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
                            CurrentLocation = location,
                            DeliveredParcelId = parcelInDeliver
                        }
                    );
            }
        }

        public void AssignParcelToDrone(int droneId)
        {
            var dlDrone = Dal.GetById(typeof(IDAL.DO.Drone), droneId);
            Drone drone = dlDrone/*converted to drone*/;
            if (drone.State == DroneState.DELIVER) return;

            var parcels = ((List<IDAL.DO.Parcel>)Dal.GetNotAssignedToDroneParcels()).FindAll(
                                                                         parcel => (int)parcel.Weight < (int)drone.MaxWeight
                                                                         && IsAbleToPassParcel(drone, parcel/*converted*/)
                                                                        );

            if (parcels.Count == 0) return;
            int parcelIndex = 0 , i;

            foreach(var parcel in parcels)
            {
                i = parcels.IndexOf(parcel);
                Location sender = parcels[i].SenderId/*converted to customer???*/;
                Location target = parcels[i].TargetId/*converted to customer???*/;
                
                if(parcels[i].Priority > parcels[parcelIndex].Priority)
                {
                    parcelIndex = i;
                }
                else if(parcels[i].Priority == parcels[parcelIndex].Priority)
                {
                    if(parcels[i].Weight > parcels[parcelIndex].Weight)
                    {
                        parcelIndex = i;
                    }
                    else if(parcels[i].Weight == parcels[parcelIndex].Weight)
                    {
                        if (Location.Distance(sender, drone.Location) > Location.Distance(sender, drone.Location))
                        {
                            parcelIndex = i;
                        }
                    }
                }
            }

            Dal.AssignParcelToDrone(parcels[parcelIndex].Id, droneId);

            //TODO how to update drone in list???
            DroneForList droneForList = drones.First(d => d.Id == drone.Id);
            droneForList.State = DroneState.DELIVER;
        }

        public void FinishCharging(int droneId, double timeInCharge)
        {
            DroneForList drone = drones.FirstOrDefault(d => d.Id == droneId);

            if (drone.Equals(default))
                throw new ObjectNotFoundException();

            if (drone.State != DroneState.MEINTENENCE)
                throw new InValidActionException();

            drone.Battery += ChargeRate * timeInCharge;
            drone.State = DroneState.FREE;

            //add an empty chargeSlot in Station- where?

            Dal.FinishCharging(drone.Id);
        }

        public void PickUpParcel(int droneId)
        {
            DroneForList drone = drones.FirstOrDefault(d => d.Id == droneId);

            if (drone.Equals(default))
                throw new ObjectNotFoundException();

            if (!drone.DeliveredParcelId.HasValue)
                throw new InValidActionException();

            var parcel = (IDAL.DO.Parcel)Dal.GetById(typeof(IDAL.DO.Parcel), drone.DeliveredParcelId.Value);

            if(parcel.PickedUp != DateTime.MinValue)
                throw new InValidActionException();

            ParcelInDeliver parcelInDeliver = parcel /*converted*/;

            drone.Battery -= Location.Distance(drone.CurrentLocation, parcelInDeliver.Collect) * ElectricityConfumctiolFree;
            drone.CurrentLocation = parcelInDeliver.Collect;

            Dal.CollectParcel(parcel.Id);
        }

        public void RenameDrone(int droneId, string newName)
        {
            DroneForList drone = drones.FirstOrDefault(d => d.Id == droneId);
            if (drone.Equals(default))
                throw new DalObject.ObjectNotFoundException;

            drones.Remove(drone);
            drone.Model = newName;
            drones.Add(drone);

            var dlDrone = (IDAL.DO.Drone)Dal.GetById(typeof(IDAL.DO.Drone), droneId);

            Dal.Remove(dlDrone);
            dlDrone.Model = newName;
            Dal.Add(dlDrone);
        }

        public void SendDroneToCharge(int droneId)
        {
            DroneForList drone = drones.FirstOrDefault(d => d.Id == droneId);

            if (drone.Equals(default))
                throw new ObjectNotFoundException();

            if (drone.State != DroneState.FREE)
                throw new InValidActionException();

            List<BaseStation> baseStations = Dal.GetList(typeof(IDAL.DO.BaseStation))/*converted to base station*/;
            List<BaseStation> availableBaseStations = baseStations.FindAll(station => station.EmptyChargeSlots > 0);

            BaseStation closest = closestBaseStaion(drone.CurrentLocation, availableBaseStations); // new implement?

            if (!IsEnoughBattery(drone/*converted*/, closest.Location))
                throw new InValidActionException();

            drone.CurrentLocation = closest.Location;
            drone.State = DroneState.MEINTENENCE;
            drone.Battery -= ElectricityConfumctiolFree * Location.Distance(closest.Location, drone.CurrentLocation);

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
            
            drone.Battery -= Location.Distance(drone.CurrentLocation, parcelInDeliver.Target) * GetElectricity(parcelInDeliver.Weight);
            drone.CurrentLocation = parcelInDeliver.Target;
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
           return drone.Battery >= (Location.Distance(drone.Location, parcel.Collect) * ElectricityConfumctiolFree +
                Location.Distance(parcel.Collect, parcel.Target) * GetElectricity(parcel.Weight) +
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
