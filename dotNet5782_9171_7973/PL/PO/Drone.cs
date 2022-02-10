using StringUtilities;
using System.ComponentModel.DataAnnotations;

namespace PO
{
    /// <summary>
    /// A class to represent a PDS of drone
    /// </summary>
    public class Drone : PropertyChangedNotification, ILocalable
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
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[a-zA-Z0-9]{4,14}", ErrorMessage = "Model length must be between 4-14")]
        public string Model
        {
            get => model;
            set => SetField(ref model, value);
        }

        WeightCategory maxWeight;
        /// <summary>
        /// Category of max weight drone can carry
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

        ParcelInDeliver parcelInDeliver;
        /// <summary>
        /// Drone's related parcel
        /// (parcel drone delivers)
        /// </summary>
        public ParcelInDeliver ParcelInDeliver
        {
            get => parcelInDeliver;
            set => SetField(ref parcelInDeliver, value);
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

        bool isAutoMode;
        /// <summary>
        /// Indicates wheather the drone is in Auto mode
        /// </summary>
        public bool IsAutoMode 
        {
            get => isAutoMode;
            set => SetField(ref isAutoMode, value);
        }

        bool isNowStopping;
        /// <summary>
        /// Indicates wheather the drone is in now stopping the simulator 
        /// </summary>
        public bool IsNowStopping
        {
            get => isNowStopping;
            set => SetField(ref isNowStopping, value);
        }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <c>ToString()</c> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
