using Dragablz;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<BO.DroneForList> Drones { get; set; } 
        public static UserControl TabToAdd { get; set; } = null;
        public static string TabHeader { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            TabControl.NewItemFactory = () => new TabItem() 
            { 
                Header = TabHeader ?? throw new InvalidOperationException("No tab to add."), Content = TabToAdd 
            };
        }
    }
}
