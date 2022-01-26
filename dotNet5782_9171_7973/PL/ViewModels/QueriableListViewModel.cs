using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
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

        public IEnumerable FilterOptions { get; set; }
        public IEnumerable GroupOptions { get; set; }
        public IEnumerable SortOptions { get; set; }

        public Array EnumOptions { get; set; }

        public object SortKey { get; set; }

        public object GroupKey { get; set; }

        public RelayCommand SortCommand { get; set; }

        public RelayCommand<object> FilterCommand { get; set; }

        public RelayCommand GroupCommand { get; set; }

        public RelayCommand OpenAddWindowCommand { get; set; }

        public QueriableListViewModel()
        {
            List = new(GetList());
            View = (CollectionView)CollectionViewSource.GetDefaultView(List);

            FilterOptions = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                                     .Where(property => property.PropertyType.IsValueType || property.PropertyType == typeof(string));
                                     
            SortOptions  =  FilterOptions.Cast<PropertyInfo>().Select(option => option.Name).Union(new List<string> { RESET_VALUE });
            GroupOptions = FilterOptions.Cast<PropertyInfo>().Where(property => property.PropertyType.IsEnum).Select(option => option.Name).Union(new List<string> { RESET_VALUE });

            View.Filter = Filter;

            FilterCommand = new(ActivateFilter, param => FilterKey != null);
            GroupCommand = new(Group, () => GroupKey != null);
            SortCommand = new(Sort);
            OpenAddWindowCommand = new(() => Workspace.AddPanelCommand.Execute(GetAddPanel()));
        }

        private void ActivateFilter(object parameter)
        {
            FilterValue = parameter;
            View.Refresh();
        }

        private bool Filter(object item)
        {
            if (FilterValue == null || FilterKey  == null || (FilterKey is string && (string)FilterKey == RESET_VALUE))
                return true;

            PropertyInfo property = FilterKey as PropertyInfo;
            var propertyValue = property.GetValue(item);

            if (propertyValue == null)
                return false;

            if (property.PropertyType == typeof(string))
                return propertyValue.ToString().ToUpper().Contains(FilterValue.ToString().ToUpper());

            if (property.PropertyType.IsEnum)
                return (int)FilterValue == (int)propertyValue;

            if (property.PropertyType == typeof(int) ||
                property.PropertyType == typeof(int?))
                return (int)propertyValue >= ((double[])FilterValue)[0] && (int)propertyValue <= ((double[])FilterValue)[1];

            if (property.PropertyType == typeof(double) ||
                property.PropertyType == typeof(double?))
                return (double)propertyValue >= ((double[])FilterValue)[0] && (double)propertyValue <= ((double[])FilterValue)[1];

            return true;
        }

        protected abstract IEnumerable<T> GetList();
        protected abstract Panel GetAddPanel();

        protected void ReloadList()
        {
            List.Clear();

            foreach (var item in GetList())
            {
                List.Add(item);
            }
        }

        void Sort()
        {
            View.SortDescriptions.Clear();

            if ((string)SortKey == RESET_VALUE) return;

            View.SortDescriptions.Add(new SortDescription() { PropertyName = (string)SortKey, Direction = ListSortDirection.Ascending });
            View.Refresh();
        }

        void Group()
        {
            View.GroupDescriptions.Clear();

            if ((string)GroupKey == RESET_VALUE) return;

            View.GroupDescriptions.Add(new PropertyGroupDescription((string)GroupKey));
            View.Refresh();
        }
    }
}
