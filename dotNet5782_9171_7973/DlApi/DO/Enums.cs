namespace DO
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
}