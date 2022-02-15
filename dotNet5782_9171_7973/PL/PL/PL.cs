using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    static partial class PLService
    {
        static public bool IsManangerMode { get; set; } = true;
        static public bool IsCustomerMode { get; set; } = !IsManangerMode;
        static public int? CustomerId { get; set; }
    }
}
