using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringUtilities;

namespace IBL.BO
{
    public class BaseStationForList
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
        public int EmptyChargeSlots { get; set; }
        public int BusyChargeSlots { get; set; }
        public override string ToString() => this.ToStringProps();

    }
}
