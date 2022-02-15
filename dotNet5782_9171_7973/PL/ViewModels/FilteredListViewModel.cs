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
        public ObservableCollection<T> List { get; set; }
        public ICollectionView View { get; set; }
        public Predicate<T> Predicate { get; set; }
        public RelayCommand<T> OpenItemCommand { get; set; }

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected abstract IEnumerable<T> GetList();
        protected abstract T GetItem(int id);
        protected abstract void ExecuteOpen(T item);
        protected abstract void Close();

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

        protected bool Filter(object item)
        {
           if (FilterValue == null) return true;
           return item.ToStringProperties().ToUpper().Contains(FilterValue.ToUpper());
        }
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            View.Refresh();
        }
    }
}
