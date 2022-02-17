using System;
using StringUtilities;

namespace PO
{
    /// <summary>
    /// A class to represent a PDS of customer in delivery
    /// (customer related to parcel delivery, sender or reciever)
    /// </summary>
    public class CustomerInDelivery : PropertyChangedNotification
    {
        int id;
        /// <summary>
        /// Customer Id
        /// </summary>
        public int Id
        {
            get => id;
            set => SetField(ref id, value);
        }

        string name;
        /// <summary>
        /// Customer name
        /// </summary>
        public string Name
        {
            get => name;
            set => SetField(ref name, value);
        }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <c>ToString()</c> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
