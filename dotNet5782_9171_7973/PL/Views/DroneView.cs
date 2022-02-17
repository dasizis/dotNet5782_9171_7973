using System.Windows.Controls;

namespace PL.Views
{
    class DroneView : ContentControl
    {
        public DroneView()
        {
            Content = new AddDroneView();
        }
        public DroneView(int id)
        {
            Content = new DroneDetailsView(id);
        }
    }
}
