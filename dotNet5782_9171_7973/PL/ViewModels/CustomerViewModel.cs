using System.Windows.Controls;

namespace PL.ViewModels
{
    class CustomerViewModel : ContentControl
    {
        public CustomerViewModel()
        {
            Content = new AddCustomerViewModel();
        }
        public CustomerViewModel(int id)
        {
            Content = new CustomerDetailsViewModel(id);
        }
    }
}
