using StringUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class BaseStation: ILocalable
    {
        public int Id { get; set; }
        string name;
        public string Name 
        { 
            get => name;
            set
            {
                if (!Validation.IsValidName(value))
                {
                    throw new ArgumentException(value);
                }
                name = value;
            }
        }
        public Location Location { get; set; }
        int emptyChargeSlots;
        public int EmptyChargeSlots 
        { 
            get => emptyChargeSlots;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(value.ToString());
                }
                emptyChargeSlots = value;
            } 
        }
        public List<Drone> DronesInChargeList { get; set; }
        public override string ToString() => this.ToStringProperties(); 
    }
}
