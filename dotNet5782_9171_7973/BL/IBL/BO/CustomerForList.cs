using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringUtilities;

namespace IBL.BO
{
    public class CustomerForList 
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
                    throw new ArgumentException();
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
        public int ParcelsSendAndSupplied { get; set; }
        public int ParcelsSendAndNotSupplied { get; set; }
        public int ParcelsRecieved { get; set; }
        public int ParcelsOnWay { get; set; }
        public override string ToString() => this.ToStringProps();

    }
}
