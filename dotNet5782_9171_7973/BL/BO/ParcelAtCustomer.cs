using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringUtilities;

namespace BO
{
    public class ParcelAtCustomer 
    {
        public int Id { get; set; }
        WeightCategory weight;
        public WeightCategory Weight
        {
            get => weight;
            set
            {
                if (!Validation.IsValidEnumOption<WeightCategory>((int)value))
                {
                    throw new ArgumentException(value.ToString());
                }
                weight = value;
            }
        }
        Priority priority;
        public Priority Priority
        {
            get => priority;
            set
            {
                if (!Validation.IsValidEnumOption<Priority>((int)value))
                {
                    throw new ArgumentException(value.ToString());
                }
                priority = value;
            }
        }
        ParcelState state;
        public ParcelState State
        {
            get => state;
            set
            {
                if (!Validation.IsValidEnumOption<ParcelState>((int)value))
                {
                    throw new ArgumentException(value.ToString());
                }
                state = value;
            }
        }
        public CustomerInDelivery OtherCustomer { get; set; }
        public override string ToString() => this.ToStringProperties();


    }
}
