using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    throw new ArgumentException(value);
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
                    throw new ArgumentException(value.ToString());
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
                    throw new ArgumentException(value.ToString());
                }
                busyChargeSlots = value;
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
