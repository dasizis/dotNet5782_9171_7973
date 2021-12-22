namespace BO
{
    /// <summary>
    /// Parcel weight category
    /// Or a weight category that a drone can carry at most
    /// </summary>
    public enum WeightCategory
    {
        Light,
        Medium,
        Heavy,
    }
    /// <summary>
    /// A priority of parcel
    /// </summary>
    public enum Priority
    {
        Regular,
        Fast,
        Urgent,
    }

    /// <summary>
    /// A state of parcel
    /// </summary>
    public enum ParcelState
    {
        Defined,
        Associated,
        PickedUp, 
        Provided
    }

    /// <summary>
    /// A state of drone
    /// </summary>
    public enum DroneState
    {
        Free,
        Maintenance,
        Deliver,
    }
}