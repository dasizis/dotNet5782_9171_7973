using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringUtilities;

namespace IBL.BO
{
    public class Parcel 
    {
        public int Id { get; set; }
        public CustomerInDelivery Sender { get; set; }
        public CustomerInDelivery Target { get; set; }
        public WeightCategory Weight { get; set; }
        public Priority Priority { get; set; }
        public Drone Drone { get; set; }
        public DateTime? Requested { get; set; }
        public DateTime? Scheduled { get; set; }
        public DateTime? PickedUp { get; set; }
        public DateTime? Supplied { get; set; }
        public override string ToString() => this.ToStringProps();


    }
}
