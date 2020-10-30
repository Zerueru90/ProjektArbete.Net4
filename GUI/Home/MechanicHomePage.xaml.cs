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
        List<Errands> ForCurrentMecanichErrandsList = new List<Errands>();
        public Mechanic _currentMechanic { get; set; }
        private CRUD _crud = new CRUD();

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

            Errands errands = null;
            foreach (var item in obj)
            {
                errands = item;
            }

            ForCurrentMecanichErrandsList.Add(errands);

            dgErrends.ItemsSource = ForCurrentMecanichErrandsList;

            txtName.Text = _currentMechanic.Name;
        }

        private void SkillCheck(CheckBox checkBox, string skill)
        {
            if (checkBox.IsChecked == true)
            {
                _currentMechanic.SkillLista.Add(skill);
                SetSkill(skill);
            }
        }
        private void SetSkill(string skill)
        {
            if (skill == "Bromsar")
            {
                _currentMechanic.Breaks = true;
            }
            if (skill == "Motor")
            {
                _currentMechanic.Engine = true;
            }
            if (skill == "Kaross")
            {
                _currentMechanic.Carbody = true;
            }
            if (skill == "Vindruta")
            {
                _currentMechanic.Windshield = true;
            }
            if (skill == "Tyre")
            {
                _currentMechanic.Tyre = true;
            }
        }

        private void ClickbtnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
