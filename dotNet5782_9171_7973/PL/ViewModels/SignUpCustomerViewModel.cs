using PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.ViewModels
{
    class SignUpCustomerViewModel
    {
        public CustomerToAdd Customer { get; set; } = new();
        public RelayCommand SignUpCommand { get; set; }

        public SignUpCustomerViewModel()
        {
            SignUpCommand = new(SignUp, () => Customer.Error == null);
        }
        void SignUp()
        {
            try
            {
                PLService.AddCustomer(Customer);
                var registerWindow = App.Current.Windows.Cast<Window>().Single(w => w.Title == "Welcome Window");
                new Views.WorkspaceWindow((int)Customer.Id).Show();
                registerWindow.Close();
            }
            catch(BO.IdAlreadyExistsException)
            {
                MessageBox.Show(MessageBox.BoxType.Warning, "Your Password is used by another user.\n Try a different one.");
            }
            
        }
    }
}
