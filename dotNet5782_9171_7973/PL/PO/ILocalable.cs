using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    /// <summary>
    /// Describes the ability to be located
    /// </summary>
    public interface ILocalable
    {
        public Location Location { get; set; }
    }
}