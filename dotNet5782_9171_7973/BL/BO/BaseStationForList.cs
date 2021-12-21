using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringUtilities;

namespace BO
{
    public class BaseStationForList
    {
        public int Id { get; set; }
        string name;
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
