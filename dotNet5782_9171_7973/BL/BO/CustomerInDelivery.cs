﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringUtilities;

namespace BO
{
    /// <summary>
    /// A struct to represent a PDS of customer in delivery
    /// (customer related to parcel delivery, sender or reciever)
    /// </summary>
    public class CustomerInDelivery 
    {
        /// <summary>
        /// Customer Id
        /// </summary>
        public int Id { get; set; }
        string name;
        /// <summary>
        /// Customer name
        /// </summary>
        public string Name 
        { 
            get => name;
            set
            {
                if (!Validation.IsValidName(value))
                {
                    throw new ArgumentException();
                }
                name = value;
            }
        }
        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <code>ToString()</code> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();

    }
}
