using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PO;

namespace PL.ViewModels
{
    class AddBaseStationViewModel : AddItemViewModel<BaseStationToAdd>
    {
        public BaseStationToAdd BaseStation => Model;

        protected override void Add()
        {
            PLService.AddBaseStation(BaseStation);
        }
    }
}
