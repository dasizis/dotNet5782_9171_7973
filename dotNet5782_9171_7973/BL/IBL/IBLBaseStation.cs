using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IBL
{
    public interface IBLBaseStation
    {
        void AddBaseStation(int id, string name, Location location, int chargeSlots);
        void UpdateBaseStation(int baseStationId, string name = null, int? chargeSlots = null);
        IEnumerable<BaseStation> GetAvailableBaseStations();
    }
}
