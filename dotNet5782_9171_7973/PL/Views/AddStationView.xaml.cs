using System.Windows.Controls;

namespace PL.Views
{
    /// <summary>
    /// Interaction logic for AddStationView.xaml
    /// </summary>
    public partial class AddStationView : UserControl
    {
        public AddStationView()
        {
            InitializeComponent();
            DataContext = new ViewModels.BaseStationViewModel().Content;
        }
    }
}
