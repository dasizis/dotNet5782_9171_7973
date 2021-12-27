using StringUtilities;

namespace BO
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

        int emptyChargeSlots;
        /// <summary>
        /// Number of empty charge slots at base station 
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
        
        private int busyChargeSlots;
        /// <summary>
        /// Number of busy charge slots at base station 
        /// </summary>
        public int BusyChargeSlots
        {
            get => busyChargeSlots;
            set
            {
                if (value < 0)
                {
                    throw new InvalidPropertyValueException(value);
                }
                busyChargeSlots = value;
            }
        }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <c>ToString()</c> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
