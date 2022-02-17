namespace ConsoleUI_BL
{
    enum MainOption
    {
        Add,
        Update,
        Display,
        DisplayList,
        Exit,
    }

    enum AddOption
    {
        BaseStation,
        Customer,
        Parcel,
        Drone,
    }

    enum UpdateOption
    {
        RenameDrone,
        UpdateBaseStation,
        UpdateCustomer,
        AssignParcelToDrone,
        CollectParcel,
        SupplyParcel,
        ChargeDroneAtBaseStation,
        FinishCharging,
    }

    enum DisplayOption
    {
        BaseStation,
        Customer,
        Parcel,
        Drone,
    }

    enum DisplayListOption
    {
        BaseStation,
        Customer,
        Parcel,
        Drone,
        NotAssignedToDroneParcels,
        AvailableBaseStations,
    }

}
