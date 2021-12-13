using System;
using System.Collections.Generic;
using System.Text;
using StringUtilities;

namespace DO
{
    public struct DroneCharge
    {
        public int StationId { get; set; }
        public int DroneId { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsDeleted { get; set; }

        public override string ToString() => this.ToStringProperties();
    }
}
