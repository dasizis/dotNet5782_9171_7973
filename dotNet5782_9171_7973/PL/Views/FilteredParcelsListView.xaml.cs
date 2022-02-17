using System;
using System.Windows.Controls;

namespace PL.Views
{
    /// <summary>
    /// Interaction logic for ParcelListView.xaml
    /// </summary>
    public partial class FilteredParcelsListView : UserControl
    {
        public FilteredParcelsListView(Predicate<PO.ParcelForList> p)
        {
            InitializeComponent();
            DataContext = new ViewModels.FilteredParcelsListViewModel(p);
        }
    }
}
