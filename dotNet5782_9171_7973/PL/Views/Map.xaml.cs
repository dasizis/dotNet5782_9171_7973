using PO;
using System;
using System.Collections.Generic;
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


        public ILocalable Item
        {
            get { return (ILocalable)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("Item", typeof(ILocalable), typeof(Map), new PropertyMetadata(null));



        public Map()
        {
            InitializeComponent();
            ShapeFileLayer.Uri = $@"{System.IO.Directory.GetCurrentDirectory()}/../../../Assest/world.shp";

            ShapeFileLayer.DataContext = this;
        }
    }
}
