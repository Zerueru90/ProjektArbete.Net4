using Logic.Entities.Vehicles_Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for FullVehicleViewWindow.xaml
    /// </summary>
    public partial class FullVehicleViewWindow : Window
    {

        public FullVehicleViewWindow(IEnumerable<Vehicle> vehicle)
        {
            InitializeComponent();
            dgFullVehicleView.ItemsSource = vehicle;
        }
    }
}
