using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PO
{
    class BaseStationToAdd : PropertyChangedNotification
    {
        int? id;
        /// <summary>
        /// Base Station Id
        /// </summary>
        [Required(ErrorMessage = "Required")]
        public int? Id
        {
            get => id;
            set => SetField(ref id, value);
        }

        string name;
        /// <summary>
        /// Base station name
        /// </summary>       
        [Required(ErrorMessage = "Required")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Name length must be between 3-10")]
        public string Name
        {
            get => name;
            set => SetField(ref name, value);
        }

        Location location;
        /// <summary>
        /// Base station location
        /// </summary>       
        [Required(ErrorMessage = "Required")]
        public Location Location
        {
            get => location;
            set => SetField(ref location, value);
        }

        int? chargeSlots;
        /// <summary>
        /// Base Station Id
        /// </summary>
        [Required(ErrorMessage = "Required")]
        [Range(0, int.MaxValue, ErrorMessage = "Charge slots number should be greater than zero")]
        public int? ChargeSlots
        {
            get => chargeSlots;
            set => SetField(ref chargeSlots, value);
        }
    }
}
