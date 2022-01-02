using StringUtilities;

namespace PO
{
    /// <summary>
    /// A class to represent a PDS of customer for list
    /// </summary>
    public class CustomerForList : PropertyChangedNotification
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

        string phone;
        /// <summary>
        /// Customer phone number
        /// </summary>
        public string Phone
        {
            get => phone;
            set => SetField(ref phone, value);
        }

        int parcelsSendAndSupplied;
        /// <summary>
        /// Number of parcels sent by customer and supplied
        /// </summary>
        public int ParcelsSendAndSupplied
        {
            get => parcelsSendAndSupplied;
            set => SetField(ref parcelsSendAndSupplied, value);
        }

        int parcelsSendAndNotSupplied;
        /// <summary>
        /// Number of parcels sent by customer and were not supplied
        /// </summary>
        public int ParcelsSendAndNotSupplied
        {
            get => parcelsSendAndNotSupplied;
            set => SetField(ref parcelsSendAndNotSupplied, value);
        }

        int parcelsRecieved;
        /// <summary>
        /// Number of parcels customer recieved
        /// </summary>
        public int ParcelsRecieved
        {
            get => parcelsRecieved;
            set => SetField(ref parcelsRecieved, value);
        }

        int parcelsOnWay;
        /// <summary>
        /// Number of parcels on the way to customer
        /// </summary>
        public int ParcelsOnWay
        {
            get => ParcelsOnWay;
            set => SetField(ref parcelsOnWay, value);
        }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <c>ToString()</c> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
