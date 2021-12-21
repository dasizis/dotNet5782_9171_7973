using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringUtilities;

namespace PL
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
