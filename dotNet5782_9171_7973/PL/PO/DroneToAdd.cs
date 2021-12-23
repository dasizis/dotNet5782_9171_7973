using System.ComponentModel.DataAnnotations;
using StringUtilities;

namespace PO
{
    class DroneToAdd : PropertyChangedNotification
    {
        int id;
        /// <summary>
        /// Drone Id
        /// </summary>
        [Required(ErrorMessage = "Required")]
        public int Id 
        {
            get => id;
            set => SetField(ref id, value);
        }
        
        string model;
        /// <summary>
        /// Drone model
        /// </summary>
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Model length must be between 3-10")]
        public string Model
        {
            get => model;
            set => SetField(ref model, value);
        }

        BO.WeightCategory maxWeight;
        /// <summary>
        /// Highest weight drone can carry
        /// </summary>
        [Required(ErrorMessage = "Required")]
        public BO.WeightCategory MaxWeight
        {
            get => maxWeight;
            set => SetField(ref maxWeight, value);
        }
        
        BO.DroneState state;
        /// <summary>
        /// Drone state
        /// </summary>
        [Required(ErrorMessage = "Required")]
        public BO.DroneState State
        {
            get => state;
            set => SetField(ref state, value);
        }

        public override string ToString() => this.ToStringProperties();
    }
}
