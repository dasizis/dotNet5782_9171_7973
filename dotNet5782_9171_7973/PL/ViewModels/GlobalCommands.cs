using PO;
using System;

namespace PL.ViewModels
{
    public static class GlobalCommands
    {
        /// <summary>
        /// A command to delete an item
        /// </summary>
        public static RelayCommand<object> DeleteCommand { get; }

        static GlobalCommands()
        {
            DeleteCommand = new(Delete, CanDelete);
        }

        /// <summary>
        /// Indicates wheather an item can be deleted or not
        /// </summary>
        /// <param name="item">item to check ability to be deleted</param>
        /// <returns>wheather an item can be deleted or not</returns>
        static bool CanDelete(object item)
        {
            if (item == null) return false;

            //base station - no drones are being charged
            if (item is BaseStationForList baseStation)
            {
                return baseStation.BusyChargeSlots == 0;
            }
            //customer - no related parcels are on way
            else if (item is CustomerForList customer)
            {
                return customer.ParcelsSendAndNotSupplied == 0
                       && customer.ParcelsSendAndSupplied == 0
                       && customer.ParcelsOnWay == 0
                       && customer.ParcelsRecieved == 0;
            }
            //drone - not working automaticaly and has free state
            else if (item is DroneForList drone)
            {
                if (PLSimulators.Simulators.ContainsKey(drone.Id))
                {
                    return !PLSimulators.Simulators[drone.Id].IsBusy;
                }
                return ((DroneForList)item).State == DroneState.Free;
            }
            //parcel - not on way
            else if (item is ParcelForList parcel)
            {
                return !parcel.IsOnWay;
            }
            else
            {
                throw new InvalidOperationException("This object can not be deleted");
            }
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="item">item to delete</param>
        static void Delete(object item)
        {
            Type type = item.GetType();
            int id;
            if (item is BaseStationForList baseStation)
            {
                PLService.DeleteBaseStation(baseStation.Id);
                Workspace.RemovePanelCommand.Execute(Workspace.BaseStationPanelName(baseStation.Id));
                id = baseStation.Id;
            }
            else if (item is CustomerForList customer)
            {
                PLService.DeleteCustomer(customer.Id);
                id = customer.Id;
            }
            else if (item is DroneForList drone)
            {
                PLService.DeleteDrone(drone.Id);
                Workspace.RemovePanelCommand.Execute(Workspace.DronePanelName(drone.Id));
                id = drone.Id;
            }
            else if (item is ParcelForList parcel)
            {
                PLService.DeleteParcel(parcel.Id);
                id = parcel.Id;
            }
            else
            {
                throw new InvalidOperationException("This object can not be deleted");
            }

            MessageBox.Show(MessageBox.BoxType.Info, $"{type.Name.Replace("ForList","")} #{id} was deleted ");
        }
    }
}
