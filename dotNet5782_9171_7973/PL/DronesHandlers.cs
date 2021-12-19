using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    static class DronesHandlers
    {
        public delegate void DronesChangedHandler();

        public static event DronesChangedHandler DronesChangedEvent;
        public static void NotifyDroneChanged()
        {
            DronesChangedEvent?.Invoke();
        }
    }
}
