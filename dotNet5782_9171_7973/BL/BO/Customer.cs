using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringUtilities;


namespace BO
{
    public class Customer: ILocalable
    {
        public int Id { get; set; }
        string name;
        public string Name 
        { 
            get => name;
            set
            {
                if (!Validation.IsValidName(value))
                {
                    throw new ArgumentException(value);
                }
                name = value;
            }
        }
        string phone;
        public string Phone 
        {
            get => phone;
            set
            {
                if (!Validation.IsValidPhone(value))
                {
                    throw new ArgumentException(value);
                }
                phone = value;
            }
        }
        public Location Location { get; set; }
        public List<Parcel> Send { get; set; }
        public List<Parcel> Recieve { get; set; }
        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <code>ToString()</code> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();

    }
}
