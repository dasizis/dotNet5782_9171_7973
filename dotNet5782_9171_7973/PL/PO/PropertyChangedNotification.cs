using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PO
{
    /// <summary>
    /// A base class for models which implements <see cref="INotifyPropertyChanged"/>
    /// and <see cref="IDataErrorInfo"/> interfaces
    /// </summary>
    internal abstract class PropertyChangedNotification : INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        /// Event which Occurs when a property was changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when a property was changed
        /// </summary>
        /// <param name="propertyName">The property name</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets a filed
        /// </summary>
        /// <typeparam name="T">The field value type</typeparam>
        /// <param name="field">The field ref</param>
        /// <param name="value">The new value to set</param>
        /// <param name="propertyName">The field property name</param>
        /// <returns>true if the value is was set to the field; otherwise false</returns>
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public string Error
        {
            get
            {
                foreach (var prop in GetType().GetProperties())
                {
                    if (prop.GetCustomAttributes(typeof(ValidationAttribute), false).Length == 0) continue;

                    if (this[prop.Name] != null)
                        return "One or more of the properties are not correct";
                }

                return null;
            }
        }

        /// <summary>
        /// Returns the property validation result
        /// </summary>
        /// <param name="columnName">The property name</param>
        /// <returns>An error message if the property is invalid; otherwise <see langword="null"/></returns>
        public string this[string columnName]
        {
            get
            {
                var validationResults = new List<ValidationResult>();
                var value = GetType().GetProperty(columnName).GetValue(this);
                var validationContext = new ValidationContext(this) { MemberName = columnName };

                if (Validator.TryValidateProperty(value, validationContext, validationResults))
                    return null;

                return validationResults.First().ErrorMessage;
            }
        }
    }
}
