using StringUtilities;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PL.Views
{
    /// <summary>
    /// Interaction logic for WorkspaceView.xaml
    /// </summary>
    public partial class WorkspaceView : UserControl
    {
        /// <summary>
        /// A command to add a new Panel to workspace
        /// </summary>
        static public RelayCommand<ViewModels.Panel> AddPanelCommand { get; set; }

        /// <summary>
        /// A command to remove a Panel from workspace
        /// </summary>
        static public RelayCommand<string> RemovePanelCommand { get; set; }

        /// <summary>
        /// The add panel logic
        /// </summary>
        /// <param name="panel">The panel to add</param>
        private void AddPanel(ViewModels.Panel panel)
        {
            var item = GetPanel(panel.Header);

            if (item != null)
            {
                Dock.SelectItem(item);
                return;
            }

            DockingManager.SetHeader(panel.View, panel.Header);

            if (panel.PanelType == ViewModels.PanelType.List)
            {
                DockingManager.SetState(panel.View, DockState.Dock);
                DockingManager.SetSideInDockedMode(panel.View, DockSide.Tabbed);
                DockingManager.SetTargetNameInDockedMode(panel.View, "ListArea");
            }
            else
            {
                DockingManager.SetState(panel.View, DockState.Document);
            }

            Dock.Children.Add(panel.View);
        }

        /// <summary>
        /// Removes a panel from the workspace
        /// </summary>
        /// <param name="header">The panel header</param>
        private void Remove(string header)
        {
            var panel = Dock.Children.Cast<ContentControl>()
                                     .SingleOrDefault(panel => (string)DockingManager.GetHeader(panel) == header);

            if (panel == null) return;

            Dock.Children.Remove(panel);
        }


        public WorkspaceView()
        {
            InitializeComponent();

            AddPanelCommand = new(AddPanel);
            RemovePanelCommand = new(Remove);
        }

        /// <summary>
        /// Removes a panel from the memory when it is behing  closed by the "X" icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dock_CloseButtonClick(object sender, CloseButtonEventArgs e)
        {
            Remove((string)DockingManager.GetHeader(e.TargetItem));
        }

        /// <summary>
        /// Hepler method to get a panel by its header
        /// </summary>
        /// <param name="header">The panel header</param>
        /// <returns>The panel object</returns>
        FrameworkElement GetPanel(string header)
        {
            foreach (FrameworkElement item in Dock.Children)
            {
                if ((string)DockingManager.GetHeader(item) == header)
                    return item;
            }

            return null;
        }
    }
}
