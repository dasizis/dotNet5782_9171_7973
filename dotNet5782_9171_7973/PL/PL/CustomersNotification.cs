namespace PL
{
    static class CustomersNotification
    {
        /// <summary>
        /// Delegate type for <see cref="CustomersChangedHandler"/>
        /// </summary>
        public delegate void CustomersChangedHandler();

        /// <summary>
        /// Occurs when one or more customers were modified
        /// </summary>
        public static event CustomersChangedHandler CustomersChanged;

        /// <summary>
        /// Used to notify from outer class the one or more customers were modified
        /// </summary>
        public static void NotifyDroneChanged()
        {
            CustomersChanged?.Invoke();
        }
    }
}
