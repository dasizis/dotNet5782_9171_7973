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
                    thorw new ArgumentException();
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
                    thorw new ArgumentException();
                }
                emptyChargeSlots = value;
            } 
        }
        public List<Drone> DronesInChargeList { get; set; }
        public override string ToString() => this.ToStringProps(); 
    }
}
