using System.Windows.Controls;

namespace PL.Views
{
    class ParcelView: ContentControl
    {
        public ParcelView()
        {
            Content = new ViewModels.AddParcelViewModel();
        }

        public ParcelView(int id)
        {
            Content = new ViewModels.ParcelDetailsViewModel(id);
        }
    }
}
