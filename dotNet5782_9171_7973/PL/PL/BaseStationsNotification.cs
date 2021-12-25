using System.Runtime.CompilerServices;

namespace PL
{
    static class BaseStationsNotification
    {
        /// <summary>
        /// Delegate type for <see cref="BaseStationsChangedEvent"/>
        /// </summary>
        /// <param name="callerMethodName">The caller method name</param>
        public delegate void BaseStationsChangedHandler(string callerMethodName);

        /// <summary>
        /// Occurs when one or more drones were modified
        /// </summary>
        public static event BaseStationsChangedHandler BaseStationsChangedEvent;

        /// <summary>
        /// Used to notify from outer class the one or more drones were modified
        /// </summary>
        /// <param name="callerMethodName">The caller method name</param>
        public static void NotifyBaseStationChanged([CallerMemberName] string callerMethodName = "")
        {
            BaseStationsChangedEvent?.Invoke(callerMethodName);
        }
    }
}
