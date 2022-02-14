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
        private int id;
        [Required(ErrorMessage = "Required")]
        public int Id
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
            SignInCommand = new(SignIn);
        }

        public void SignIn()
        {
            try
            {
                PO.Customer customer = PLService.GetCustomer(Id);
                if (customer.Name == Name)
                {
                    //Get in with proper customer
                    //temp
                    new Views.WorkspaceWindow(customer.Id).Show();
                    Views.WorkspaceView.AddPanelCommand.Execute(Workspace.CustomerPanel(Id));
                }
                else
                { 
                    MessageBox.Show("User name is not correct.");
                }
            }
            catch (ObjectNotFoundException)
            {
                MessageBox.Show("User Id is not correct.");
            }
        }
    }
}
