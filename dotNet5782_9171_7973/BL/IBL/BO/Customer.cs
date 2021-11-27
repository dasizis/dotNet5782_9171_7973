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
        public string Name { get; set; }
        string phone;
        public string Phone 
        { 
            get => phone
            set
            {
                if (!Validation.IsValidPhone(value))
                {
                    thorw new ArgumentException();
                }
                phone = value;
            }
        }
        public Location Location { get; set; }
        public List<Parcel> Send { get; set; }
        public List<Parcel> Recieve { get; set; }
        public override string ToString() => this.ToStringProps();

    }
}
