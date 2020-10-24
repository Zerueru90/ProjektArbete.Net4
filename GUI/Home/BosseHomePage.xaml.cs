using Logic.DAL;
using Logic.Entities.Person_Entities;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for BosseHomePage.xaml
    /// </summary>
    public partial class BosseHomePage : Page
    {
        public BosseHomePage()
        {
            InitializeComponent();
        }

        private Mechanic _mechanic;
        private string _brakes;
        private string _engine;
        private string _carbody;
        private string _windshield;
        private string _tires;


        private void BtnSaveNewMechanic(object sender, RoutedEventArgs e)
        {
            _mechanic = new Mechanic();
            _mechanic.Name = txtName.Text;
            _mechanic.DateOfBirthday = Convert.ToDateTime(txtBirthday.Text);
            _mechanic.DateOfEmployment = Convert.ToDateTime(txtEmployementday.Text);
            MechanicDataAccess.SaveNewMechanicData(_mechanic);
        }

        private void BtnSaveNewUser(object sender, ContextMenuEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
