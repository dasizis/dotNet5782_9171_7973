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
    abstract class QueriableListViewModel<T> where T : PO.IIdentifiable
    {
        private const string RESET_VALUE = "None";
        /// <summary>
        /// The list to present
        /// </summary>
        public ObservableCollection<T> List { get; set; }

        /// <summary>
        /// The collection that is actually seen 
        /// (filtering is done on this list)
        /// </summary>
        public ICollectionView View { get; set; }

        /// <summary>
        /// The key, or property by which to filter
        /// </summary>
        public object FilterKey { get; set; }

        /// <summary>
        /// The value to filter for
        /// </summary>
        public object FilterValue { get; set; }

        /// <summary>
        /// List of properties options to filter by
        /// </summary>
        public IEnumerable FilterOptions { get; set; }

        /// <summary>
        /// List of properties options to group by
        /// </summary>
        public IEnumerable GroupOptions { get; set; }

        /// <summary>
        /// List of properties options to sort by
        /// </summary>
        public IEnumerable SortOptions { get; set; }

        /// <summary>
        /// Enum options to present for a key value
        /// </summary>
        public Array EnumOptions { get; set; }

        /// <summary>
        /// The key, or property by which to sort
        /// </summary>
        public object SortKey { get; set; }

        /// <summary>
        /// The key, or property by which to group
        /// </summary>
        public object GroupKey { get; set; }

        /// <summary>
        /// Sort command
        /// </summary>
        public RelayCommand SortCommand { get; set; }

        /// <summary>
        /// Filter command
        /// </summary>
        public RelayCommand<object> FilterCommand { get; set; }

        /// <summary>
        /// Group command
        /// </summary>
        public RelayCommand GroupCommand { get; set; }

        /// <summary>
        /// Open add window command
        /// </summary>
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

        /// <summary>
        /// Activate filter by a parameter
        /// </summary>
        /// <param name="parameter">the parameter to filter by</param>
        private void ActivateFilter(object parameter)
        {
            FilterValue = parameter;
            View.Refresh();
        }

        /// <summary>
        /// Filter list
        /// </summary>
        /// <param name="item">item to check if suits the filtering</param>
        /// <returns>boolean result which indicates wheather the item suits the filtering</returns>
        private bool Filter(object item)
        {
            if (FilterValue == null || FilterKey  == null || (FilterKey is string filterString && filterString == RESET_VALUE))
                return true;

            PropertyInfo property = FilterKey as PropertyInfo;
            var propertyValue = property.GetValue(item);

            if (propertyValue == null)
            {
                return false;
            }

            if (property.PropertyType == typeof(string))
            {
                return propertyValue.ToString().ToUpper().Contains(FilterValue.ToString().ToUpper());
            }

            if (property.PropertyType.IsEnum)
            {
                return (int)FilterValue == (int)propertyValue;
            }

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

        protected abstract T GetItem(int id);

        /// <summary>
        /// Reload list
        /// </summary>
        /// <param name="id">id of the item to reload</param>
        protected void ReloadList(int id)
        {
            var item = List.FirstOrDefault(item => item.Id == id);

            if (item != null)
            {
                List.Remove(item);
            }

            try
            {
                List.Add(GetItem(id));
            }
            catch (BO.ObjectNotFoundException) { }
        }

        /// <summary>
        /// Sort list
        /// </summary>
        void Sort()
        {
            View.SortDescriptions.Clear();

            if ((string)SortKey == RESET_VALUE) return;

            View.SortDescriptions.Add(new SortDescription() { PropertyName = (string)SortKey, Direction = ListSortDirection.Ascending });
            View.Refresh();
        }

        /// <summary>
        /// Group list
        /// </summary>
        void Group()
        {
            View.GroupDescriptions.Clear();

            if ((string)GroupKey == RESET_VALUE) return;

            View.GroupDescriptions.Add(new PropertyGroupDescription((string)GroupKey));
            View.Refresh();
        }
    }
}
