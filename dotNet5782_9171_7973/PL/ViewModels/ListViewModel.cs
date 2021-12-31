using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.ViewModels
{
    abstract class ListViewModel<T>
    {
        public ObservableCollection<T> list { get; set; } = new();
        public Predicate<T> Predicate { get; set; }
        public RelayCommand<T> OpenItemCommand { get; set; }

        public ListViewModel(Predicate<T> predicate)
        {
            Predicate = predicate;
            LoadList();
            OpenItemCommand = new((e) => ExecuteOpen(e));
        }

        protected abstract IEnumerable<T> GetList();
        protected abstract void ExecuteOpen(T item);
        protected abstract void Close();
        protected void LoadList()
        {
            list.Clear();
            foreach (var item in GetList().Where(item => Predicate(item)).ToList())
            {
                list.Add(item);
            }
            if (list.Count() == 0) Close();
        }
    }
}
