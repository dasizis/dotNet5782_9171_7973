using System.ComponentModel.DataAnnotations;

namespace PO
{
    //public void AddParcel(int senderId, int targetId, WeightCategory weight, Priority priority)
    class ParcelToAdd : PropertyChangedNotification
    {
        int? senderId;
        /// <summary>
        /// Parcel sender Id
        /// </summary>
        [Required(ErrorMessage = "Required")]
        public int? SenderId
        {
            get => senderId;
            set => SetField(ref senderId, value);
        }

        int? targetId;
        /// <summary>
        /// Parcel target Id
        /// </summary>
        [Required(ErrorMessage = "Required")]
        public int? TargetId
        {
            get => targetId;
            set => SetField(ref targetId, value);
        }

        WeightCategory? weight;
        /// <summary>
        /// Parcel weight
        /// </summary>
        [Required(ErrorMessage = "Required")]
        [EnumDataType(typeof(WeightCategory), ErrorMessage = "Weight should be of type WeightCategory")]
        public WeightCategory? Weight
        {
            get => weight;
            set => SetField(ref weight, value);
        }

        Priority? priority;
        /// <summary>
        /// Parcel priority
        /// </summary>
        [Required(ErrorMessage = "Required")]
        [EnumDataType(typeof(Priority), ErrorMessage = "Priority should be of type Priority")]
        public Priority? Priority
        {
            get => priority;
            set => SetField(ref priority, value);
        }

    }
}
