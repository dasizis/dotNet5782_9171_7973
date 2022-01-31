using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PL
{
    static class PLNotification
    {
        private static Dictionary<Type, Dictionary<int, ItemChangedHandler>> handlers = new();

        private static Dictionary<Type, ItemChangedHandler> listHandlers = new();

        /// <summary>
        /// Adds a handler for spesific type (and id)   
        /// </summary>
        /// <typeparam name="T">The <see cref="PO"/> Type of the item (use <see cref="PO.Drone"/> and not <see cref="PO.DroneForList"/> for example</typeparam>
        /// <param name="handler">The handler function</param>
        /// <param name="id">The id of the related item</param>
        public static void AddHandler<T>(ItemChangedHandler handler, int? id = null)
        {
            if (id == null)
            {
                AddToDictionary(listHandlers, typeof(T), handler);
            }
            else if (handlers.ContainsKey(typeof(T)))
            {
                AddToDictionary(handlers[typeof(T)], (int)id, handler);
            }
            else
            {
                handlers.Add(typeof(T), new());
                AddToDictionary(handlers[typeof(T)], (int)id, handler);
            }
        }

        /// <summary>
        /// Helper method which add new value to dictionary, if the key exists push the item to the key's list 
        /// otherwise adds a new key
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="handler"></param>
        private static void AddToDictionary<TKey>(Dictionary<TKey, ItemChangedHandler> dictionary, TKey key, ItemChangedHandler handler)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] += handler;
            }
            else
            {
                dictionary.Add(key, handler);
            }
        }

        /// <summary>
        /// Removes an handler of a item 
        /// </summary>
        /// <typeparam name="T">The item type</typeparam>
        /// <param name="id">The item id</param>
        public static void RemoveHandlers<T>(int id)
        {
            if (handlers.ContainsKey(typeof(T)) && handlers[typeof(T)].ContainsKey(id))
            {
                handlers[typeof(T)][id] = null;
            }
        }

        public delegate void ItemChangedHandler();

        /// <summary>
        /// Used to notify from outer class the one or more drones were modified
        /// </summary>
        /// <param name="callerMethodName">The caller method name</param>
        public static void NotifyItemChanged<T>(int? id = null, [CallerMemberName] string callerMethodName = "")
        {
            if (listHandlers.ContainsKey(typeof(T)))
            {
                listHandlers[typeof(T)]?.Invoke();
            }

            if (id == null)
            {
                InvokeAll<T>();
            }
            else if (handlers.ContainsKey(typeof(T)) && handlers[typeof(T)].ContainsKey((int)id))
            {

                handlers[typeof(T)][(int)id]?.Invoke();
            }
        }

        /// <summary>
        /// Invokes all handlers of type
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        private static void InvokeAll<T>()
        {
            if (handlers.ContainsKey(typeof(T)))
            {
                foreach (var handler in handlers[typeof(T)].Values)
                {
                    handler?.Invoke();
                }
            }
        }
    }
}