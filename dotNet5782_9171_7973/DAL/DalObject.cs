using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace DalObject
{
    public struct DalObject
    {
        //DalObject()
        //{
        //    DataSource.Initialize();
        //}

        public void AddParcel(Parcel parcel)
        {
            DataSource.parcels.Add(parcel);
        }

        public void AddBaseStation(BaseStation baseStation)
        {
            DataSource.baseStations.Add(baseStation);
        }

        public void AddCustomer(Customer customer)
        {
            DataSource.customers.Add(customer);
        }

        public void AddDrone(Drone drone)
        {
            DataSource.drones.Add(drone);
        }

        //public void Add(object item)
        //{
        //    var field = typeof(DataSource).GetField(item.GetType().Name);
        //    var tempDataSource = new DataSource();

        //    Console.WriteLine(field.GetValue(tempDataSource));
        //    typeof(DataSource).GetMember("drones");
        //}
    }
}
