using StringUtilities;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace PL.Views
{
    /// <summary>
    /// Interaction logic for WorkspaceWindow.xaml
    /// </summary>
    public partial class WorkspaceWindow : Window
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
            if (panel.PanelType == ViewModels.PanelType.List)
            {
                DockingManager.SetState(panel.View, DockState.Dock);
                DockingManager.SetSideInDockedMode(panel.View, DockSide.Tabbed);
                DockingManager.SetTargetNameInDockedMode(panel.View, ViewModels.Workspace.TargerNameOfListPanelType);
            }
            else if (panel.PanelType == ViewModels.PanelType.Display || panel.PanelType == ViewModels.PanelType.Add)
            {
                DockingManager.SetState(panel.View, DockState.Document);
            }
            else if (panel.PanelType == ViewModels.PanelType.Other)
            {
                DockingManager.SetState(panel.View, DockState.Dock);
                DockingManager.SetSideInDockedMode(panel.View, DockSide.Bottom);
                DockingManager.SetDesiredHeightInDockedMode(panel.View, 280);
            }

            AddGeneralPanel(panel);
        }

        private void AddGeneralPanel(ViewModels.Panel panel)
        {
            var item = GetPanel(panel.Header);

            if (item != null)
            {
                Dock.SelectItem(item);
                return;
            }

            DockingManager.SetHeader(panel.View, panel.Header);
            DockingManager.SetCanClose(panel.View, panel.CanClose);

            Dock.Children.Add(panel.View);
            Dock.SelectItem(panel.View);
        }

        private void AddBaseListPanel(ViewModels.Panel listPanel)
        {
            DockingManager.SetSideInDockedMode(listPanel.View, DockSide.Left);
            listPanel.View.Name = ViewModels.Workspace.TargerNameOfListPanelType;
            DockingManager.SetState(listPanel.View, DockState.Dock);
            DockingManager.SetDesiredWidthInDockedMode(listPanel.View, 300);

            AddGeneralPanel(listPanel);
        }

        /// <summary>
        /// Removes a panel from the workspace
        /// </summary>
        /// <param name="header">The panel header</param>
        private void Remove(string header)
        {
            var panel = Dock.Children.Cast<ContentControl>().SingleOrDefault(panel => (string)DockingManager.GetHeader(panel) == header);

            if (panel == null) return;

            Dock.Children.Remove(panel);
        }

        private void Dock_DockStateChanged(FrameworkElement sender, DockStateEventArgs e)
        {
            if (e.NewState == DockState.Hidden)
                Remove((string)DockingManager.GetHeader(sender));
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

        private void Init(params ViewModels.Panel[] panels)
        {
            InitializeComponent();

            AddPanelCommand = new(AddPanel);
            RemovePanelCommand = new(Remove);

            Dock.DockStateChanged += Dock_DockStateChanged;

            bool baseListAdded = false;

            foreach (var panel in panels)
            {
                if (panel.PanelType == ViewModels.PanelType.List && !baseListAdded)
                {
                    AddBaseListPanel(panel);
                    baseListAdded = true;
                }
                AddPanel(panel);
            }

        }

        static WorkspaceWindow()
        { }

        public WorkspaceWindow()
        {
            Init(
                ViewModels.Workspace.MainMapPanel,
                ViewModels.Workspace.CustomersListPanel,
                ViewModels.Workspace.BaseStationsListPanel,
                ViewModels.Workspace.ParcelsListPanel,
                ViewModels.Workspace.DronesListPanel
            );
            PLService.IsManangerMode = true;
        }

        public WorkspaceWindow(params ViewModels.Panel[] panels)
        {
            Init(panels);
        }

        public WorkspaceWindow(int userId)
        {
            Init(
                ViewModels.Workspace.CustomerPanel(userId, false),
                ViewModels.Workspace.FilteredParcelsListPanel(p => p.SenderId == userId,
                                                              ViewModels.Workspace.CustomerSentListName(userId), false),
                ViewModels.Workspace.FilteredParcelsListPanel(p => p.TargetId == userId,
                                                              ViewModels.Workspace.CustomerRecievedListName(userId), false)
            );
            PLService.IsManangerMode = false;
            PLService.IsCustomerMode = !PLService.IsManangerMode;
            PLService.CustomerId = userId;
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            if (ManageWindows.CanCloseAppWindow())
            {
                ManageWindows.OpenRegisterWindow();
            }
            ManageWindows.CloseAppWindow();
        }

        private void SendNewParcel(object sender, RoutedEventArgs e)
        {
            AddPanel(new ViewModels.Panel(
                ViewModels.PanelType.Add,
                new Views.AddParcelView(),
                ViewModels.Workspace.ParcelPanelName()
                ));
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ManageWindows.CanCloseAppWindow())
            {
                e.Cancel = true;
            }
        }
    }
}
