using System.Windows.Controls;

namespace PL.Views
{
    class CustomerView : ContentControl
    {
        public CustomerView()
        {
            Content = new ViewModels.AddCustomerViewModel();
        }
        public CustomerView(int id)
        {
            Content = new ViewModels.CustomerDetailsViewModel(id);
        }
    }
}
