using System;

namespace IBL.BO
{
    public class ParcelForList 
    {
        public int Id { get; set; }
        public string SenderName { get; set; }
        public string TargetName { get; set; }
        public WeightCategory Weight { get; set; }
        public Priority Priority { get; set; }
        public override string ToString() => this.ToStringProps();

    }
}
