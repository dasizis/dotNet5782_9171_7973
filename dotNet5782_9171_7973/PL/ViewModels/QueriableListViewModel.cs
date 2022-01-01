using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Data;

namespace PL.ViewModels
{
    abstract class QueriableListViewModel<T>
    {
        private const string RESET_VALUE = "None";
        public ObservableCollection<T> List { get; set; }

        public ICollectionView View { get; set; }

        public object FilterKey { get; set; }

        public object FilterValue { get; set; }

        public IEnumerable Options { get; set; }

        public string SortKey { get; set; }

        public string GroupKey { get; set; }

        public RelayCommand SortCommand { get; set; }
        public RelayCommand FilterCommand { get; set; }
        public RelayCommand GroupCommand { get; set; }
        public RelayCommand OpenAddWindowCommand { get; set; }

        public QueriableListViewModel()
        {
            List = new(GetList());
            View = (CollectionView)CollectionViewSource.GetDefaultView(List);

            Options = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                               .Where(property => property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                               .Cast<object>()
                               .Union(new List<object> { RESET_VALUE });

            View.Filter = Filter;

            FilterCommand = new(View.Refresh, () => FilterKey != null);
            GroupCommand = new(Group, () => GroupKey != null);
            SortCommand = new(Sort);
            OpenAddWindowCommand = new(() => Workspace.AddPanelCommand.Execute(GetAddPanel()));
        }

        private bool Filter(object item)
        {
            if (FilterValue == null)
                return true;

            PropertyInfo property = typeof(T).GetProperty(FilterKey.ToString());

            if (property.PropertyType == typeof(string))
                return property.GetValue(item).ToString().Contains((string)FilterValue);

            return true;
        }

        protected abstract IEnumerable<T> GetList();
        protected abstract Panel GetAddPanel();

        void Sort()
        {
            View.SortDescriptions.Clear();

            if (GroupKey == RESET_VALUE) return;

            View.SortDescriptions.Add(new SortDescription() { PropertyName = SortKey, Direction = ListSortDirection.Ascending });
            View.Refresh();
        }

        void Group()
        {
            View.GroupDescriptions.Clear();

            if (GroupKey == RESET_VALUE) return;

            View.GroupDescriptions.Add(new PropertyGroupDescription(GroupKey));
            View.Refresh();
        }
    }
}
