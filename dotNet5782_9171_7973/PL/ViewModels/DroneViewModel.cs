using System.Windows.Controls;

namespace PL.ViewModels
{
    class DroneViewModel : ContentControl
    {
        public DroneViewModel()
        {
            Content = new AddDroneViewModel();
        }
        public DroneViewModel(int id)
        {
            Content = new DroneDetailsViewModel(id);
        }
    }
}
