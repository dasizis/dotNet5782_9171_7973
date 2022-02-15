using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    class Simulator
    {
        public BackgroundWorker Worker { get; set; }
        public bool IsBusy { get; set; }

        public Simulator(BackgroundWorker worker, bool isBusy = true)
        {
            Worker = worker;
            IsBusy = isBusy;
        }
    }
    static class PLSimulators
    {
        public static Dictionary<int, Simulator> Simulators { get; set; } = new();
        public static Dictionary<int, bool> AreBusy { get; set; } = new();

        public static RelayCommand<int> StartSimulatorCommand;
        public static RelayCommand<int> StopSimulatorCommand;
        public static RelayCommand<int> ToggleSimulatorCommand;

        public static void StartSimulator(int id)
        {
            if (Simulators.ContainsKey(id))
            {
                if (CanStartSimulator(id))
                {
                    Simulators[id].Worker.RunWorkerAsync();
                    Simulators[id].IsBusy = true;
                }
            }
            else
            {
                BackgroundWorker worker = new()
                {
                    WorkerSupportsCancellation = true,
                    WorkerReportsProgress = true,
                };

                worker.DoWork += (sender, args) =>
                    BLApi.BLFactory.GetBL().StartDroneSimulator(id, changes => worker.ReportProgress(0, changes), () => worker.CancellationPending);

                worker.ProgressChanged += (sender, args) =>
                {
                    PLNotification.DroneNotification.NotifyItemChanged(id);
                    
                    DroneSimulatorChanges  changes = args.UserState as DroneSimulatorChanges;
                    
                    if (changes.Customer != null)
                    {
                        PLNotification.CustomerNotification.NotifyItemChanged((int)changes.Customer);
                    }

                    if (changes.BaseStation != null)
                    {
                        PLNotification.BaseStationNotification.NotifyItemChanged((int)changes.BaseStation);
                    }

                    if (changes.Parcel != null)
                    {
                        PLNotification.ParcelNotification.NotifyItemChanged((int)changes.Parcel);
                    }
                };

                worker.RunWorkerCompleted += (sender, args) =>
                {
                    Simulators[id].IsBusy = false;
                    PLNotification.DroneNotification.NotifyItemChanged(id);
                };

                worker.RunWorkerAsync();

                Simulators.Add(id, new Simulator(worker));
            }
        }

        public static void StopSimulator(int id)
        {
            if (Simulators.ContainsKey(id))
            {
                Simulators[id].Worker.CancelAsync();
            }
        }

        public static bool CanStartSimulator(int id)
        {
            if (!Simulators.ContainsKey(id)) return true;

            return !Simulators[id].IsBusy;
        }


        public static bool IsNowStopping(int id)
        {
            return Simulators.ContainsKey(id) && Simulators[id].IsBusy && Simulators[id].Worker.CancellationPending;
        }
        

        static PLSimulators()
        {
            StartSimulatorCommand = new(StartSimulator, CanStartSimulator);
            StopSimulatorCommand = new(StopSimulator, id => !CanStartSimulator(id));
            ToggleSimulatorCommand = new(id =>
            {
                if (CanStartSimulator(id)) StartSimulator(id);
                else StopSimulator(id);
            });
        }

    }
}
