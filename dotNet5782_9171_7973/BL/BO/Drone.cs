using StringUtilities;

namespace BO
{
    /// <summary>
    /// A class to represent a PDS of drone
    /// </summary>
    public class Drone : ILocalable
    {
        /// <summary>
        /// Drone Id
        /// </summary>
        public int Id { get; set; }

        string model;
        /// <summary>
        /// Drone model
        /// </summary>
        public string Model
        {
            get => model;
            set
            {
                if (!Validation.IsValidModel(value))
                {
                    throw new InvalidPropertyValueException(value);
                }
                model = value;
            }
        }

        WeightCategory maxWeight;
        /// <summary>
        /// Category of max weight drone can carry
        /// </summary>
        public WeightCategory MaxWeight 
        {
            get => maxWeight;
            set
            {
                if(!Validation.IsValidEnumOption<WeightCategory>((int)value))
                {
                    throw new InvalidPropertyValueException(value);
                }
                maxWeight = value;
            }
        }

        double battery;
        /// <summary>
        /// Drone battery 
        /// (in parcents)
        /// </summary>
        public double Battery 
        {
            get => battery;
            set
            {
                if(!Validation.IsValidBattery(value)) 
                {
                    throw new InvalidPropertyValueException(value);
                }
                battery = value;
            }
        }

        DroneState state;
        /// <summary>
        /// Drone state
        /// </summary>
        public DroneState State
        {
            get => state;
            set
            {
                if (!Validation.IsValidEnumOption<DroneState>((int)value))
                {
                    throw new InvalidPropertyValueException(value);
                }
                state = value;
            }
        }

        /// <summary>
        /// Drone's related parcel
        /// (parcel drone delivers)
        /// </summary>
        public ParcelInDeliver ParcelInDeliver { get; set; }

        /// <summary>
        /// Drone location
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <c>ToString()</c> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
