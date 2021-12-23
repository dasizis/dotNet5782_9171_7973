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

namespace PL
{
    /// <summary>
    /// Interaction logic for Alert.xaml
    /// </summary>
    public partial class Alert : UserControl
    {
        public string Text { get; set; }
        public bool IsSuccess { get; set; }

        bool isFailure = false;
        public bool IsFailure { get => !IsSuccess }

        public Alert()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
