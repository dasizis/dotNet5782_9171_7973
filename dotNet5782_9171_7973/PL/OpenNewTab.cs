using Dragablz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    static class OpenNewTab
    {
        static public void AddDroneDisplayTab(int id)
        {
            MainWindow.TabToAdd = new DroneDisplay(id);
            MainWindow.TabHeader = $"Drone #{id}";
            ExecuteCommand();
            MainWindow.TabToAdd = null;
            MainWindow.TabHeader = null;
        }

        static private void ExecuteCommand()
        {
            TabablzControl.AddItemCommand.Execute(null, null);
        }
    }
}
