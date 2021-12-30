using System.Windows;
using PO;
using StringUtilities;

namespace PL.ViewModels
{
    class DroneDetailsViewModel
    {
        public Drone Drone { get; set; }

        public bool IsInCharge => Drone.State == DroneState.Maintenance;

        public RelayCommand ProceedWithParcelCommand { get; set; }

        public RelayCommand HandleChargeCommand { get; set; }

        public RelayCommand RenameDroneCommand { get; set; }

        public RelayCommand DeleteCommand { get; set; }

        public RelayCommand ViewParcelCommand { get; set; }
        public string ProceedWithParcelText
        {
            get => Drone.State == DroneState.Deliver
                   ? Drone.ParcelInDeliver.WasPickedUp ? "Supply Parcel" : "Pick Parcel Up"
                   : "Assign Parcel to Drone";
        }

        public DroneDetailsViewModel(int id)
        {
            Drone = PLService.GetDrone(id);

            DronesNotification.DronesChangedEvent += LoadDrone;

            ProceedWithParcelCommand = new(ProceedWithParcel, () => Drone.State != DroneState.Maintenance);
            HandleChargeCommand = new(HandleCharge, () => Drone.State != DroneState.Deliver);
            RenameDroneCommand = new(RenameDrone, () => Drone.Error == null);
            DeleteCommand = new(Delete, () => Drone.State == DroneState.Free);
            ViewParcelCommand = new(() => Workspace.AddPanelCommand.Execute(Workspace.ParcelPanel(Drone.ParcelInDeliver?.Id)), 
                                    () => Drone.ParcelInDeliver != null);
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
                    try
                    {
                        PLService.SupplyParcel(Drone.Id);
                        MessageBox.Show($"Drone {Drone.Id} supplied parcel");
                    }
                    catch (BO.InvalidActionException e)
                    {
                        MessageBox.Show(e.Message);
                    }
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
            DronesNotification.DronesChangedEvent -= LoadDrone;

            PLService.RenameDrone(Drone.Id, Drone.Model);
            MessageBox.Show($"Drone {Drone.Id} renamed succesfully to {Drone.Model}");
        }

        private void Delete()
        {
            PLService.DeleteDrone(Drone.Id);
            Workspace.RemovePanelCommand.Execute($"{nameof(Views.DroneDetailsView).CamelCaseToReadable()} {Drone.Id}");
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
