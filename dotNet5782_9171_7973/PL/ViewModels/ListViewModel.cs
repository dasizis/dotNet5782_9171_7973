using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    class ListViewModel<T>
    {
        public ObservableCollection<T> list { get; set; }

        public ListViewModel(Predicate<T> predicate)
        {
           foreach(var item in GetList().Where(item => predicate(item)))
            {
                list.Add(item);
            }
            
        }

        protected abstract IEnumerable<T> GetList();
    }
}
