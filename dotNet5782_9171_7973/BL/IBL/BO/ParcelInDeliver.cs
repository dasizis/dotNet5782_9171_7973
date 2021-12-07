using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringUtilities;


namespace IBL.BO
{
    public class ParcelInDeliver
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
        public bool Position { get; set; }
        public Location CollectLocation { get; set; }
        public Location TargetLocation { get; set; }
        public double DeliveryDistance { get; set; }
        public CustomerInDelivery Sender { get; set; }
        public CustomerInDelivery Target { get; set; }
        public override string ToString() => this.ToStringProperties();

    }
}
