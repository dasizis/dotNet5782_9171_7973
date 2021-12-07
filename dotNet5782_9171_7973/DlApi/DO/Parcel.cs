using System;
using System.Collections.Generic;
using System.Text;
using StringUtilities;

namespace DO
{
    public class Parcel : IIdentifiable
    {
        public int? DroneId { get; set; }
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int TargetId { get; set; }
        public WeightCategory Weight { get; set; }
        public Priority Priority { get; set; }
        public DateTime? Requested { get; set; }
        public DateTime? Scheduled { get; set; }
        public DateTime? PickedUp { get; set; }
        public DateTime? Supplied { get; set; }

        public override string ToString() => this.ToStringProperties();
    }
}
