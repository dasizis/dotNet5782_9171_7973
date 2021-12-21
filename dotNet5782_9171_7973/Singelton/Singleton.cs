using System;
using System.Reflection;

namespace Singleton
{
    /// <summary>
    /// A generic class that can make any class a singleton by inheritance.
    /// If you have a class Person for example, you can make it singleton by the following steps:
    /// <list type="number">
    ///     <listheader></listheader>
    ///     <item>Make its constructor private</item>
    ///     <item>Add a static constructor</item>
    ///     <item>Make the Person class sealed</item>
    ///     <item>
    ///         Inherit from Singleton in the following form 
    ///         <c>public sealed class Person: Singleton&lt;Person&gt;</c>
    ///     </item>
    /// </list>
    /// </summary>
    /// <example>
    /// </example>
    /// <typeparam name="T">The class to be singleton</typeparam>
    public abstract class Singleton<T> where T : Singleton<T>
    {
        static Singleton() { }
        protected Singleton() { }

        class Nested
        {
            internal static volatile T instatnce = null;
            internal static readonly object locker = new object();
        }
        public static T Instance
        {
            get
            {
                if (Nested.instatnce == null)
                {
                    lock (Nested.locker)
                    {
                        if (Nested.instatnce == null)
                        {
                            Type type = typeof(T);

                            if (type == null || !type.IsSealed)
                                throw new SingletonException($"{type.Name} must be a seald class");

                            ConstructorInfo ctor = null;
                            try
                            {
                                ctor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                                                           null, Type.EmptyTypes, null);
                            }
                            catch (ArgumentException exception)
                            {
                                throw new SingletonException($"A private/protected constructor is missing for {type.Name}", exception);
                            }

                            if (ctor == null || ctor.IsAssembly)
                                throw new SingletonException($"A private/protected constructor is missing for {type.Name}");

                            Nested.instatnce = (T)ctor.Invoke(null);
                        }
                    }
                }
                return Nested.instatnce;
            }
        }
    }
}
