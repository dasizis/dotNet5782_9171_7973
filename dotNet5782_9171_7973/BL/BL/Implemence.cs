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
    }
}
