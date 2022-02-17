using System.Windows.Controls;

namespace PL.Views
{
    /// <summary>
    /// Interaction logic for ParcelDetailsView.xaml
    /// </summary>
    public partial class ParcelDetailsView : UserControl
    {
        public ParcelDetailsView(int id)
        {
            InitializeComponent();
            DataContext = new ViewModels.ParcelDetailsViewModel(id);
        }
    }
}
