using System.Windows.Controls;

namespace PL.Views
{
    class ParcelView: ContentControl
    {
        public ParcelView()
        {
            Content = new AddParcelView();
        }

        public ParcelView(int id)
        {
            Content = new ParcelDetailsView(id);
        }
    }
}
