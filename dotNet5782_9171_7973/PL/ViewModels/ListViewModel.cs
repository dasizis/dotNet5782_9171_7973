using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    abstract class ListViewModel<T>
    {
        public ObservableCollection<T> list { get; set; } = new();
        public RelayCommand<T> OpenItemCommand { get; set; }
        public ListViewModel(Predicate<T> predicate)
        {
           foreach(var item in GetList().Where(item => predicate(item)).ToList())
           {
                list.Add(item);
           }
           OpenItemCommand = new((e) => ExecuteOpen(e), null);
        }

        protected abstract IEnumerable<T> GetList();
        protected abstract void ExecuteOpen(T item);
    }
}
