using Logic;
using Logic.DAL;
using Logic.Entities;
using Logic.Entities.Person_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            //Hämtar från Listan och lägger upp allt i DataGrid, längre fram så kommer det innehålla mekaniker i listan från Json filen.
            foreach (var item in MechanicList._mechanicList)
            {
                MechanicList._mechanicList.Add(new Mechanic() { Id = item.Id, Name = item.Name, DateOfBirthday = item.DateOfBirthday, DateOfEmployment = item.DateOfEmployment, DateOfEnd = item.DateOfEnd, SkillLista = item.SkillLista, IdentityUser = item.IdentityUser });
            }
            //Dessa två är för att fylla vår datagrid/lista av mekaniker från listan.
            dgUserAccess.ItemsSource = MechanicList._mechanicList;
            dgMainPage.ItemsSource = MechanicList._mechanicList;

            MechanicList._mechanicList.Add(new Mechanic() { Name = "John", DateOfBirthday = DateTime.Now, DateOfEmployment = DateTime.Now, DateOfEnd = DateTime.Now, SkillLista = { "Bromsar", "Kaross", "Vindruta" } });

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
                SetSkill(skill);
            }
        }

        //Funderar på att radera List<string> SkillLista då detta funkar väldigt smidigt med WPF, nu när man sparar ny användare så får de egenskaperna ett sant eller falsk värde. 
        private void SetSkill(string skill)
        {
            if (skill == "Bromsar")
            {
                _mechanic.Breaks = true;
            }
            if (skill == "Motor")
            {
                _mechanic.Engine = true;
            }
            if (skill == "Kaross")
            {
                _mechanic.Carbody = true;
            }
            if (skill == "Vindruta")
            {
                _mechanic.Windshield = true;
            }
            if (skill == "Tyre")
            {
                _mechanic.Tyre = true;
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

        private void BtnNewUser_Click(object sender, RoutedEventArgs e)
        {

            //var isMatch = RegexValidation.VerifyEmail(txtUserName.Text) && RegexValidation.VerifyPassword(txtPassword.Text);
            //if (isMatch)
            //{
            //_crud.AddUser(_newUser = new User() { Username = txtUserName.Text, Password = txtPassword.Text });

            _mechanic.IdentityUser = _newUser = new User() { Username = txtUserName.Text, Password = txtPassword.Text };
                MessageBox.Show("Saved");
            //}
           
        }

        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {

        }

        #region
        //Tagen från doc windows sidan, väldigt smidigt om man inte vill ha med något från Mekaniker egenskaperna till DataGrid listan.
        //Access and update columns during autogeneration
        private void DG1_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();

            //Cancel the column you don't want to generate
            if (headername == "SkillLista")
            {
                e.Cancel = true;
            }
            if (headername == "MechanicProgressList")
            {
                e.Cancel = true;
            }

            //update column details when generating
            //if (headername == "FirstName")
            //{
            //    e.Column.Header = "First Name";
            //}
            //else if (headername == "LastName")
            //{
            //    e.Column.Header = "Last Name";
            //}
            //else if (headername == "EmailAddress")
            //{
            //    e.Column.Header = "Email";
            //}
        }
        #endregion
    }
}
