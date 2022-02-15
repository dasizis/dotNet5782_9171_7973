using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PL
{
    class PLNotification
    {
        protected Dictionary<int, Action> handlers = new();

        protected event Action<int> globalHandleres;

        /// <summary>
        /// Adds a new handler
        /// </summary>
        /// <param name="handler">The handler to add</param>
        /// <param name="id">The item id</param>
        public void AddHandler(Action handler, int id)
        {
            if (handlers.ContainsKey(id))
            {
                handlers[id] += handler;
            }
            else
            {
                handlers.Add(id, handler);
            }
        }

        public void AddGlobalHandler(Action<int> handler)
        {
            globalHandleres += handler;
        }

        /// <summary>
        /// Removes an handler of the item 
        /// </summary>
        /// <param name="id">The item id</param>
        public void RemoveHandler(int id)
        {
            if (handlers.ContainsKey(id))
            {
                handlers[id] = null;
            }
        }

        /// <summary>
        /// Used to notify from an outer class the one or more drones were modified
        /// </summary>
        /// <param name="id">The item id which was changed</param>
        /// <param name="callerMethodName">The caller method name</param>
        public void NotifyItemChanged(int id, [CallerMemberName] string callerMethodName = "")
        {
            globalHandleres?.Invoke(id);

            if (handlers.ContainsKey(id))
            {
                handlers[id]?.Invoke();
            }
        }

        public static PLNotification BaseStationNotification { get; } = new();

        public static PLNotification CustomerNotification { get; } = new();

        public static PLNotification DroneNotification { get; } = new();

        public static PLNotification ParcelNotification { get; } = new();

    }
}
