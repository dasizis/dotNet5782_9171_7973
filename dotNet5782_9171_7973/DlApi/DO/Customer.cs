using System;
using System.Collections.Generic;
using System.Text;
using StringUtilities;

namespace DO
{
    /// <summary>
    /// A struct to represent a PDS of customer
    /// </summary>
    public struct Customer : IIdentifiable, IDeletable
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
        /// Customer location longitude
        /// </summary>
        [SexadecimalLongitude]
        public double Longitude { get; set; }

        /// <summary>
        /// Customer location latitude
        /// </summary>
        [SexadecimalLatitude]
        public double Latitude { get; set; }

        /// <summary>
        /// Determines whether the customer is deleted or not
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <code>ToString()</code> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
