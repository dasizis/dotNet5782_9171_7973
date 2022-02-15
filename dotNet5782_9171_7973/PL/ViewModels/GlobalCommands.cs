using PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    public static class GlobalCommands
    {
        public static RelayCommand<object> DeleteCommand { get; }

        static GlobalCommands()
        {
            DeleteCommand = new(Delete, CanDelete);
        }
        
        static bool CanDelete(object item)
        {
            if (item == null) return false;

            if (item is BaseStationForList baseStation)
            {
                return baseStation.BusyChargeSlots == 0;
            }
            else if (item is CustomerForList customer)
            {
                return customer.ParcelsSendAndNotSupplied == 0
                       && customer.ParcelsSendAndSupplied == 0
                       && customer.ParcelsOnWay == 0
                       && customer.ParcelsRecieved == 0;
            }
            else if (item is DroneForList drone)
            {
                if (PLSimulators.Simulators.ContainsKey(drone.Id))
                {
                    return !PLSimulators.Simulators[drone.Id].IsBusy;
                }
                return ((DroneForList)item).State == DroneState.Free;
            }
            else if (item is ParcelForList parcel)
            {
                return !parcel.IsOnWay;
            }
            else
            {
                throw new InvalidOperationException("This object can not be deleted");
            }
        }

        static void Delete(object item)
        {
            Type type = item.GetType();
            if (item is BaseStationForList baseStation)
            {
                PLService.DeleteBaseStation(baseStation.Id);
                Workspace.RemovePanelCommand.Execute(Workspace.BaseStationPanelName(baseStation.Id));
            }
            else if (item is CustomerForList customer)
            {
                PLService.DeleteCustomer(customer.Id);
            }
            else if (item is DroneForList drone)
            {
                PLService.DeleteDrone(drone.Id);
                Workspace.RemovePanelCommand.Execute(Workspace.DronePanelName(drone.Id));
            }
            else if (item is ParcelForList parcel)
            {
                PLService.DeleteParcel(parcel.Id);
            }
            else
            {
                throw new InvalidOperationException("This object can not be deleted");
            }
        }
    }
}
