using PL.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PL.ViewModels
{
    class WelcomeWindowViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Indicates sign in mode
        /// </summary>
        private bool isSignIn;
        public bool IsSignIn
        {
            get => isSignIn;
            set
            {
                isSignIn = value;
                NotifyPropertyChanged(nameof(IsSignIn));
            }
        }

        /// <summary>
        /// Indicates sign up mode
        /// </summary>
        private bool isSignUp;
        public bool IsSignUp
        {
            get => isSignUp;
            set
            {
                isSignUp = value;
                NotifyPropertyChanged(nameof(IsSignUp));
            }
        }

        /// <summary>
        /// A command to change mode to sign in mode
        /// </summary>
        public RelayCommand SignInCustomerCommand { get; set; }

        /// <summary>
        /// A command to change mode to sign up mode
        /// </summary>
        public RelayCommand SignUpCustomerCommand { get; set; }

        /// <summary>
        /// A command to sign in as manager
        /// </summary>
        public RelayCommand SignInManagerCommand { get; set; }

        /// <summary>
        /// Constractor
        /// Initialize mode and commands
        /// </summary>
        public WelcomeWindowViewModel()
        {
            SignInCustomerView signInCustomer = new();
            SignInCustomerCommand = new(SignInCustomer);
            SignUpCustomerCommand = new(SignUpCustomer);
            SignInManagerCommand = new(SignInManager);
            IsSignIn = false;
            IsSignUp = !IsSignIn;
        }

        /// <summary>
        /// Change mode to sign in mode
        /// </summary>
        public void SignInCustomer()
        {
            IsSignIn = true;
            IsSignUp = !IsSignIn;
        }

        /// <summary>
        /// Change mode to sign up mode
        /// </summary>
        public void SignUpCustomer()
        {
            IsSignIn = false;
            IsSignUp = !IsSignIn;
        }

        /// <summary>
        /// Sign in as manager
        /// </summary>
        public void SignInManager()
        {
            ManageWindows.OpenAppWindow();
            ManageWindows.CloseRegisterWindow();
        }
        #region InotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }
        #endregion
    }
}
