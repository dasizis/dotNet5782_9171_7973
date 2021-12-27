using System;
using StringUtilities;

namespace PO
{
    /// <summary>
    /// A class to represent a PDS of customer in delivery
    /// (customer related to parcel delivery, sender or reciever)
    /// </summary>
    public class CustomerInDelivery
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
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <c>ToString()</c> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
