using StringUtilities;

namespace PO
{
    /// <summary>
    /// A class to represent a PDS of base station for list
    /// </summary>
    public class BaseStationForList : PropertyChangedNotification, IIdentifiable
    {

        int id;
        /// <summary>
        /// Base station Id
        /// </summary>
        public int Id
        {
            get => id;
            set => SetField(ref id, value);
        }

        string name;
        /// <summary>
        /// Base station name
        /// </summary>
        public string Name
        {
            get => name;
            set => SetField(ref name, value);
        }

        int emptyChargeSlots;
        /// <summary>
        /// Number of empty charge slots in base station
        /// </summary>
        public int EmptyChargeSlots
        {
            get => emptyChargeSlots;
            set => SetField(ref emptyChargeSlots, value);
        }

        int busyChargeSlots;
        /// <summary>
        /// Number of busy charge slots at base station 
        /// </summary>
        public int BusyChargeSlots 
        {
            get => busyChargeSlots;
            set => SetField(ref busyChargeSlots, value);
        }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <c>ToString()</c> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}

