using System.Windows.Controls;

namespace PL.ViewModels
{
    class ParcelViewModel: ContentControl
    {
        public ParcelViewModel()
        {
            Content = new AddParcelViewModel();
        }

        public ParcelViewModel(int id)
        {
            Content = new ParcelDetailsViewModel(id);
        }
    }
}
