using PL.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.ViewModels
{
    class WelcomeWindowViewModel : INotifyPropertyChanged
    {
        
        private bool isSignIn;
        public bool IsSignIn
        {
            get => isSignIn;
            set
            {
                isSignIn = value;
                NotifyPropertyChanged("IsSignIn");
            }
        }

        private bool isSignUp;
        public bool IsSignUp
        {
            get => isSignUp;
            set
            {
                isSignUp = value;
                NotifyPropertyChanged("IsSignUp");
            }
        }

        public RelayCommand SignInCustomerCommand { get; set; }
        public RelayCommand SignUpCustomerCommand { get; set; }
        public RelayCommand SignInManagerCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public WelcomeWindowViewModel()
        {
            SignInCustomerView signInCustomer = new();
            SignInCustomerCommand = new(SignInCustomer);
            SignUpCustomerCommand = new(SignUpCustomer);
            SignInManagerCommand = new(SignInManager);
            IsSignIn = false;
            IsSignUp = !IsSignIn;
        }

        public void SignInCustomer()
        {
            IsSignIn = true;
            IsSignUp = !IsSignIn;
        }

        public void SignUpCustomer()
        {
            IsSignIn = false;
            IsSignUp = !IsSignIn;
        }

        public void SignInManager()
        {
            new Views.WorkspaceWindow().Show();
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
