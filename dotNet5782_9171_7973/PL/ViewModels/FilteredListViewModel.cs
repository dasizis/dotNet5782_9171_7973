using StringUtilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace PL.ViewModels
{
    abstract class FilteredListViewModel<T> : INotifyPropertyChanged where T: PO.IIdentifiable
    {
        /// <summary>
        /// The list
        /// </summary>
        public ObservableCollection<T> List { get; set; }

        /// <summary>
        /// The filtered list 
        /// </summary>
        public ICollectionView View { get; set; }

        /// <summary>
        /// Predicate to filter by
        /// </summary>
        public Predicate<T> Predicate { get; set; }

        /// <summary>
        /// A command to open an item from the list
        /// </summary>
        public RelayCommand<T> OpenItemCommand { get; set; }

        /// <summary>
        /// Filter value
        /// </summary>
        private string filterValue = null;
        public string FilterValue 
        { 
            get => filterValue;
            set
            {
                filterValue = value;
                NotifyPropertyChanged(FilterValue);
            } 
        }

        public FilteredListViewModel(Predicate<T> predicate)
        {
            Predicate = predicate;
            List = new(GetList().Where(item => Predicate(item)));

            View = (CollectionView)CollectionViewSource.GetDefaultView(List);
            View.Filter = Filter;
            OpenItemCommand = new((e) => ExecuteOpen(e));          
        }

        /// <summary>
        /// Get list of type T
        /// </summary>
        /// <returns>list of items</returns>
        protected abstract IEnumerable<T> GetList();

        /// <summary>
        /// Get specific item item 
        /// </summary>
        /// <param name="id">id of requested item</param>
        /// <returns>item with the id</returns>
        protected abstract T GetItem(int id);

        /// <summary>
        /// Open item from the list
        /// </summary>
        /// <param name="item">item to open</param>
        protected abstract void ExecuteOpen(T item);

        /// <summary>
        /// Close the list panel
        /// </summary>
        protected abstract void Close();

        /// <summary>
        /// Load list
        /// </summary>
        /// <param name="id">id of changed item to reload</param>
        protected void LoadList(int id)
        {
            var item = List.FirstOrDefault(item => item.Id == id);

            if (item != null)
            {
                List.Remove(item);
            }

            try
            {
                T newItem = GetItem(id);

                if (Predicate(newItem))
                {
                    List.Add(newItem);
                }
            }
            catch (BO.ObjectNotFoundException) { }
        }

        /// <summary>
        /// Filter list by filter value
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected bool Filter(object item)
        {
           if (FilterValue == null) return true;
           return item.ToStringProperties().ToUpper().Contains(FilterValue.ToUpper());
        }

        #region InotifyPropertyChanged mambers
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            View.Refresh();
        }
        #endregion
    }
}
