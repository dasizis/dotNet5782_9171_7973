using System;
using System.Collections.Generic;
using System.Text;
using StringUtilities;

namespace IDAL.DO
{
    public struct Customer : IIdentifiable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        [SexadecimalLongitude]
        public double Longitude { get; set; }
        [SexadecimalLatitude]
        public double Latitude { get; set; }

        public override string ToString() => this.ToStringProperties();
    }
}
