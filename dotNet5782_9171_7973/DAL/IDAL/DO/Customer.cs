using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL.DO
{
    public struct Customer : IIdentifiable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public override string ToString() => this.ToStringProps();
    }
}
