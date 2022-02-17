using PO;
using System.Collections.ObjectModel;

namespace PL.ViewModels
{
    class DroneDetailsViewModel
    {
        /// <summary>
        /// The model drone 
        /// </summary>
        public Drone Drone { get; set; } = new();

        /// <summary>
        /// List of markers to display on drone's map
        /// </summary>
        public ObservableCollection<MapMarker> Markers { get; set; } = new();

        /// <summary>
        /// Proceed with related parcel to the next step in delivery command
        /// </summary>
        public RelayCommand ProceedWithParcelCommand { get; set; }

        /// <summary>
        /// Send to charge or release drone from charging command
        /// </summary>
        public RelayCommand HandleChargeCommand { get; set; }

        /// <summary>
        /// Rename drone command
        /// </summary>
        public RelayCommand RenameDroneCommand { get; set; }

        /// <summary>
        /// Delete drone command
        /// </summary>
        public RelayCommand DeleteCommand { get; set; }

        /// <summary>
        /// Open panel of related parcel's details command
        /// </summary>
        public RelayCommand ViewParcelCommand { get; set; }

        /// <summary>
        /// Constructor
        /// Initialize drone and commands
        /// </summary>
        /// <param name="id">id of the model drone</param>
        public DroneDetailsViewModel(int id)
        {
            Drone.Id = id;
            LoadDrone();

            PLNotification.DroneNotification.AddHandler(LoadDrone, id);

            ProceedWithParcelCommand = new(ProceedWithParcel, () => Drone.State != DroneState.Maintenance);
            HandleChargeCommand = new(HandleCharge, () => Drone.State != DroneState.Deliver);
            RenameDroneCommand = new(RenameDrone, () => Drone.Error == null);
            DeleteCommand = new(Delete, () => Drone.State == DroneState.Free);
            ViewParcelCommand = new(() => Workspace.AddPanelCommand.Execute(Workspace.ParcelPanel(Drone.ParcelInDeliver?.Id)),
                                    () => Drone.ParcelInDeliver != null);
        }

        /// <summary>
        /// Proceed with related parcel to the next step in delivery
        /// </summary>
        private void ProceedWithParcel()
        {
            //drone's state is Free
            //step: assign parcel to drone
            if (Drone.State == DroneState.Free)
            {
                try
                {
                    PLService.AssignParcelToDrone(Drone.Id);
                }
                catch (BO.InvalidActionException e)
                {
                    MessageBox.Show(MessageBox.BoxType.Error, e.Message);
                }
            }
            //drone's state is Deliver
            else
            {
                //Parcel was not picked up yet
                //step: pick up parcel
                if (!Drone.ParcelInDeliver.WasPickedUp)
                {
                    PLService.PickUpParcel(Drone.Id);
                }
                //parcel was not supplied yet
                else
                {
                    //step: supply parcel
                    try
                    {
                        PLService.SupplyParcel(Drone.Id);
                    }
                    catch (BO.InvalidActionException e)
                    {
                        MessageBox.Show(MessageBox.BoxType.Error, e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Handle drone charge
        /// </summary>
        private void HandleCharge()
        {
            //if drone needs to charge
            //step: charge drone 
            if (Drone.State == DroneState.Free)
            {
                try
                {
                    PLService.ChargeDrone(Drone.Id);
                }
                catch (BO.InvalidActionException e)
                {
                    MessageBox.Show(MessageBox.BoxType.Error, e.Message);
                }
            }
            //if drone needs to be released
            //step: finish charging
            else if (Drone.State == DroneState.Maintenance)
            {
                try
                {
                    PLService.FinishCharging(Drone.Id);
                }
                catch (BO.InvalidActionException e)
                {
                    MessageBox.Show(MessageBox.BoxType.Error, e.Message);
                }
            }
        }

        /// <summary>
        /// Rename drone
        /// </summary>
        private void RenameDrone()
        {
            PLService.RenameDrone(Drone.Id, Drone.Model);
            MessageBox.Show(MessageBox.BoxType.Success, $"Drone #{Drone.Id} renamed succesfully to {Drone.Model}");
        }

        /// <summary>
        /// Delete drone
        /// </summary>
        private void Delete()
        {
            PLService.DeleteDrone(Drone.Id);
            Workspace.RemovePanelCommand.Execute(Workspace.DronePanelName(Drone.Id));
            MessageBox.Show(MessageBox.BoxType.Info, $"Drone #{Drone.Id} deleted");
            
        }

        /// <summary>
        /// Load drone
        /// </summary>
        private void LoadDrone()
        {
            Drone.Reload(PLService.GetDrone(Drone.Id));
            //load map
            Markers.Clear();
            Markers.Add(MapMarker.FromType(Drone));
        }
    }
}
