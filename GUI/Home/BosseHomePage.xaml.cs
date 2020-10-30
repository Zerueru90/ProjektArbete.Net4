using Logic;
using Logic.DAL;
using Logic.Entities;
using Logic.Entities.Person_Entities;
using Logic.Entities.Vehicles_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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

        private User _newUser;
        private Mechanic _mechanic;
        private CRUD _crud = new CRUD();

        private Mechanic _choosenComboBoxMechanicObject;
        private const string _breakes = "Breaks";
        private const string _engine = "Engine";
        private const string _carbody = "Carbody";
        private const string _windshield = "Windshield";
        private const string _tyre = "Tyre";


        public BosseHomePage()
        {
            InitializeComponent();

            //Hämtar från Listan och lägger upp allt i DataGrid, längre fram så kommer det innehålla mekaniker i listan från Json filen.
            foreach (var item in MechanicList._mechanicList)
            {
                MechanicList._mechanicList.Add(new Mechanic() 
                { 
                    Id = item.Id, Name = item.Name,
                    DateOfBirthday = item.DateOfBirthday, 
                    DateOfEmployment = item.DateOfEmployment, 
                    DateOfEnd = item.DateOfEnd, 
                    MechanicUser = item.MechanicUser
                });
            }
            foreach (var item in ErrandList.ErrandsList)
            {
                ErrandList.ErrandsList.Add(new Errands()
                {
                    ErrandsID = item.ErrandsID,
                    Description = item.Description,
                    Problem = item.Problem,
                    Status = item.Status
                });
            }

            //Dessa tre är för att fylla vår datagrid/lista av mekaniker från listan.
            dgUserAccess.ItemsSource = MechanicList._mechanicList;
            dgMainPage.ItemsSource = MechanicList._mechanicList;
            dgErrands.ItemsSource = ErrandList.ErrandsList;
            //dgErrandOngoingAndDone.ItemsSource = ErrandList.ErrandsList;

            #region DummyData
            ErrandList.ErrandsList.Add(new Errands()
            {
                Description = "blablabla",
                Problem = "Tyre",
                Status = "Available"
            });

            Errands errands = null;
            foreach (var item in ErrandList.ErrandsList)
            {
                errands = item;
            }
            MechanicList._mechanicList.Add(new Mechanic()
            {
                Name = "John",
                DateOfBirthday = DateTime.Now,
                DateOfEmployment = DateTime.Now,
                DateOfEnd = DateTime.Now,
            });
            MechanicList._mechanicList.Add(new Mechanic()
            {
                Name = "Dave",
                DateOfBirthday = DateTime.Now,
                DateOfEmployment = DateTime.Now,
                DateOfEnd = DateTime.Now,
                Breaks = true,
                Engine = true
            });

            txtName.Text = "Lasse";
            txtEmployementday.Text = "20-10-24";
            txtBirthday.Text = "20-10-24";
            txtUnEnmploymentday.Text = "22-10-24";
            #endregion

            //Viktig, denna fyller i comboboxen med objekt av Mechanic MEN visar bara Namen på mekaniker. Ordnade i XAML.
            foreach (var item in MechanicList._mechanicList)
            {
                cbBoxMechanics.Items.Add(item);
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

            Mechanic mec = (dgUserAccess.SelectedItem as Mechanic);
            var obj = _newUser = new User() { Username = txtUserName.Text, Password = txtPassword.Text };
            _crud.AddUser(_newUser);
            mec.UserID = obj.ID; 
            mec.MechanicUser = true;//För att trigga PropertyChanged 
            MessageBox.Show("Sparat ny användare");


            //}

        }

        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (dgUserAccess.SelectedItem != null)
            {
                //Raderar inte själva mekanikern men raderar användarnamn och lösenord som den ska
                Mechanic mec = (dgUserAccess.SelectedItem as Mechanic);
                _crud.RemoveUser(mec.UserID);
                mec.MechanicUser = false; //För att trigga PropertyChanged så gör man denna till falsk så ändras det checkboxen direkt.

                MessageBox.Show("Raderat användare");
            }
        }

    # region Tagen från doc windows sidan, väldigt smidigt om man inte vill ha med något från Mekaniker egenskaperna till DataGrid listan.
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
            if (headername == "MechanicDoneList")
            {
                e.Cancel = true;
            }
            if (headername == "UserID")
            {
                e.Cancel = true;
            }
            if (headername == "ErrandsID")
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
            if (headername == "UserID")
            {
                e.Cancel = true;
            }
            if (headername == "ErrandsID")
            {
                e.Cancel = true;
            }
        }



        private void dgErrands_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();

            //Cancel the column you don't want to generate
            if (headername == "OngoingErrands")
            {
                e.Cancel = true;
            }
            if (headername == "finnishedErrands")
            {
                e.Cancel = true;
            }
            if (headername == "Isfinnished")
            {
                e.Cancel = true;
            }
            if (headername == "ID")
            {
                e.Cancel = true;
            }
            if (headername == "RegNrID")
            {
                e.Cancel = true;
            }
            if (headername == "RegistrationNumber")
            {
                e.Cancel = true;
            }
            if (headername == "Registrated")
            {
                e.Cancel = true;
            }
            if (headername == "Fuel")
            {
                e.Cancel = true;
            }
            if (headername == "OdoMeter")
            {
                e.Cancel = true;
            }
            if (headername == "ModelName")
            {
                e.Cancel = true;
            }
        }
        #endregion

        private void BtnUpdateSkill_Click(object sender, RoutedEventArgs e)
        {
            //Varje gång man ändrar kompetenser och trycker på uppdatera så triggar den NotifyProp, då ändras båda fönstren
            Mechanic mec = (dgMainPage.SelectedItem as Mechanic);
            mec.NotifyPropertyChanged(_breakes);
            mec.NotifyPropertyChanged(_engine);
            mec.NotifyPropertyChanged(_carbody);
            mec.NotifyPropertyChanged(_windshield);
            mec.NotifyPropertyChanged(_tyre);
            MessageBox.Show("Uppdaterad");
        }

        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            if (_choosenComboBoxMechanicObject != null)
            {

            }
        }

        //Denna har med comboxboxen att göra, det som är så nice är att när man väljer ett namn från listan så lagras mekaniker objektet i "var obj" och inte bara namnet sen därifrån så kan man enkelt arbeta med att lägga till Task/Errands osv.
        void OnDropDownClosed(object sender, EventArgs e)
        {
            if (cbBoxMechanics.IsDropDownOpen == false)
            {
                _choosenComboBoxMechanicObject = cbBoxMechanics.SelectedItem as Mechanic;

                dgErrandOngoingAndDone.ItemsSource = ErrandList.ErrandsList.Where(x => x.ID == _choosenComboBoxMechanicObject.ErrandsID);
            }
        }
    }
}
