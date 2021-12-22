using StringUtilities;

namespace BO
{
    /// <summary>
    /// A class to represent a PDS of drone in charge
    /// </summary>
    public class DroneInCharge
    {
        /// <summary>
        /// Drone Id
        /// </summary>
        public int Id { get; set; }

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
                if (!Validation.IsValidBattery(value))
                {
                    throw new InvalidPropertyValueException(nameof(Battery), value);
                }
                battery = value;
            }
        }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <code>ToString()</code> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
