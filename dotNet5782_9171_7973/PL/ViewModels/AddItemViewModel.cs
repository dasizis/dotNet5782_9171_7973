using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

                string name = typeof(T).Name.Replace("ToAdd", "");
                MessageBox.Show($"{name} added successfully");
                Workspace.RemovePanelCommand.Execute($"Add {name}");
            }
            catch (BO.IdAlreadyExistsException e)
            {
                MessageBox.Show(e.Message);

            }
        }

        protected abstract void Add();
    }
}
