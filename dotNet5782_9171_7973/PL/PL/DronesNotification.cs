namespace PL
{
    static class DronesNotification
    {
        /// <summary>
        /// Delegate type for <see cref="DronesChangedEvent"/>
        /// </summary>
        public delegate void DronesChangedHandler();

        /// <summary>
        /// Occurs when one or more drones were modified
        /// </summary>
        public static event DronesChangedHandler DronesChangedEvent;

        /// <summary>
        /// Used to notify from outer class the one or more drones were modified
        /// </summary>
        public static void NotifyDroneChanged()
        {
            DronesChangedEvent?.Invoke();
        }
    }
}
