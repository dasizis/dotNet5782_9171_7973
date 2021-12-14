using System;
using StringUtilities;

namespace BO
{
    public class ParcelForList 
    {
        public int Id { get; set; }
        string senderName;
        public string SenderName
        {
            get => senderName;
            set
            {
                if (!Validation.IsValidName(value))
                {
                    throw new ArgumentException();
                }
                senderName = value;
            }
        }
        string targetName;
        public string TargetName
        {
            get => targetName;
            set
            {
                if (!Validation.IsValidName(value))
                {
                    throw new ArgumentException();
                }
                targetName = value;
            }
        }
        WeightCategory weight;
        public WeightCategory Weight
        {
            get => weight;
            set
            {
                if (!Validation.IsValidEnumOption<WeightCategory>((int)value))
                {
                    throw new ArgumentException(value.ToString());
                }
                weight = value;
            }
        }
        Priority priority;
        public Priority Priority
        {
            get => priority;
            set
            {
                if (!Validation.IsValidEnumOption<Priority>((int)value))
                {
                    throw new ArgumentException(value.ToString());
                }
                priority = value;
            }
        }
        public override string ToString() => this.ToStringProperties();

    }
}
