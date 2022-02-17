using PO;

namespace PL.ViewModels
{
    class SignUpCustomerViewModel
    {
        /// <summary>
        /// Customer to add
        /// </summary>
        public CustomerToAdd Customer { get; set; } = new();

        /// <summary>
        /// A command to sign up
        /// </summary>
        public RelayCommand SignUpCommand { get; set; }

        /// <summary>
        /// Constractor
        /// initialize command
        /// </summary>
        public SignUpCustomerViewModel()
        {
            SignUpCommand = new(SignUp, () => Customer.Error == null);
        }

        /// <summary>
        /// Sign up
        /// </summary>
        void SignUp()
        {
            try
            {
                PLService.AddCustomer(Customer);
                ManageWindows.OpenAppWindow((int)Customer.Id);
                ManageWindows.CloseRegisterWindow();
            }
            catch(BO.IdAlreadyExistsException)
            {
                MessageBox.Show(MessageBox.BoxType.Warning, "Your password is used by another user.\n Try a different one.", 250);
            }
            
        }
    }
}
