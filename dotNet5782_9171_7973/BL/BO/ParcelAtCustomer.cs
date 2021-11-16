using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelAtCustomer
    {
        public int Id { get; set; }
        public WeightCategory Weight { get; set; }
        public Priority Priority { get; set; }
        public ParcelState State { get; set; }
        public int MyProperty { get; set; }
        public CustomerInDelivery OtherCustomer { get; set; }

    }
}
