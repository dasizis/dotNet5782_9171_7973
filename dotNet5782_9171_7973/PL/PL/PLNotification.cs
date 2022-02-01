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
        /// Add a new handler
        /// </summary>
        /// <param name="handler">The handler to add</param>
        /// <param name="id">The item id</param>
        public void AddHandler(Action handler, int? id = null)
        {
            if (id == null)
            {
                globalHandleres += handler;
            }
            if (handlers.ContainsKey((int)id))
            {
                handlers[(int)id] += handler;
            }
            else
            {
                handlers.Add((int)id, handler);
            }

        }

        /// <summary>
        /// Removes an handler of a item 
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
        /// Used to notify from outer class the one or more drones were modified
        /// </summary>
        /// <param name="id">The item id which was changed</param>
        /// <param name="callerMethodName">The caller method name</param>
        public void NotifyItemChanged(int? id = null, [CallerMemberName] string callerMethodName = "")
        {
            if (id == null)
            {
                globalHandleres?.Invoke();

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

        public static PLNotification BaseStationNotification => new();

        public static PLNotification CustomerNotification => new();

        public static PLNotification DroneNotification => new();

        public static PLNotification ParcelNotification => new();
    }
}