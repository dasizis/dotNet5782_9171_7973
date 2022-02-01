using System;
using System.Runtime.CompilerServices;

namespace PL
{
    static class DroneNotification
    {
        /// <summary>
        /// Occurs when one or more values were modified
        /// </summary>
        public static event Action ValueChanged;

        /// <summary>
        /// Used to notify from outer class the one or more values were modified
        /// </summary>
        /// <param name="callerMethodName">The caller method name</param>
        public static void NotifyValueChanged([CallerMemberName] string callerMethodName = "")
        {
            ValueChanged?.Invoke();
        }
    }
}
