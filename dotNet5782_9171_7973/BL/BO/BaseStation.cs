using StringUtilities;
using System.Collections.Generic;

namespace BO
{
    /// <summary>
    /// A class to represent a PDS of base station
    /// </summary>
    public class BaseStation: ILocalable
    {
        /// <summary>
        /// Base station Id
        /// </summary>
        public int Id { get; set; }
        
        string name;
        /// <summary>
        /// Base station name
        /// </summary>
        public string Name 
        { 
            get => name;
            set
            {
                if (!Validation.IsValidName(value))
                {
                    throw new InvalidPropertyValueException(value);
                }
                name = value;
            }
        }

        /// <summary>
        /// Base station location
        /// </summary>
        public Location Location { get; set; }

        int emptyChargeSlots;
        /// <summary>
        /// Number of empty charge slots in base station
        /// </summary>
        public int EmptyChargeSlots 
        { 
            get => emptyChargeSlots;
            set
            {
                if (value < 0)
                {
                    throw new InvalidPropertyValueException(value);
                }
                emptyChargeSlots = value;
            } 
        }

        /// <summary>
        /// List of drones beeing in charged at base station
        /// </summary>
        public List<Drone> DronesInCharge { get; set; }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <c>ToString()</c> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties(); 
    }
}
