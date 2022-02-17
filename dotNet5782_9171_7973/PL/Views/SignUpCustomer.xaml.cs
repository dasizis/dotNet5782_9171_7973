using System.Windows.Controls;

namespace PL.Views
{
    /// <summary>
    /// Interaction logic for SignUpCustomer.xaml
    /// </summary>
    public partial class SignUpCustomer : UserControl
    {
        public SignUpCustomer()
        {
            InitializeComponent();
            DataContext = new ViewModels.SignUpCustomerViewModel();
        }
    }
}
