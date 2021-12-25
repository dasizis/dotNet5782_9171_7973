using System.Runtime.CompilerServices;

namespace PL
{
    static class DronesNotification
    {
        /// <summary>
        /// Delegate type for <see cref="DronesChangedEvent"/>
        /// </summary>
        /// <param name="callerMethodName">The caller method name</param>
        public delegate void DronesChangedHandler(string callerMethodName);

        /// <summary>
        /// Occurs when one or more drones were modified
        /// </summary>
        public static event DronesChangedHandler DronesChangedEvent;

        /// <summary>
        /// Used to notify from outer class the one or more drones were modified
        /// </summary>
        /// <param name="callerMethodName">The caller method name</param>
        public static void NotifyDroneChanged([CallerMemberName] string callerMethodName = "")
        {
            DronesChangedEvent?.Invoke(callerMethodName);
        }
    }
}
