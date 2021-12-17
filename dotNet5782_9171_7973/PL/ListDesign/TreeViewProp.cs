using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    class Prop { };
    class TreeViewProp
    {
        public string PropName { get; set; }
        public object propValue { get; set; }
    }

    class TreeViewLocationProp
    {
        public BO.Location Location { get; set; }
    }

    class Orientation {  };
    class Longitude { public double Long { get; set; } }
    class Latitude { public double Lat { get; set; } }
}
