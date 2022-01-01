using System.Windows.Controls;

namespace PL.ViewModels
{
    class ParcelViewModel: ContentControl
    {
        public ParcelViewModel()
        {
        }

        public ParcelViewModel(int id)
        {
            Content = new ParcelDetailsViewModel(id);
        }
    }
}
