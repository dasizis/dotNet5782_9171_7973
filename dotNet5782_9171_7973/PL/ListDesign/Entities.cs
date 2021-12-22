﻿using StringUtilities;

namespace PL.ListDesign
{
    class TreeViewProp
    {
        public string PropName { get; set; }
        public object propValue { get; set; }
    }
    
    class Longitude 
    { 
        public double Long { get; set; }
        public override string ToString()
        {
            return Sexadecimal.Longitde(Long);
        }
    }

    class Latitude 
    {
        public double Lat { get; set; }
        public override string ToString()
        {
            return Sexadecimal.Latitude(Lat);
        }
    }

    class Battery
    {
        public double BatteryValue { get; set; }
    }
}
