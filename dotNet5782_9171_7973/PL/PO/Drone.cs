﻿using StringUtilities;

namespace PO
{
    /// <summary>
    /// A class to represent a PDS of drone
    /// </summary>
    class Drone : PropertyChangedNotification
    {
        int id;
        /// <summary>
        /// Drone Id
        /// </summary>
        public int Id
        {
            get => id;
            set => SetField(ref id, value);
        }

        string model;
        /// <summary>
        /// Drone model
        /// </summary>
        public string Model
        {
            get => model;
            set => SetField(ref model, value);
        }

        WeightCategory maxWeight;
        /// <summary>
        /// Category of max weight drone can carry
        /// </summary>
        public WeightCategory MaxWeight
        {
            get => maxWeight;
            set => SetField(ref maxWeight, value);
        }

        double battery;
        /// <summary>
        /// Drone battery 
        /// (in parcents)
        /// </summary>
        public double Battery
        {
            get => battery;
            set => SetField(ref battery, value);
        }

        DroneState state;

        /// <summary>
        /// Drone state
        /// </summary>
        public DroneState State
        {
            get => state;
            set => SetField(ref state, value);
        }

        ParcelInDeliver parcelInDeliver;
        /// <summary>
        /// Drone's related parcel
        /// (parcel drone delivers)
        /// </summary>
        public ParcelInDeliver ParcelInDeliver
        {
            get => parcelInDeliver;
            set => SetField(ref parcelInDeliver, value);
        }

        Location location;
        /// <summary>
        /// Drone location
        /// </summary>
        public Location Location 
        { 
            get => location;
            set => SetField(ref location, value);
        }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <code>ToString()</code> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}