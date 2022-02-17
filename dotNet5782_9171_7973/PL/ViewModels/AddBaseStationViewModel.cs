using PO;
using System;

namespace PL.ViewModels
{
    /// <summary>
    /// A class to represent a view model of Add base station view
    /// </summary>
    class AddBaseStationViewModel : AddItemViewModel<BaseStationToAdd>
    {
        /// <summary>
        /// the base station view model's model
        /// </summary>
        public BaseStationToAdd BaseStation => Model;

        /// <summary>
        /// Add the base station
        /// </summary>
        protected override void Add()
        {
            PLService.AddBaseStation(BaseStation);
            Workspace.RemovePanelCommand.Execute(Workspace.BaseStationPanelName());
        }
    }
}
