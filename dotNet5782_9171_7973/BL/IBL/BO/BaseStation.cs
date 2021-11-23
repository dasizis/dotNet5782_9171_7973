using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class BaseStation: IDAL.DO.IIdentifiable, ILocalable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public int EmptyChargeSlots { get; set; }
        public List<Drone> DronesInChargeList { get; set; }
        public override string ToString() => this.ToStringProps(); 
    }
}
