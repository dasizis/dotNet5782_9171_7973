using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    class Prop { };
    class TreeViewProp : Prop
    {
        public string PropName { get; set; }
        public object propValue { get; set; }
    }

    class TreeViewLocationProp : Prop
    {
        public BO.Location Location { get; set; }
    }

    class Orientation {  };
    class Longitude: Orientation { public double Long { get; set; } }
    class Latitude: Orientation { public double Lat { get; set; } }
}
