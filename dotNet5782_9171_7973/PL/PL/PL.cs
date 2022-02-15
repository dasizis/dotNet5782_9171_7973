using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        static public bool IsCustomerMode { get; set; } = !IsManangerMode;

        /// <summary>
        /// Id of customer user
        /// (null if user is manager)
        /// </summary>
        static public int? CustomerId { get; set; }
    }
}
