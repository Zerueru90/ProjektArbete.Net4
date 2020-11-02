using Logic;
using Logic.Entities.Person_Entities;
using Logic.Entities.Vehicles_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for MechanicHomePage.xaml
    /// </summary>
    public partial class MechanicHomePage : Page
    {
        List<Errand> ForCurrentMecanichErrandsList = new List<Errand>();
        public Mechanic _currentMechanic { get; set; }
        private CRUD _crud = new CRUD();
        private Errand _errands { get; set; }

        private string _breakes = "Bromsar";
        private string _engine = "Motor";
        private string _carbody = "Kaross";
        private string _windshield = "Vindruta";
        private string _tyre = "Tyre";


        public MechanicHomePage()
        {
            InitializeComponent();

            var obj = from err in ErrandList.ErrandsList
                      where err.ErrandsID == _currentMechanic.ErrandsID
                      select err;

            Errand errands = null;
            foreach (var item in obj)
            {
                errands = item;
            }

            ForCurrentMecanichErrandsList.Add(errands);

            dgErrends.ItemsSource = ForCurrentMecanichErrandsList;

            //LableNamn.Name = _currentMechanic.Name;
            foreach (var item in Enum.GetValues(typeof(Enums.VehicelStatus)))
            {
                comboBoxErrands.Items.Add(item.ToString());
            }
        }
        private void ClickbtnSave_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
