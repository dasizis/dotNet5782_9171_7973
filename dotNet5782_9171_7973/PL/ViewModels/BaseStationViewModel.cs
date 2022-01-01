using System.Windows.Controls;

namespace PL.ViewModels
{
    class BaseStationViewModel : ContentControl
    {
        public BaseStationViewModel()
        {
            Content = new AddBaseStationViewModel();
        }
        public BaseStationViewModel(int id)
        {
            Content = new StationDetailsViewModel(id);
        }
    }
}
