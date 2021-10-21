using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL.DO
{
    public struct DroneCharge
    {
        public int StationId { get; set; }

        public int DroneId { get; set; }

        /// <summary>
        /// Print charge slot information
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return (
                $"*****************************************\n"+
                $"Drone charge in station #{StationId} " +
                $"charges drone number {DroneId} \n"+
                $"*****************************************\n"
            );
        }
    }
}
