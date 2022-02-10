using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using PO;

namespace PL.ViewModels
{
    class DroneDetailsViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The drone 
        /// </summary>
        public Drone Drone { get; set; } = new();

        private bool isAutoMode = false;
        /// <summary>
        /// Indicates whether the drone is ruuning automaticaly or not (manualy)
        /// </summary>
        public bool IsAutoMode 
        { 
            get => isAutoMode;
            set 
            {
                isAutoMode = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsAutoMode"));
                }
            }
        }
        /// <summary>
        /// Background worker which runs simulator
        /// </summary>
        BackgroundWorker worker;

        public event PropertyChangedEventHandler PropertyChanged;

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
        /// Handle simulator command (run or stop)
        /// </summary>
        public RelayCommand HandleSimulatorCommand { get; set; }

        public DroneDetailsViewModel(int id)
        {
            Drone.Id = id;
            LoadDrone();

            PLNotification.DroneNotification.AddHandler(LoadDrone, id);

            ProceedWithParcelCommand = new(ProceedWithParcel, () => Drone.State != DroneState.Maintenance);
            HandleChargeCommand = new(HandleCharge, () => Drone.State != DroneState.Deliver);
            RenameDroneCommand = new(RenameDrone, () => Drone.Error == null);
            DeleteCommand = new(Delete, () => Drone.State == DroneState.Free);
            HandleSimulatorCommand = new(HandleSimulator);
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
                    MessageBox.Show($"Drone {Drone.Id} assigned to parcel {Drone.ParcelInDeliver.Id}");
                }
                catch (BO.InvalidActionException e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            //drone's state is Deliver
            //(we do not get to this function with Maintanance state
            //since then drone does not have a related parcel)
            else
            {
                //Parcel was not picked up yet
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
            PLService.RenameDrone(Drone.Id, Drone.Model);
            MessageBox.Show($"Drone {Drone.Id} renamed succesfully to {Drone.Model}");
        }

        private void Delete()
        {
            PLService.DeleteDrone(Drone.Id);
            Workspace.RemovePanelCommand.Execute(Workspace.DronePanelName(Drone.Id));
        }

        private void UpdateDrone() => worker.ReportProgress(0);
        private bool ShouldStop() => worker.CancellationPending;

        private void HandleSimulator()
        {
            if (IsAutoMode)
            {
                IsAutoMode = false;
                worker?.CancelAsync();
                return;
            }

            IsAutoMode = true;
            worker = new() { WorkerReportsProgress = true, WorkerSupportsCancellation = true, };
            worker.DoWork += (sender, args) => BLApi.BLFactory.GetBL().StartDroneSimulator((int)args.Argument, UpdateDrone, ShouldStop);
            worker.RunWorkerCompleted += (sender, args) => IsAutoMode = false;
            worker.ProgressChanged += (sender, args) => PLNotification.DroneNotification.NotifyItemChanged(Drone.Id);
            worker.RunWorkerAsync(Drone.Id);
        }

        private void LoadDrone()
        {
            Drone.Reload(PLService.GetDrone(Drone.Id));

            Markers.Clear();
            Markers.Add(MapMarker.FromType(Drone));
        }
    }
}
