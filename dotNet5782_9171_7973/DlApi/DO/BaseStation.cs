using StringUtilities;

namespace DO
{
    /// <summary>
    /// A Class to represent a PDS of base station
    /// </summary>
    public struct BaseStation : IIdentifiable, IDeletable
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
        /// Base station location longitude
        /// </summary>
        [SexadecimalLongitude]
        public double Longitude { get; set; }

        /// <summary>
        /// Base station location latitude
        /// </summary>
        [SexadecimalLatitude]
        public double Latitude { get; set; }

        /// <summary>
        /// Number of charge slots in base station
        /// (The number of drones that can be charged simultaneously)
        /// </summary>
        public int ChargeSlots { get; set; }

        /// <summary>
        /// Determines whether the base station is deleted or not
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <code>ToString()</code> method
        /// </summary>
        /// <returns></returns>
        public override string ToString() => this.ToStringProperties();
    }
}
