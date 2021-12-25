using System.Runtime.CompilerServices;

namespace PL
{
    static class ParcelsNotification
    {
        /// <summary>
        /// Delegate type for <see cref="ParcelsChangedEvent"/>
        /// </summary>
        /// <param name="callerMethodName">The caller method name</param>
        public delegate void ParcelsChangedHandler(string callerMethodName);

        /// <summary>
        /// Occurs when one or more customers were modified
        /// </summary>
        public static event ParcelsChangedHandler ParcelsChangedEvent;

        /// <summary>
        /// Used to notify from outer class the one or more customers were modified
        /// </summary>
        /// <param name="callerMethodName">The caller method name</param>
        public static void NotifyParcelChanged([CallerMemberName] string callerMethodName = "")
        {
            ParcelsChangedEvent?.Invoke(callerMethodName);
        }
    }
}
