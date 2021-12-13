﻿using StringUtilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public struct BaseStation : IIdentifiable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [SexadecimalLongitude]
        public double Longitude { get; set; }
        [SexadecimalLatitude]
        public double Latitude { get; set; }
        public int ChargeSlots { get; set; }

        public override string ToString() => this.ToStringProperties();
    }
}
