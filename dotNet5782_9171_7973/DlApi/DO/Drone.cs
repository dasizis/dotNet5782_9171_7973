using StringUtilities;

namespace DO
{
    /// <summary>
    /// A struct to represent a PDS of drone
    /// </summary>
    public struct Drone : IIdentifiable, IDeletable
    {
        /// <summary>
        /// Drone Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Drone model
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Category of max weight the drone can carry
        /// </summary>
        public WeightCategory MaxWeight { get; set; }

        /// <summary>
        /// Determines whether the drone is deleted or not
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <c>ToString()</c> method
        /// </summary>
        /// <returns>String representation of drone</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
