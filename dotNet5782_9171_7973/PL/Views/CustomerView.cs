using System.Windows.Controls;

namespace PL.Views
{
    class CustomerView : ContentControl
    {
        public CustomerView()
        {
            Content = new AddCustomerView();
        }
        public CustomerView(int id)
        {
            Content = new CustomerDetailsView(id);
        }
    }
}
