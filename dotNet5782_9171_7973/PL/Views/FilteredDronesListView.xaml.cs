using System;
using System.Windows.Controls;

namespace PL.Views
{
    /// <summary>
    /// Interaction logic for FilteredDronesListView.xaml
    /// </summary>
    public partial class FilteredDronesListView : UserControl
    {
        public FilteredDronesListView(Predicate<PO.DroneForList> p)
        {
            InitializeComponent();
            DataContext = new ViewModels.FilteredDronesListViewModel(p);
        }
    }
}
