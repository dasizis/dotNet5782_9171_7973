using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringUtilities;


namespace IBL.BO
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
        private Location location;
        public Location Location
        {
            get => location;
            set
            {
                if(!Validation.IsValidLatitude(value.Latitude)
                    || !Validation.IsValidLatitude(value.Longitude))
                {
                    throw new ArgumentException(value.ToString());
                }
                location = value;
            }
        }
        public List<Parcel> Send { get; set; }
        public List<Parcel> Recieve { get; set; }
        public override string ToString() => this.ToStringProps();

    }
}
