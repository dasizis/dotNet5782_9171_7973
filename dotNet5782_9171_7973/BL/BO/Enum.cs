namespace BO
{
    public enum WeightCategory
    {
        Light,
        Medium,
        Heavy,
    }
    public enum Priority
    {
        Regular,
        Fast,
        Urgent,
    }
    public enum ParcelState
    {
        Defined,
        Associated,
        PickedUp, 
        Provided
    }
    public enum DroneState
    {
        Free,
        Maintenance,
        Deliver,
    }
}