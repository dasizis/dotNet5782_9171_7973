using System.Windows;

namespace PL.Views
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            InitializeComponent();
            DataContext = new ViewModels.WelcomeWindowViewModel();
        }
    }
}
