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
    /// <summary>
    /// An abstarct class to represent a view model for add an entity view model
    /// </summary>
    /// <typeparam name="T">Type of the entity to add</typeparam>
    abstract class AddItemViewModel<T> where T : PropertyChangedNotification, new()
    {
        public T Model { get; set; } = new();

        public RelayCommand<object> AddCommand { get; set; }

        public AddItemViewModel()
        {
            AddCommand = new RelayCommand<object>(ExecuteAdd, param => Model.Error == null);
        }

        private void ExecuteAdd(object target)
        {
            try
            {
                Add();

                string name = typeof(T).Name.Replace("ToAdd", "");
                MessageBox.Show(MessageBox.BoxType.Success, $"{name} added successfully");
                Workspace.RemovePanelCommand.Execute($"Add {name}");
            }
            catch (BO.IdAlreadyExistsException e)
            {
                MessageBox.Show(MessageBox.BoxType.Error, e.Message);
            }
        }

        protected abstract void Add();
    }
}
