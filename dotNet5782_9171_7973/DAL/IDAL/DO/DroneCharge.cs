﻿using System;
using System.Collections.Generic;
using System.Text;
using StringUtilities;

namespace IDAL.DO
{
    public struct DroneCharge
    {
        public int StationId { get; set; }
        public int DroneId { get; set; }

        public override string ToString() => this.ToStringProps();
    }
}