using System.Windows.Controls;

namespace PL.Views
{
    /// <summary>
    /// Interaction logic for DroneDetailsView.xaml
    /// </summary>
    public partial class DroneDetailsView : UserControl
    {
        public DroneDetailsView(int id)
        {
            InitializeComponent();
            DataContext = new ViewModels.DroneDetailsViewModel(id);

        }
    }
}
