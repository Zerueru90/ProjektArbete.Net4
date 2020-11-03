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

            //Dessa tre är för att fylla vår datagrid/lista av mekaniker från listan.
            dgUserAccess.ItemsSource = MechanicList.MechanicLists;
            dgMainPage.ItemsSource = MechanicList.MechanicLists;
            //dgErrands.ItemsSource = ErrandList.ErrandsList;
            dgNewErrands.ItemsSource = ErrandList.ErrandsList;
            dgVeichleList.ItemsSource = VehicleList.VehicleLists;
            //dgErrandOngoingAndDone.ItemsSource = ErrandList.ErrandsList;

            #region DummyData

            //DummyData.ErrandData();
            //DummyData.UserData();
            DummyData.MecanichData();
            DummyData.VehicleData();


            txtName.Text = "Lasse";
            txtEmployementday.Text = "20-10-24";
            txtBirthday.Text = "20-10-24";
            txtUnEnmploymentday.Text = "22-10-24";

            txtModelName.Text = "Mercedes";
            txtRegNr.Text = "ewr159";
            txtOdoMeter.Text = "3000";
            txtRegDate.Text = "20-10-11";
            txtFuel.Text = "Diesel";

            txtDescription.Text = "blablablblablablblablablblablabl";
            #endregion

            //Viktig, denna fyller i comboboxen med objekt av Mechanic MEN visar bara Namen på mekaniker. Ordnade i XAML.
            foreach (var item in Enum.GetValues(typeof(Vehicle.VeichleType)))
            {
                cbBoxVeichleType.Items.Add(item.ToString());
            }
            foreach (var item in Enum.GetValues(typeof(Enums.VehicelProblems)))
            {
                cbBoxProblemsErrand.Items.Add(item.ToString());
            }
            foreach (var item in Enum.GetValues(typeof(Enums.VehicelStatus)))
            {
                cbBoxChangeErrandsStatus.Items.Add(item.ToString());
            }

            UpdateMechanicCheckBox();
            //Fyller comboboxen i Registrera ärenden -Fordon.
            UpdateVechileCheckBox();
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

                UpdateMechanicCheckBox();
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
            mec.UserID = obj.ID; //Viktigt att detta görs, prop MechanicUser settar auto till true
            mec.MechanicUser = true;//Här ska den vanligtvis setta men den triggar bara PropertyChanged så att WPFn ändras
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
                mec.UserID = Guid.Empty;//Viktigt att detta görs, prop MechanicUser settar auto till false
                mec.MechanicUser = false; //Här ska den vanligtvis setta men den triggar bara PropertyChanged så att WPFn ändras, så gör man denna till falsk så ändras det checkboxen direkt.

                MessageBox.Show("Raderat användare");
            }
        }

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

        private void BtnChangesStatusOnly_Click(object sender, RoutedEventArgs e)
        {
            if (_choosenComboBoxMechanicObject != null)
            {
                string newStatus = cbBoxChangeErrandsStatus.SelectedItem.ToString();
                var objMechanic = cbBoxAppointMechanicAnErrand.SelectedItem as Mechanic;
                var objErrand = dgChangeErrands.SelectedItem as Errand;

                if (newStatus == "Klar")
                {
                    //Funkar inte, eftersom MechanicDoneList är null
                    Task.AddDoneList(objMechanic, objErrand.ErrandsID.ToString());

                    objErrand.ChangeStatus = newStatus;
                    MessageBox.Show("Uppdaterad");
                }
            }
        }

        private void BtnSaveVeichle_Click(object sender, RoutedEventArgs e)
        {
            var obj = cbBoxVeichleType.SelectedItem.ToString();
            switch (obj)
            {
                case "Bil":
                    Car _newCar = new Car();
                    NewVehicleInputData(_newCar);
                    if (checkBoxCarHook.IsChecked == true)
                    {
                        _newCar.SetTowbarValue(true);
                    }
                    VehicleList.VehicleLists.Add(_newCar);
                    break;

                case "Motorcykel":
                    Motorbike _newMotorbike = new Motorbike();
                    NewVehicleInputData(_newMotorbike);
                    VehicleList.VehicleLists.Add(_newMotorbike);
                    break;

                case "Lastbil":
                    Truck _newTruck = new Truck();
                    NewVehicleInputData(_newTruck);
                    _newTruck.SetMaxLoadWeight(Convert.ToDecimal(txtMaxLoadWeight.Text));
                    VehicleList.VehicleLists.Add(_newTruck);
                    break;

                case "Buss":
                    Bus _newBus = new Bus();
                    NewVehicleInputData(_newBus);
                    _newBus.SetPassengersValue(Convert.ToInt32(txtMaxPassanger.Text));
                    VehicleList.VehicleLists.Add(_newBus);
                    break;

                default:
                    break;
            }
            UpdateVechileCheckBox();
            MessageBox.Show("Sparad");
        }

        //Kan förmodligen flyttas till Logik... som en massa andra i denna blad.
        private void FillingSkillList(Mechanic mec)
        {
            if (mec.Breaks == true)
            {
                mec.SkillLista.Add("Bromsar");
            }
            if (mec.Engine == true)
            {
                mec.SkillLista.Add("Motor");
            }
            if (mec.Carbody == true)
            {
                mec.SkillLista.Add("Kaross");
            }
            if (mec.Windshield == true)
            {
                mec.SkillLista.Add("Vindruta");
            }
            if (mec.Tyre == true)
            {
                mec.SkillLista.Add("Däck");
            }
        }

        private void BtnGiveMechanicErrand_Click(object sender, RoutedEventArgs e)
        {
            if (dgChangeErrands != null)
            {
                var objMechanic = cbBoxAppointMechanicAnErrand.SelectedItem as Mechanic;
                var objErrands = dgChangeErrands.SelectedItem as Errand;

                if (objMechanic.MechanicProgressList.Count != 2) //Så att man inte kan tilldela mer än 2 ärenden.
                {
                    objErrands.MechanicID = objMechanic.Id;
                    objErrands.ChangeMechanic = objMechanic.Name;
                    objErrands.ChangeStatus = "Pågående"; //När Bosse tilldelar ett ärende så ska den automatisk gå på Pågående.

                    objMechanic.ErrandsID.Add(objErrands.ErrandsID);

                    //Funkar
                    Task.AddProgressList(objMechanic, objErrands.ErrandsID.ToString());

                    MessageBox.Show($"{objMechanic.Name} har blivit tilldelat ett ärende");
                }
                else
                    MessageBox.Show($"{objMechanic.Name} har redan två ärenden pågående");
            }
        }

        private void BtnSaveErrand_Click(object sender, RoutedEventArgs e)
        {
            var objVehicle = cbBoxVeichlesErrand.SelectedItem as Vehicle;

            Errand newErrand = new Errand();
            newErrand.Description = txtDescription.Text;
            newErrand.Problem = cbBoxProblemsErrand.SelectedItem.ToString();
            newErrand.VeichleID = objVehicle.ID;

            ErrandList.ErrandsList.Add(newErrand);

            MessageBox.Show("Sparad");
        }

        private void BtnDeleteErrand_Click(object sender, RoutedEventArgs e)
        {
            if (dgNewErrands.SelectedItem != null)
            {
                _crud.RemoveErrand(dgNewErrands.SelectedItem as Errand);
            }
        }

        //funkar att uppdatera men det ändras i inte på datagriden...
        private void BtnUpdateErrand_Click(object sender, RoutedEventArgs e)
        {
            if (dgNewErrands.SelectedItem != null)
            {
                Errand errands = dgNewErrands.SelectedItem as Errand;
                var objVehicle = cbBoxVeichlesErrand.SelectedItem as Vehicle;

                errands.ChangeDescription = txtDescription.Text;
                errands.ChangeVeichleID = objVehicle.ID;
                errands.ChangeProblem = cbBoxProblemsErrand.SelectedItem.ToString();

                //obj.ErrandsID[obj.ErrandsID.FindIndex(x => x.Equals(cbBoxVeichlesErrand.SelectedIndex))] = errands.ErrandsID;


                MessageBox.Show("Uppdaterad");
            }
        }
        private void NewVehicleInputData(Vehicle vehicle)
        {
            vehicle.ModelName = txtModelName.Text;
            vehicle.RegistrationNumber = txtRegNr.Text;
            vehicle.OdoMeter = Convert.ToDecimal(txtOdoMeter.Text);
            vehicle.RegistrationDate = Convert.ToDateTime(txtRegDate.Text);
            vehicle.Fuel = txtFuel.Text;
        }

        #region Håller Chechboxen uppdaterade, kallas när man skapar något nytt.
        private void UpdateVechileCheckBox()
        {
            cbBoxVeichlesErrand.Items.Clear();
            foreach (var item in VehicleList.VehicleLists)
            {
                cbBoxVeichlesErrand.Items.Add(item);
            }
        }
        private void UpdateMechanicCheckBox()
        {
            cbBoxAppointMechanicAnErrand.Items.Clear();
            foreach (var item in MechanicList.MechanicLists)
            {
                cbBoxAppointMechanicAnErrand.Items.Add(item);
            }
        }
        #endregion

        #region Denna har med comboxboxen att göra, det som är så nice är att när man väljer ett namn från listan så lagras mekaniker objektet i "var obj" och inte bara namnet sen därifrån så kan man enkelt arbeta med att lägga till Task/Errands osv.
        void cbBoxAppointMechanicAnErrand_OnDropDownClosed(object sender, EventArgs e)
        {
            cbBoxChangeErrandsStatus.Visibility = Visibility.Hidden;
            btnChangeStatusErrand.Visibility = Visibility.Hidden;

            if (cbBoxAppointMechanicAnErrand.IsDropDownOpen == false)
            {
                _choosenComboBoxMechanicObject = cbBoxAppointMechanicAnErrand.SelectedItem as Mechanic;
                FillingSkillList(_choosenComboBoxMechanicObject);

                //Kollar igenom SkillListan och varje Ärende som har det problemet mekanikern har kompetensen så öppnas den.
                foreach (var item in _choosenComboBoxMechanicObject.SkillLista)
                {
                    dgChangeErrands.ItemsSource = ErrandList.ErrandsList.Where(x => x.Problem == item);
                }
                if (_choosenComboBoxMechanicObject.ErrandsID.Count != 0)
                {
                    cbBoxChangeErrandsStatus.Visibility = Visibility.Visible;
                    btnChangeStatusErrand.Visibility = Visibility.Visible;
                }
            }
        }
        private void cbBoxVeichleType_DropDownClosed(object sender, EventArgs e)
        {
            if (cbBoxVeichleType.IsDropDownOpen == false)
            {
                if (cbBoxVeichleType.SelectedItem != null)
                {
                    checkBoxCarHook.Visibility = Visibility.Hidden;
                    labelBilTyp.Visibility = Visibility.Hidden;
                    cbBoxCarType.Visibility = Visibility.Hidden;
                    labelMaxLoad.Visibility = Visibility.Hidden;
                    txtMaxLoadWeight.Visibility = Visibility.Hidden;
                    labelPassanger.Visibility = Visibility.Hidden;
                    txtMaxPassanger.Visibility = Visibility.Hidden;
                    cbBoxCarType.Items.Clear();
                    if (cbBoxVeichleType.SelectedItem.ToString() == "Bil")
                    {
                        foreach (var item in Enum.GetValues(typeof(Car.CarType)))
                        {
                            cbBoxCarType.Items.Add(item.ToString());
                        }

                        checkBoxCarHook.Visibility = Visibility.Visible;
                        labelBilTyp.Visibility = Visibility.Visible;
                        cbBoxCarType.Visibility = Visibility.Visible;
                    }
                    if (cbBoxVeichleType.SelectedItem.ToString() == "Lastbil")
                    {
                        labelMaxLoad.Visibility = Visibility.Visible;
                        txtMaxLoadWeight.Visibility = Visibility.Visible;
                    }
                    if (cbBoxVeichleType.SelectedItem.ToString() == "Buss")
                    {
                        labelPassanger.Visibility = Visibility.Visible;
                        txtMaxPassanger.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        #endregion   

        #region Tagen från doc windows sidan, väldigt smidigt om man inte vill ha med något från Mekaniker egenskaperna till DataGrid listan.   
        private void dgMainPage_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
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

        private void dgUserAccess_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
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

        private void dgChangeErrands_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
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
            if (headername == "Registrated")
            {
                e.Cancel = true;
            }
            if (headername == "MechanicID")
            {
                e.Cancel = true;
            }
            if (headername == "VeichleID")
            {
                e.Cancel = true;
            }
            if (headername == "ErrandsID")
            {
                e.Cancel = true;
            }
            if (headername == "ChangeStatus")
            {
                e.Cancel = true;
            }


            if (headername == "ChangeVeichleID")
            {
                e.Cancel = true;
            }
            if (headername == "ChangeMechanicID")
            {
                e.Cancel = true;
            }
            if (headername == "ChangeMechanic")
            {
                e.Cancel = true;
            }
            if (headername == "ChangeDescription")
            {
                e.Cancel = true;
            }
            if (headername == "ChangeProblem")
            {
                e.Cancel = true;
            }
        }

        private void dgVeichleList_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();

            //Cancel the column you don't want to generate
            if (headername == "Id")
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
            if (headername == "DateOfBirthday")
            {
                e.Cancel = true;
            }
            if (headername == "DateOfEmployment")
            {
                e.Cancel = true;
            }
            if (headername == "DateOfEnd")
            {
                e.Cancel = true;
            }
        }

        private void dgNewErrands_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();

            //Cancel the column you don't want to generate
            if (headername == "ID")
            {
                e.Cancel = true;
            }
            if (headername == "MechanicID")
            {
                e.Cancel = true;
            }
            if (headername == "OdoMeter")
            {
                e.Cancel = true;
            }
            if (headername == "RegistrationDate")
            {
                e.Cancel = true;
            }
            if (headername == "Fuel")
            {
                e.Cancel = true;
            }
            if (headername == "ErrandsID")
            {
                e.Cancel = true;
            }
            if (headername == "VeichleID")
            {
                e.Cancel = true;
            }
            if (headername == "ChangeStatus")
            {
                e.Cancel = true;
            }
            if (headername == "ChangeVeichleID")
            {
                e.Cancel = true;
            }
            if (headername == "ChangeMechanicID")
            {
                e.Cancel = true;
            }
            if (headername == "ChangeMechanic")
            {
                e.Cancel = true;
            }
            if (headername == "ChangeDescription")
            {
                e.Cancel = true;
            }
            if (headername == "ChangeProblem")
            {
                e.Cancel = true;
            }
        }

        #endregion

        
    }
}
