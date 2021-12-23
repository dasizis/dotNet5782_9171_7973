using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL
{
    class DisplayListViewModel<T>
    {
        public CollectionView DataList { get; set; }

        public string SortKey { get; set; }

        public IEnumerable<string> SortOptions { get; set; }

        private string filterKey;
        /// <summary>
        /// Sets or gets the key to filter by
        /// </summary>
        public string FilterKey 
        {
            get => filterKey;
            set
            {
                filterKey = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Setes or gets the value of the key to filter by
        /// </summary>
        public IEnumerable<string> FilterOptions { get; set; }

        public object FilterValue { get; set; }

        public DisplayListViewModel()
        {
            SortOptions = from property in typeof(T).GetProperties()
                          where property.PropertyType.IsValueType
                          select property.Name;

            FilterOptions = from property in typeof(T).GetProperties()
                            where property.PropertyType.IsValueType
                            select property.Name;

            DataList.Filter = Filter;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged()
        {
            DataList.SortDescriptions.Prepend(new SortDescription() { PropertyName = SortKey });
            DataList.Refresh();
        }

        private bool Filter(object value)
        {
            PropertyInfo property = typeof(T).GetProperty(FilterKey);
            Type propertyType = property.PropertyType;
            var propertyValue = property.GetValue(value);

            if (propertyType == typeof(string))
                return ((string)propertyValue).Contains((string)FilterValue);

            if (propertyType.IsEnum)
                return !Enum.IsDefined(propertyType, FilterValue) || propertyValue == FilterValue;

            return propertyValue == FilterValue;
        }
    }
}
