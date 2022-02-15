using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public record DroneSimulatorChanges(int? BaseStation = null, int? Customer = null, int? Parcel = null);
}
