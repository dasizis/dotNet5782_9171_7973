namespace PL
{
    static partial class PLService
    {
        /// <summary>
        /// Indicates wheather user is a manager or not
        /// </summary>
        static public bool IsManangerMode { get; set; } = true;

        /// <summary>
        /// Indicates wheather user is a customer or not
        /// </summary>
        static public bool IsCustomerMode
        {
            get => !IsManangerMode;
            set => IsManangerMode = !value;
        }

        /// <summary>
        /// Id of customer user
        /// (null if user is manager)
        /// </summary>
        static public int? CustomerId { get; set; }
    }
}
