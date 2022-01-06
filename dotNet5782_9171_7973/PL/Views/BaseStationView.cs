using System.Windows.Controls;

namespace PL.Views
{
    class BaseStationView : ContentControl
    {
        public BaseStationView()
        {
            Content = new ViewModels.AddBaseStationViewModel();
        }
        public BaseStationView(int id)
        {
            Content = new ViewModels.StationDetailsViewModel(id);
        }
    }
}
