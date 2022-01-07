using System.Windows.Controls;

namespace PL.Views
{
    class DroneView : ContentControl
    {
        public DroneView()
        {
            Content = new ViewModels.AddDroneViewModel();
        }
        public DroneView(int id)
        {
            Content = new ViewModels.DroneDetailsViewModel(id);
        }
    }
}
