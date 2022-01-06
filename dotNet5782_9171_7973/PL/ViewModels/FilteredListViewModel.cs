using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PL.ViewModels
{
    abstract class FilteredListViewModel<T> : INotifyPropertyChanged
    {
        public ObservableCollection<T> List { get; set; } = new();
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
            LoadList();

            View = (CollectionView)CollectionViewSource.GetDefaultView(List);
            View.Filter = Filter;
            OpenItemCommand = new((e) => ExecuteOpen(e));          
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected abstract IEnumerable<T> GetList();
        protected abstract void ExecuteOpen(T item);
        protected abstract void Close();
        protected void LoadList()
        {
            List.Clear();
            foreach (var item in GetList().Where(item => Predicate(item)).ToList())
            {
                List.Add(item);
            }
            if (List.Count() == 0) Close();
        }
        protected bool Filter(object item)
        {
            return item.ToString().Contains(FilterValue ?? "");
        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            View.Refresh();
        }
    }
}
