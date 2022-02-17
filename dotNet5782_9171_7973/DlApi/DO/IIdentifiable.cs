using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    /// <summary>
    /// Describes the ability to be recognized by Id number
    /// </summary>
    public interface IIdentifiable
    {
        public int Id { get; }
    }
}
