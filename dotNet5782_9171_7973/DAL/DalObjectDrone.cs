using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace DalObject
{
    public partial class DalObject
    {
        /// <summary>
        /// adds a drone to dromnes list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="weight"></param>
        public void AddDrone(int id, string model, WeightCategory weight)
        {
            if (DataSource.drones.DoesIdExist(id))
            {
                throw new IdAlreadyExistsException(typeof(Drone), id);
            }

            Drone drone = new Drone()
            {
                Id = id,
                Model = model,
                MaxWeight = weight,
            };

            DataSource.drones.Add(drone);
        }
        
        /// <summary>
        /// returns the drones list as array
        /// </summary>
        /// <returns>an array of all the exist drones</returns>
        public IEnumerable<Drone> GetDroneList()
        {
            return DataSource.drones;
        }
    }
}
