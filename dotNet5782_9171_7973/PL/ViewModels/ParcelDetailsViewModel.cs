using PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.ViewModels
{
    class ParcelDetailsViewModel
    {
        public Parcel Parcel { get; set; }

        public RelayCommand ShowSenderCommand { get; set; }
        public RelayCommand ShowTargetCommand { get; set; }
        public RelayCommand ShowDroneCommand { get; set; }

        public ParcelDetailsViewModel(int id)
        {
            Parcel = PLService.GetParcel(id);

            ShowSenderCommand = new(ExecuteShowSenderDetails);
            ShowTargetCommand = new(ExecuteShowTargetDetails);
            ShowDroneCommand = new(ExecuteShowDroneDetails, () => Parcel.Scheduled != null);

            ParcelsNotification.ParcelsChangedEvent += LoadParcel;
        }

        private void ExecuteShowSenderDetails()
        {
            MessageBox.Show("Sender Details");
        }

        private void ExecuteShowTargetDetails()
        {
            MessageBox.Show("Target Details");
        }

        private void ExecuteShowDroneDetails()
        {
            MessageBox.Show("Drone Details");
        }

        private void LoadParcel()
        {
            Parcel = PLService.GetParcel(Parcel.Id);
        }
    }
}
