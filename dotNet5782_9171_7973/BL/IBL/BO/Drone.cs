﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringUtilities;

namespace IBL.BO
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
                    throw new ArgumentException(value);
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
                    throw new ArgumentException(value.ToString());
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
                    throw new ArgumentException(value.ToString());
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
                    throw new ArgumentException(value.ToString());
                }
                state = value;
            }
        }
        public ParcelInDeliver ParcelInDeliver { get; set; }
        private Location location;
        public Location Location
        {
            get => location;
            set
            {
                if (!Validation.IsValidLatitude(value.Latitude)
                    || !Validation.IsValidLatitude(value.Longitude))
                {
                    throw new ArgumentException(value.ToString());
                }
                location = value;
            }
        }
        public override string ToString() => this.ToStringProps();


    }
}
