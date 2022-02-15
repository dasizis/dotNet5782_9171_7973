﻿using PO;
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
            SignUpCommand = new(SignUp);
        }
        void SignUp()
        {
            try
            {
                PLService.AddCustomer(Customer);
                //Get In with proper customer 
                //temp
                new Views.WorkspaceWindow((int)Customer.Id).Show();
            }
            catch(BO.IdAlreadyExistsException)
            {
                MessageBox.Show("Your Password is used by another user.\n Try a different one.");
            }
            
        }
    }
}
