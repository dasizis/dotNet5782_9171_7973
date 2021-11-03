using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    partial class BL
    {
        public IDAL.IDal DalObject { get; set; } = new DalObject.DalObject();
        public int ElectricityConfumctiolFree { get; set; }
        public int ElectricityConfumctiolLight { get; set; }
        public int ElectricityConfumctiolMedium { get; set; }
        public int ElectricityConfumctiolHeavy { get; set; }
        public int ChargeRate { get; set; }


    }
}
