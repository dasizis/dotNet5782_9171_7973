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
        Requested,
        Scheduled,
        PickedUp, 
        Supplied
    }
    public enum DroneState
    {
        Free,
        Maintenance,
        Deliver,
    }
}