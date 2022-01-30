using PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL.Views
{
    /// <summary>
    /// Interaction logic for Map.xaml
    /// </summary>
    public partial class Map : UserControl
    {
        public List<object>  Markers
        {
            get { return (List<object>)GetValue(MarkersProperty); }
            set { SetValue(MarkersProperty, value); }
        }

        public static readonly DependencyProperty MarkersProperty =
            DependencyProperty.Register("Markers", typeof(List<object>), typeof(Map), new PropertyMetadata(new List<object>()));


        public Map()
        {
            InitializeComponent();
            ShapeFileLayer.Uri = $@"{System.IO.Directory.GetCurrentDirectory()}/../../../Assest/world.shp";

            DataContext = this;
        }
    }
}
