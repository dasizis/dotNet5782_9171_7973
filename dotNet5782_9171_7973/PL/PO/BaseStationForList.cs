using StringUtilities;

namespace PO
{
    /// <summary>
    /// A class to represent a PDS of base station for list
    /// </summary>
    public class BaseStationForList
    {
        /// <summary>
        /// Base station Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Base station name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Number of empty charge slots at base station 
        /// </summary>
        public int EmptyChargeSlots { get; set; }

        /// <summary>
        /// Number of busy charge slots at base station 
        /// </summary>
        public int BusyChargeSlots { get; set; }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <code>ToString()</code> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}

