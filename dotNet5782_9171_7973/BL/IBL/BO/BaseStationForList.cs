﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class BaseStationForList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EmptyChargeSlots { get; set; }
        public int BusyChargeSlots { get; set; }
        public override string ToString() => this.ToStringProps();

    }
}
