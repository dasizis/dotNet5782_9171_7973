using System.Windows.Controls;

namespace PL.Views
{
    /// <summary>
    /// Interaction logic for StationDetailsView.xaml
    /// </summary>
    public partial class StationDetailsView : UserControl
    {
        public StationDetailsView(int id)
        {
            InitializeComponent();
            DataContext = new ViewModels.StationDetailsViewModel(id);
        }
    }
}
