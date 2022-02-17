using BO;
using PO;
using System.ComponentModel.DataAnnotations;

namespace PL.ViewModels
{
    class SignInCustomerViewModel : PropertyChangedNotification
    {
        //Should be built new entity?
        /// <summary>
        /// Customer id
        /// </summary>
        private int? id;
        [Required(ErrorMessage = "Required")]
        public int? Id
        {
            get => id;
            set => SetField(ref id, value);
        }

        /// <summary>
        /// Customer name
        /// </summary>
        private string name;
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[a-zA-Z ]{4,14}", ErrorMessage = "Name must match a 4-14 letters only format")]
        public string Name 
        { 
            get => name;
            set => SetField(ref name, value);
        }

        /// <summary>
        /// A command to sign in
        /// </summary>
        public RelayCommand SignInCommand { get; set; }

        /// <summary>
        /// Constractor
        /// Initialize command
        /// </summary>
        public SignInCustomerViewModel()
        {
            SignInCommand = new(SignIn, () => Error == null);
        }

        /// <summary>
        /// Sign in
        /// </summary>
        public void SignIn()
        {
            try
            {
                PO.Customer customer = PLService.GetCustomer((int)Id);
                if (customer.Name == Name)
                {
                    ManageWindows.OpenAppWindow(customer.Id);
                    ManageWindows.CloseRegisterWindow();
                }
                //if no customer has the entered name
                else MessageBox.Show(MessageBox.BoxType.Error, "User name is not correct.", 250);
            }
            //if no customer has the entered id
            catch (ObjectNotFoundException)
            {
                MessageBox.Show(MessageBox.BoxType.Error, "User Id not found.", 250);
            }
        }
    }
}
