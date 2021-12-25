using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using PO;

namespace PL.ViewModels
{
    abstract class AddItemViewModel<T> where T : PropertyChangedNotification, new()
    {
        public T Model { get; set; } = new();

        public RelayCommand<object> AddCommand { get; set; }

        public AddItemViewModel()
        {
            AddCommand = new RelayCommand<object>(ExecuteAdd, param => Model.Error == null);
        }

        private void ExecuteAdd(object param)
        {
            try
            {
                Add();
                UserControl dialog = new Alert() { Text = "Drone added successfully" , IsSuccess = true };
                DialogHost.OpenDialogCommand.Execute(dialog, null);
                // Close Tab
            }
            catch (BO.IdAlreadyExistsException e)
            {
                UserControl dialog = new Alert() { Text = e.Message, IsSuccess = false };
                DialogHost.OpenDialogCommand.Execute(dialog, null);
            }
        }

        protected abstract void Add();
    }
}
