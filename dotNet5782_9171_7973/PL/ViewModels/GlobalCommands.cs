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

            Type type = item.GetType();
            if (type == typeof(BaseStationForList))
            {
                return ((BaseStationForList)item).BusyChargeSlots == 0;
            }
            else if (type == typeof(CustomerForList))
            {
                CustomerForList customer = (CustomerForList)item;

                return customer.ParcelsSendAndNotSupplied == 0
                       && customer.ParcelsSendAndSupplied == 0
                       && customer.ParcelsOnWay == 0
                       && customer.ParcelsRecieved == 0;
            }
            else if (type == typeof(DroneForList))
            {
                return ((DroneForList)item).State == DroneState.Free;
            }
            else if (type == typeof(ParcelForList))
            {
                return !((ParcelForList)item).IsOnWay;
            }
            else
            {
                throw new InvalidOperationException("This object can not be deleted");
            }
        }

        static void Delete(object item)
        {
            Type type = item.GetType();
            if (type == typeof(BaseStationForList))
            {
                BaseStationForList baseStation = (BaseStationForList)item;
                PLService.DeleteBaseStation(baseStation.Id);
                Workspace.RemovePanelCommand.Execute(Workspace.BaseStationPanelName(baseStation.Id));
            }
            else if (type == typeof(CustomerForList))
            {
                PLService.DeleteCustomer(((CustomerForList)item).Id);
            }
            else if (type == typeof(DroneForList))
            {
                DroneForList drone = (DroneForList)item;
                PLService.DeleteDrone(drone.Id);
                Workspace.RemovePanelCommand.Execute(Workspace.DronePanelName(drone.Id));
            }
            else if (type == typeof(ParcelForList))
            {
                PLService.DeleteParcel(((ParcelForList)item).Id);
            }
            else
            {
                throw new InvalidOperationException("This object can not be deleted");
            }
        }
    }
}
