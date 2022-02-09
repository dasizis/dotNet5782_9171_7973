using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PL
{
    class PLNotification
    {
        protected Dictionary<int, Action> handlers = new();

        protected event Action globalHandleres;

        /// <summary>
        /// Adds a new handler
        /// </summary>
        /// <param name="handler">The handler to add</param>
        /// <param name="id">The item id</param>
        public void AddHandler(Action handler, int? id = null)
        {
            if (id == null)
            {
                globalHandleres += handler;
            }
            else if (handlers.ContainsKey((int)id))
            {
                handlers[(int)id] += handler;
            }
            else
            {
                handlers.Add((int)id, handler);
            }
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
        public void NotifyItemChanged(int? id = null, [CallerMemberName] string callerMethodName = "")
        {
            globalHandleres?.Invoke();

            if (id == null)
            {
                foreach (var (handlerId, handler) in handlers)
                {
                    handler?.Invoke();
                }
            }
            else if (handlers.ContainsKey((int)id))
            {
                handlers[(int)id]?.Invoke();
            }
        }

        public static PLNotification BaseStationNotification { get; } = new();

        public static PLNotification CustomerNotification { get; } = new();

        public static PLNotification DroneNotification { get; } = new();

        public static PLNotification ParcelNotification { get; } = new();

    }
}
