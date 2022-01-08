using System.Windows.Controls;

namespace PL.Views
{
    class BaseStationView : ContentControl
    {
        public BaseStationView()
        {
            Content = new AddStationView();
        }
        public BaseStationView(int id)
        {
            Content = new StationDetailsView(id);
        }
    }
}
