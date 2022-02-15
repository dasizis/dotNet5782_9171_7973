using BO;
using PO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.ViewModels
{
    class SignInCustomerViewModel : PropertyChangedNotification
    {
        //Should be built new entity?
        private int? id;
        [Required(ErrorMessage = "Required")]
        public int? Id
        {
            get => id;
            set => SetField(ref id, value);
        }

        private string name;
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[a-zA-Z ]{4,14}", ErrorMessage = "Name must match a 4-14 letters only format")]
        public string Name 
        { 
            get => name;
            set => SetField(ref name, value);
        }
        public RelayCommand SignInCommand { get; set; }

        public SignInCustomerViewModel()
        {
            SignInCommand = new(SignIn, () => Error == null);
        }

        public void SignIn()
        {
            try
            {
                PO.Customer customer = PLService.GetCustomer((int)Id);
                if (customer.Name == Name)
                {
                    new Views.WorkspaceWindow(customer.Id).Show();
                    var registerWindow = App.Current.Windows.Cast<Window>().Single(w => w.Title == "Welcome Window");
                    registerWindow.Close();
                }
                else MessageBox.Show(MessageBox.BoxType.Error, "User name is not correct.", 250);
            }
            catch (ObjectNotFoundException)
            {
                MessageBox.Show(MessageBox.BoxType.Warning, "User Id is not correct.", 250);
            }
        }
    }
}
