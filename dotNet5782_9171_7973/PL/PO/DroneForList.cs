using StringUtilities;

namespace PO
{
    /// <summary>
    /// A class to represent a PDS of drone for list
    /// </summary>
    public class DroneForList : PropertyChangedNotification, IIdentifiable, ILocalable
    {
        int id;
        /// <summary>
        /// Drone Id
        /// </summary>
        public int Id
        {
            get => id;
            set => SetField(ref id, value);
        }

        string model;
        /// <summary>
        /// Drone model
        /// </summary>
        public string Model
        {
            get => model;
            set => SetField(ref model, value);
        }

        WeightCategory maxWeight;
        /// <summary>
        /// Highest weight drone can carry
        /// </summary>
        public WeightCategory MaxWeight
        {
            get => maxWeight;
            set => SetField(ref maxWeight, value);
        }

        double battery;
        /// <summary>
        /// Drone battery 
        /// (in parcents)
        /// </summary>
        public double Battery
        {
            get => battery;
            set => SetField(ref battery, value);
        }

        DroneState state;
        /// <summary>
        /// Drone state
        /// </summary>
        public DroneState State
        {
            get => state;
            set => SetField(ref state, value);
        }

        Location location;
        /// <summary>
        /// Drone location
        /// </summary>
        public Location Location
        {
            get => location;
            set => SetField(ref location, value);
        }

        int? deliveredParcelId;
        /// <summary>
        /// Id of drone's related parcel
        /// (if exists)
        /// </summary>
        public int? DeliveredParcelId
        {
            get => deliveredParcelId;
            set => SetField(ref deliveredParcelId, value);
        }

        bool isAutoMode;
        /// <summary>
        /// Indicates wheather the drone is in Auto mode
        /// </summary>
        public bool IsAutoMode
        {
            get => isAutoMode;
            set => SetField(ref isAutoMode, value);
        }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <c>ToString()</c> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
