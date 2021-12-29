﻿using System;
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
    /// Interaction logic for DronesListModel.xaml
    /// </summary>
    public partial class DronesListView : UserControl
    {
        public DronesListView(List<PO.Drone> list)
        {
            InitializeComponent();
            DataContext = new ViewModels.DronesListViewModel(list);
        }
    }
}