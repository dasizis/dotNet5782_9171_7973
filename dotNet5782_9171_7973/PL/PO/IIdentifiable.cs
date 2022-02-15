using System;
using System.Collections.Generic;
using System.Text;

namespace PO
{
    /// <summary>
    /// Describes the ability to be recognized by Id number
    /// </summary>
    public interface IIdentifiable
    {
        public int Id { get; }
    }
}
