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

        private string[] _unwantedColumns = new string[] { "SkillLista", "MechanicProgressList", "MechanicDoneList", "UserID", "ErrandsID", "OngoingErrands", "finnishedErrands", "Isfinnished", "MechanicID", "VeichleID",
                                                            "ChangeStatus", "ChangeVeichleID", "ChangeMechanicID", "ChangeMechanic", "ChangeDescription", "ChangeProblem"};

        private const string _breakes = "Breaks";
        private const string _engine = "Engine";
        private const string _carbody = "Carbody";
        private const string _windshield = "Windshield";
        private const string _tyre = "Tyre";

        public BosseHomePage()
        {
            InitializeComponent();

            //Dessa är för att fylla vår datagrid
            dgUserAccess.ItemsSource = MechanicList.MechanicLists;
            dgMechanicList.ItemsSource = MechanicList.MechanicLists;
            dgErrandList.ItemsSource = ErrandList.ErrandsList;
            dgVeichleList.ItemsSource = VehicleList.VehicleLists;

            #region DummyData

            //DummyData.ErrandData();
            //DummyData.UserData();
            DummyData.MecanichData();
            DummyData.VehicleData();


            //txtName.Text = "Lasse";
            //txtEmployementday.Text = "20-10-24";
            //txtBirthday.Text = "20-10-24";
            //txtUnEnmploymentday.Text = "22-10-24";

            txtModelName.Text = "Mercedes";
            txtRegNr.Text = "ewr159";
            txtOdoMeter.Text = "3000";
            txtRegDate.Text = "20-10-11";
            txtFuel.Text = "Diesel";

            txtDescription.Text = "blablablblablablblablablblablabl";
            #endregion

            //Viktig, denna fyller i comboboxen.
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

            //Ska dessa vara här?
            UpdateMechanicCheckBox();
            UpdateVechileCheckBox();
        }

        #region Mekaniker: Skapa/Radera/Uppdatera. Första sidan.
        private void BtnSaveNewMechanic_Click(object sender, RoutedEventArgs e)
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
        private void BtnDeleteMechanic_Click(object sender, RoutedEventArgs e)
        {
            if (dgMechanicList.SelectedItem != null)
            {//SelectedItem innebär vilken rad av mekaniker på datagriden man väljer
                _crud.RemoveMechanic(dgMechanicList.SelectedItem as Mechanic);

                MessageBox.Show("Raderat mekaniker");
            }
        }
        private void BtnUpdateMechanic_Click(object sender, RoutedEventArgs e)
        {
            if (dgMechanicList.SelectedItem != null)
            {
                var objMechanic = dgMechanicList.SelectedItem as Mechanic;
                //Varje gång man ändrar kompetenser och trycker på uppdatera så triggar den NotifyProp, då ändras båda fönstren
                objMechanic.NotifyPropertyChanged(_breakes);
                objMechanic.NotifyPropertyChanged(_engine);
                objMechanic.NotifyPropertyChanged(_carbody);
                objMechanic.NotifyPropertyChanged(_windshield);
                objMechanic.NotifyPropertyChanged(_tyre);
                MessageBox.Show("Uppdaterad");
            }
        }
        #endregion

        #region User: Skapa/Radera User inlogg. Andra sidan.
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
        #endregion

        #region Fordon: Skapa. har en metod för upprepande kod. Tredje sidan. (Antar att radera ej behövs, man ska ha koll på alla klargjorda fordon)
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
        private void NewVehicleInputData(Vehicle vehicle)
        {
            vehicle.ModelName = txtModelName.Text;
            vehicle.RegistrationNumber = txtRegNr.Text;
            vehicle.OdoMeter = Convert.ToDecimal(txtOdoMeter.Text);
            vehicle.RegistrationDate = Convert.ToDateTime(txtRegDate.Text);
            vehicle.Fuel = txtFuel.Text;
        }
        #endregion

        #region Ärenden: Skapa/Radera/Uppdatera. Tredje sidan. (Fattas dock att ärendet GRIDEN ska visa info om fordon, namn, reg osv.)
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
            if (dgErrandList.SelectedItem != null)
            {
                _crud.RemoveErrand(dgErrandList.SelectedItem as Errand);
            }
        }

        private void BtnUpdateErrand_Click(object sender, RoutedEventArgs e)
        {
            if (dgErrandList.SelectedItem != null)
            {
                var objErrand = dgErrandList.SelectedItem as Errand;
                var objVehicle = cbBoxVeichlesErrand.SelectedItem as Vehicle;

                objErrand.ChangeDescription = txtDescription.Text;
                objErrand.ChangeVeichleID = objVehicle.ID;
                objErrand.ChangeProblem = cbBoxProblemsErrand.SelectedItem.ToString();

                MessageBox.Show("Uppdaterad");
            }
        }
        #endregion

        #region Tilldelar en mekaniker ett ärende, kollar så att mekaniker inte har 2 pågående. Man kan även ändra status.
        private void BtnGiveMechanicErrand_Click(object sender, RoutedEventArgs e)
        {
            if (dgSkillList != null)
            {
                var objMechanic = cbBoxAppointMechanicAnErrand.SelectedItem as Mechanic;
                var objErrands = dgSkillList.SelectedItem as Errand;

                if (objMechanic.MechanicProgressList.Count != 2) //Så att man inte kan tilldela mer än 2 ärenden.
                {
                    objErrands.MechanicID = objMechanic.Id;
                    objErrands.ChangeMechanic = objMechanic.Name;
                    objErrands.ChangeStatus = "Pågående"; //När Bosse tilldelar ett ärende så ska den automatisk gå på Pågående.

                    objMechanic.ErrandsID.Add(objErrands.ID);

                    //Funkar
                    Task.AddProgressList(objMechanic, objErrands.ID.ToString());

                    MessageBox.Show($"{objMechanic.Name} har blivit tilldelat ett ärende");
                }
                else
                    MessageBox.Show($"{objMechanic.Name} har redan två ärenden pågående");
            }
        }
        private void BtnChangesStatusOnly_Click(object sender, RoutedEventArgs e)
        {
            if (_choosenComboBoxMechanicObject != null)
            {
                string newStatus = cbBoxChangeErrandsStatus.SelectedItem.ToString();
                var objMechanic = cbBoxAppointMechanicAnErrand.SelectedItem as Mechanic;
                var objErrand = dgSkillList.SelectedItem as Errand;

                if (newStatus == "Klar")
                {
                    Task.AddDoneList(objMechanic, objErrand.ID.ToString());

                    objErrand.ChangeStatus = newStatus;
                    MessageBox.Show("Uppdaterad");
                }
            }
        }
        #endregion

        #region Håller Chechboxen uppdaterade, kallas när man skapar något nytt i.e Mekaniker, Fordon
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

        #region Håller koll på vad som väljs i vissa comboxar och skapar objekt
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
                    dgSkillList.ItemsSource = ErrandList.ErrandsList.Where(x => x.Problem == item);
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

        private void CancelUnwantedColumnHeaderName(DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();

            //Cancel the column you don't want to generate

            for (int i = 0; i < _unwantedColumns.Length; i++)
            {
                if (headername == _unwantedColumns[i])
                {
                    e.Cancel = true;
                }
            }
        }

        private void dgMechanicList_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            CancelUnwantedColumnHeaderName(e);
        }

        private void dgUserAccess_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            CancelUnwantedColumnHeaderName(e);
        }

        private void dgSkillList_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            CancelUnwantedColumnHeaderName(e);
        }

        private void dgVeichleList_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            CancelUnwantedColumnHeaderName(e);
        }

        private void dgErrandList_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            CancelUnwantedColumnHeaderName(e);
        }
        #endregion
    }
}