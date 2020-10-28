using Logic;
using Logic.DAL;
using Logic.Entities;
using Logic.Entities.Person_Entities;
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
    /// Interaction logic for BosseHomePage.xaml
    /// </summary>
    public partial class BosseHomePage : Page
    {
        public BosseHomePage()
        {
            InitializeComponent();

            List<Mechanic> mechanics = new List<Mechanic>();

            //Hämtar från Listan och lägger upp allt i DataGrid, längre fram så kommer det innehålla mekaniker i listan från Json filen.
            foreach (var item in MechanicList._mechanicList)
            {
                mechanics.Add(new Mechanic() { Id = item.Id, Name = item.Name, DateOfBirthday = item.DateOfBirthday, DateOfEmployment = item.DateOfEmployment, DateOfEnd = item.DateOfEnd, SkillLista = item.SkillLista, users = item.users });
            }

            dgUserAccess.ItemsSource = mechanics;
            dgMainPage.ItemsSource = mechanics;

            txtName.Text = "Lasse";
            txtEmployementday.Text = "20-10-24";
            txtBirthday.Text = "20-10-24";
            txtUnEnmploymentday.Text = "22-10-24";
        }
        private User _newUser;
        private Mechanic _mechanic;
        private CRUD _crud = new CRUD();

        private string _breakes = "Bromsar";
        private string _engine = "Motor";
        private string _carbody = "Kaross";
        private string _windshield = "Vindruta";
        private string _tyre = "Tyre";

        //Simple metod för att kolla igenom om checkboxen är klickade och sparar dom sen till SkillListan som finns i Mekaniker klassen.
        private void SkillCheck(CheckBox checkBox, string skill)
        {
            if (checkBox.IsChecked == true) 
            {
                _mechanic.SkillLista.Add(skill);
            }
        }

        //Vad denna metod ska göras är när mekaniker hämtas från listan så ska checkboxen checkas men kommer inte på vad för kod man skriver för att manuellt klicka i checkbox.
        private void SkillCheck2(string skill)
        {
            if (skill == _breakes)
            {
                //Om strängen existerar så checkas boxen.
            }
            if (skill == _engine)
            {
                //Om strängen existerar så checkas boxen.
            }
            if (skill == _carbody)
            {
                //Om strängen existerar så checkas boxen.
            }
            if (skill == _windshield)
            {
                //Om strängen existerar så checkas boxen.
            }
            if (skill == _tyre)
            {
                //Om strängen existerar så checkas boxen.
            }
        }

        private void BtnSaveNewMechanic(object sender, RoutedEventArgs e)
        {
            if (!(txtName.Text == null) || !(txtName.Text == ""))
            {
                _mechanic = new Mechanic
                {
                    Name = txtName.Text,
                    DateOfBirthday = Convert.ToDateTime(txtBirthday.Text),
                    DateOfEmployment = Convert.ToDateTime(txtEmployementday.Text),
                    DateOfEnd = Convert.ToDateTime(txtUnEnmploymentday.Text)
                };

                SkillCheck(checkBoxBreaks, _breakes);
                SkillCheck(checkBoxEngine, _engine);
                SkillCheck(checkBoxBody, _carbody);
                SkillCheck(checkBoxWindShield, _windshield);
                SkillCheck(checkBoxTyre, _tyre);

                _crud.AddMechanic(_mechanic);

                MessageBox.Show("Saved");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgMainPage.SelectedItem != null)
            {//SelectedItem innebär vilken rad av mekaniker på datagriden man väljer
                _crud.RemoveMechanic(dgMainPage.SelectedItem as Mechanic);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgMainPage.SelectedItem != null)
            {
                //Här så ska man välje en mekaniker från lista, kunna ändra namn osv och sen uppdatera.
            }
        }


        private void BtnNewUser_Click(object sender, RoutedEventArgs e)
        {
            _newUser = new User()
            {
                Username = txtUserName.Text,
                Password = txtPassword.Text
            };
            _crud.AddUser(_newUser);
        }

        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
