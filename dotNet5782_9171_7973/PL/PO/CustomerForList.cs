using StringUtilities;

namespace PO
{
    /// <summary>
    /// A class to represent a PDS of customer for list
    /// </summary>
    public class CustomerForList
    {
        /// <summary>
        /// Customer Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Customer name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Customer phone number
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Number of parcels sent by customer and supplied
        /// </summary>
        public int ParcelsSendAndSupplied { get; set; }

        /// <summary>
        /// Number of parcels sent by customer and were not supplied
        /// </summary>
        public int ParcelsSendAndNotSupplied { get; set; }

        /// <summary>
        /// Number of parcels customer recieved
        /// </summary>
        public int ParcelsRecieved { get; set; }

        /// <summary>
        /// Number of parcels on the way to customer
        /// </summary>
        public int ParcelsOnWay { get; set; }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <code>ToString()</code> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
