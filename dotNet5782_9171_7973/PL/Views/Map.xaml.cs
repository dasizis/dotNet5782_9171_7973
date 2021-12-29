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


        public List<Parcel> Parcels
        {
            get { return (List<Parcel>)GetValue(ParcelsProperty); }
            set { SetValue(ParcelsProperty, value); }
        }

        public static readonly DependencyProperty ParcelsProperty =
            DependencyProperty.Register("Parcels", typeof(List<Parcel>), typeof(Map), new PropertyMetadata(new List<Parcel>()));


        public List<BaseStation> BaseStations
        {
            get { return (List<BaseStation>)GetValue(BaseStationsProperty); }
            set { SetValue(BaseStationsProperty, value); }
        }

        public static readonly DependencyProperty BaseStationsProperty =
            DependencyProperty.Register("BaseStations", typeof(List<BaseStation>), typeof(Map), new PropertyMetadata(new List<BaseStation>()));


        public List<Customer> Customers
        {
            get { return (List<Customer>)GetValue(CustomersProperty); }
            set { SetValue(CustomersProperty, value); }
        }

        public static readonly DependencyProperty CustomersProperty =
            DependencyProperty.Register("Customers", typeof(List<Customer>), typeof(Map), new PropertyMetadata(new List<Customer>()));


        public List<Drone> Drones
        {
            get { return (List<Drone>)GetValue(DronesProperty); }
            set { SetValue(DronesProperty, value); }
        }

        public static readonly DependencyProperty DronesProperty =
            DependencyProperty.Register("Drones", typeof(List<Drone>), typeof(Map), new PropertyMetadata(new List<Drone>()));


        public Dictionary<Type, Color> ColorsDictionary { get; set; } = new()
        {
            [typeof(Drone)] = Colors.Pink,
            [typeof(Customer)] = Colors.LightBlue,
            [typeof(Parcel)] = Colors.Lavender,
            [typeof(BaseStation)] = Colors.Lavender,
        };

        public List<object> DataList => BaseStations.Cast<ILocalable>()
                                                    .Concat(Customers)
                                                    .Concat(Drones)
                                                    .Select(item => new { item.Location.Longitude, 
                                                                          item.Location.Latitude, 
                                                                          Color = ColorsDictionary[item.GetType()].ToString() , 
                                                                          Name = item.GetType().Name })
                                                    .Cast<object>()
                                                    .ToList();
        public Map()
        {
            InitializeComponent();
            ShapeFileLayer.Uri = $@"{System.IO.Directory.GetCurrentDirectory()}/../../../Assest/world.shp";

            DataContext = this;

        }
    }
}
