using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PO;

namespace PL.ViewModels
{
    class AddParcelViewModel : AddItemViewModel<ParcelToAdd>
    {
        public ParcelToAdd Parcel => Model;

        public Array PriorityOptions { get; } = Enum.GetValues(typeof(Priority));

        public Array WeightOptions { get; } = Enum.GetValues(typeof(WeightCategory));

        public List<int> CustomersOptions { get; set; }

        public AddParcelViewModel()
        {
            CustomersOptions = PL.GetCustomersList().Select(station => station.Id).ToList();
        }

        protected override void Add()
        {
            PL.AddParcel(Parcel);
        }
    }
}
