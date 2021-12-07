using System;
using System.Collections.Generic;
using System.Text;
using StringUtilities;

namespace DO
{
    public class Drone : IIdentifiable
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategory MaxWeight { get; set; }

        public override string ToString() => this.ToStringProperties();
    }
}
