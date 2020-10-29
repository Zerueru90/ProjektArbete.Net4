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
                MechanicList._mechanicList.Add(new Mechanic() { Id = item.Id, Name = item.Name, DateOfBirthday = item.DateOfBirthday, DateOfEmployment = item.DateOfEmployment, 
                    DateOfEnd = item.DateOfEnd, IsMechanicUser = item.IsMechanicUser
                });
            }
            //Dessa två är för att fylla vår datagrid/lista av mekaniker från listan.
            dgUserAccess.ItemsSource = MechanicList._mechanicList;
            dgMainPage.ItemsSource = MechanicList._mechanicList;

            //DummyData
            MechanicList._mechanicList.Add(new Mechanic() { Name = "John", DateOfBirthday = DateTime.Now, DateOfEmployment = DateTime.Now, DateOfEnd = DateTime.Now, IdentityUser = new User() { Username = "John", Password = "Lösenord" } });

            txtName.Text = "Lasse";
            txtEmployementday.Text = "20-10-24";
            txtBirthday.Text = "20-10-24";
            txtUnEnmploymentday.Text = "22-10-24";

            int i = dgMainPage.Columns.Count;

            //dgMainPage.Columns[0].IsReadOnly = true;
            //dgMainPage.Columns[1].IsReadOnly = true;
            //dgMainPage.Columns[2].IsReadOnly = true;
            //dgMainPage.Columns[3].IsReadOnly = true;
            //dgMainPage.Columns[4].IsReadOnly = true;
            //dgMainPage.Columns[10].IsReadOnly = true;
        }
        private User _newUser;
        private Mechanic _mechanic;
        private CRUD _crud = new CRUD();

        private string _breakes = "Bromsar";
        private string _engine = "Motor";
        private string _carbody = "Kaross";
        private string _windshield = "Vindruta";
        private string _tyre = "Tyre";

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
                _crud.AddMechanic(_mechanic);

                MessageBox.Show("Saved");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgMainPage.SelectedItem != null)
            {//SelectedItem innebär vilken rad av mekaniker på datagriden man väljer
                _crud.RemoveMechanic(dgMainPage.SelectedItem as Mechanic);

                MessageBox.Show("Raderat mekaniker");
            }
        }

        private void BtnNewUser_Click(object sender, RoutedEventArgs e)
        {
            //var isMatch = RegexValidation.VerifyEmail(txtUserName.Text) && RegexValidation.VerifyPassword(txtPassword.Text);
            //if (isMatch)
            //{

                _mechanic.IdentityUser = _newUser = new User() { Username = txtUserName.Text, Password = txtPassword.Text };
                _crud.AddUser(_newUser, _mechanic.Id);
                _mechanic.IsMechanicUser = true;//För att trigga PropertyChanged 
                 MessageBox.Show("Sparat ny användare");
            //}
           
        }

        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (dgUserAccess.SelectedItem != null)
            {
                //Raderar inte själva mekanikern men raderar användarnamn och lösenord.
                Mechanic mec = (dgUserAccess.SelectedItem as Mechanic);
                _crud.RemoveUser(mec.IdentityUser, mec.Id);
                mec.IsMechanicUser = false; //För att trigga PropertyChanged så gör man denna till falsk så ändras det checkboxen direkt.

                MessageBox.Show("Raderat användare");
            }
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
            if (headername == "IdentityUser")
            {
                e.Cancel = true;
            }
            if (headername == "MechanicDoneList")
            {
                e.Cancel = true;
            }


        }

        private void DG2_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
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
            if (headername == "MechanicDoneList")
            {
                e.Cancel = true;
            }
            if (headername == "IdentityUser")
            {
                e.Cancel = true;
            }
        }

        #endregion
    }
}
