using StringUtilities;

namespace PO
{
    /// <summary>
    /// A class to represent a PDS of drone for list
    /// </summary>
    public class DroneForList
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
        /// Highest weight drone can carry
        /// </summary>
        public WeightCategory MaxWeight { get; set; }

        /// <summary>
        /// Drone battery 
        /// (in parcents)
        /// </summary>
        public double Battery { get; set; }

        /// <summary>
        /// Drone state
        /// </summary>
        public DroneState State { get; set; }

        /// <summary>
        /// Drone location
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Id of drone's related parcel
        /// (if exists)
        /// </summary>
        public int? DeliveredParcelId { get; set; }
        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <code>ToString()</code> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
