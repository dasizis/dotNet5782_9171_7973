//using System.Collections.Generic;
//using System.Runtime.CompilerServices;

//namespace PL
//{
//    static class DronesNotification
//    {
//        static Dictionary<int, DronesChangedHandler> handlers = new();

//        static DronesChangedHandler dronesListChanged;

//        public static void AddHandler(DronesChangedHandler handler, int? id = null)
//        {
//            if (id == null)
//            {
//                dronesListChanged += handler;
//            }
//            else if (handlers.ContainsKey((int)id))
//            {
//                handlers[(int)id] += handler;
//            }
//            else
//            {
//                handlers.Add((int)id, handler);
//            }
//        }

//        public static void RemoveHandler(int id)
//        {
//            if (handlers.ContainsKey(id))
//            {
//                handlers[id] = null;
//            }
//        }

//        /// <summary>
//        /// Delegate type for <see cref="DronesChangedEvent"/>
//        /// </summary>
//        public delegate void DronesChangedHandler();

//        /// <summary>
//        /// Used to notify from outer class the one or more drones were modified
//        /// </summary>
//        /// <param name="callerMethodName">The caller method name</param>
//        public static void NotifyDroneChanged(int? id = null, [CallerMemberName] string callerMethodName = "")
//        {
//            if (id == null)
//            {
//                dronesListChanged?.Invoke();

//                foreach (var keyValuePair in handlers)
//                {
//                    keyValuePair.Value?.Invoke();
//                }
//            }
//            else if (handlers.ContainsKey((int)id))
//            {
//                handlers[(int)id]?.Invoke();
//            }
//        }
//    }
//}
