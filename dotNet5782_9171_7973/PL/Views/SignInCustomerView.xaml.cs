using PL.ViewModels;
using System.Windows.Controls;

namespace PL.Views
{
    /// <summary>
    /// Interaction logic for SignInCustomerView.xaml
    /// </summary>
    public partial class SignInCustomerView : UserControl
    {
        public SignInCustomerView()
        {
            InitializeComponent();
            DataContext = new SignInCustomerViewModel();
        }
    }
}
