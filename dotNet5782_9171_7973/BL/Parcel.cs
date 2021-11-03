using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DateTime Create { get; set; }
        public DateTime Assign { get; set; }
        public DateTime PickUp { get; set; }
        public DateTime Supply { get; set; }
    }
}
