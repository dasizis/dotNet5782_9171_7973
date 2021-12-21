using System;
using StringUtilities;

namespace BO
{
    public class Drone : ILocalable
    {
        public int Id { get; set; }

        string model;
        public string Model
        {
            get => model;
            set
            {
                if (!Validation.IsValidName(value))
                {
                    throw new InvalidPropertyValueException(nameof(Model), value);
                }
                model = value;
            }
        }

        WeightCategory maxWeight;
        public WeightCategory MaxWeight 
        {
            get => maxWeight;
            set
            {
                if(!Validation.IsValidEnumOption<WeightCategory>((int)value))
                {
                    throw new InvalidPropertyValueException(nameof(MaxWeight), value.ToString());
                }
                maxWeight = value;
            }
        }

        double battery;
        public double Battery 
        {
            get => battery;
            set
            {
                if(value < 0) 
                {
                    throw new InvalidPropertyValueException(nameof(Battery), value);
                }
                battery = value;
            }
        }

        DroneState state;
        public DroneState State
        {
            get => state;
            set
            {
                if (!Validation.IsValidEnumOption<DroneState>((int)value))
                {
                    throw new InvalidPropertyValueException(nameof(State), value.ToString());
                }
                state = value;
            }
        }
        public ParcelInDeliver ParcelInDeliver { get; set; }

        public Location Location { get; set; }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <code>ToString()</code> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();

    }
}
