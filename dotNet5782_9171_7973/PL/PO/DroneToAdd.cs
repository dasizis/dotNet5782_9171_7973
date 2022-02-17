using StringUtilities;
using System.ComponentModel.DataAnnotations;

namespace PO
{
    class DroneToAdd : PropertyChangedNotification
    {
        int? id;
        /// <summary>
        /// Drone Id
        /// </summary>
        [Required(ErrorMessage = "Required")]
        public int? Id 
        {
            get => id;
            set => SetField(ref id, value);
        }
        
        string model;
        /// <summary>
        /// Drone model
        /// </summary>       
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"\w{4,14}", ErrorMessage = "Model length must be between 4-14")]
        public string Model
        {
            get => model;
            set => SetField(ref model, value);
        }

        WeightCategory? maxWeight;
        /// <summary>
        /// Highest weight drone can carry
        /// </summary>
        [Required(ErrorMessage = "Required")]
        [EnumDataType(typeof(WeightCategory), ErrorMessage = "Max weight should be of type WeightCategory")]
        public WeightCategory? MaxWeight
        {
            get => maxWeight;
            set => SetField(ref maxWeight, value);
        }
        
        int? stationId;
        /// <summary>
        /// The base station id for first charging
        /// </summary>
        [Required(ErrorMessage = "Required")]
        public int? StationId
        {
            get => stationId;
            set => SetField(ref stationId, value);
        }

        public override string ToString() => this.ToStringProperties();
    }
}