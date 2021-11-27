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
        int parcelsSendAndSupplied;
        public int ParcelsSendAndSupplied
        {
            get => parcelsSendAndSupplied;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(value.ToString());
                }
                parcelsSendAndSupplied = value;
            }
        }
        int parcelsSendAndNotSupplied;
        public int ParcelsSendAndNotSupplied
        {
            get => parcelsSendAndNotSupplied;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(value.ToString());
                }
                parcelsSendAndNotSupplied = value;
            }
        }
        int parcelsRecieved;
        public int ParcelsRecieved
        {
            get => parcelsRecieved;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(value.ToString());
                }
                parcelsRecieved = value;
            }
        }
        int parcelsOnWay;
        public int ParcelsOnWay
        {
            get => parcelsOnWay;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(value.ToString());
                }
                parcelsOnWay = value;
            }
        }
        public override string ToString() => this.ToStringProps();

    }
}
