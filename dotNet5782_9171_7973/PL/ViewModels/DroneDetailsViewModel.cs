using System.Windows;
using PO;

namespace PL.ViewModels
{
    class DroneDetailsViewModel
    {
        public Drone Drone { get; set; }

        public RelayCommand ProceedWithParcelCommand { get; set; }

        public RelayCommand HandleChargeCommand { get; set; }

        public RelayCommand RenameDroneCommand { get; set; }

        public RelayCommand DeleteCommand { get; set; }

        public DroneDetailsViewModel(int id)
        {
            Drone = PLService.GetDrone(id);

            DronesNotification.DronesChangedEvent += LoadDrone;

            ProceedWithParcelCommand = new(ProceedWithParcel, () => Drone.State != DroneState.Maintenance);
            HandleChargeCommand = new(HandleCharge, () => Drone.State != DroneState.Deliver);
            RenameDroneCommand = new(RenameDrone, () => Drone.Error == null);
            DeleteCommand = new(Delete, () => Drone.State == DroneState.Free);

            LoadDrone();
        }

        private void ProceedWithParcel()
        {
            if (Drone.State == DroneState.Free)
            {
                try
                {
                    PLService.AssignParcelToDrone(Drone.Id);
                    MessageBox.Show($"Drone {Drone.Id} assigned to parcel {Drone.ParcelInDeliver.Id}");
                }
                catch (BO.InvalidActionException e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                if (!Drone.ParcelInDeliver.WasPickedUp)
                {
                    PLService.PickUpParcel(Drone.Id);
                    MessageBox.Show($"Drone {Drone.Id} Pick parcel {Drone.ParcelInDeliver.Id} up");
                }
                else
                {
                    PLService.SupplyParcel(Drone.Id);
                    MessageBox.Show($"Drone {Drone.Id} supplied parcel");
                }
            }
        }

        private void HandleCharge()
        {

            if (Drone.State == DroneState.Free)
            {
                try
                {
                    PLService.ChargeDrone(Drone.Id);
                    MessageBox.Show("Drone was send to charge");
                }
                catch (BO.InvalidActionException e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else if (Drone.State == DroneState.Maintenance)
            {
                try
                {
                    PLService.FinishCharging(Drone.Id);
                    MessageBox.Show("Drone realesed from charging");
                }
                catch (BO.InvalidActionException e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        private void RenameDrone()
        {
            PLService.RenameDrone(Drone.Id, Drone.Model);
            MessageBox.Show($"Drone {Drone.Id} renamed succesfully to {Drone.Model}");
            WasChanged = false;
        }

        private void Delete()
        {
            PLService.DeleteDrone(Drone.Id);
            MessageBox.Show($"Drone {Drone.Id} deleted");
            // Close Tab 
        }

        private void LoadDrone()
        {
            string model = Drone.Model;
            Drone.Reload(PLService.GetDrone(Drone.Id));
            Drone.Model = model;
        }
    }
}
