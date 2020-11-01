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
                ErrandList.ErrandsList.Add(new Errand()
                {
                    ErrandsID = item.ErrandsID,
                    Description = item.Description,
                    Problem = item.Problem,
                    Status = item.Status
                });
            }
            foreach (var item in VehicleList.VehicleLists)
            {
                VehicleList.VehicleLists.Add(new Vehicle()
                {
                    ID = item.ID,
                    ModelName = item.ModelName,
                    RegistrationNumber = item.RegistrationNumber,
                    OdoMeter = item.OdoMeter,
                    RegistrationDate = item.RegistrationDate,
                    Fuel = item.Fuel
                });
            }

            //Dessa tre är för att fylla vår datagrid/lista av mekaniker från listan.
            dgUserAccess.ItemsSource = MechanicList._mechanicList;
            dgMainPage.ItemsSource = MechanicList._mechanicList;
            dgErrands.ItemsSource = ErrandList.ErrandsList;
            dgNewErrands.ItemsSource = ErrandList.ErrandsList;
            dgVeichleList.ItemsSource = VehicleList.VehicleLists;
            //dgErrandOngoingAndDone.ItemsSource = ErrandList.ErrandsList;

            #region DummyData
            ErrandList.ErrandsList.Add(new Errand()
            {
                Description = "blablabla",
                Problem = "Tyre",
                Status = "Available"
            });

            Errand errands = null;
            foreach (var item in ErrandList.ErrandsList)
            {
                errands = item;
            }
            //--------Lägga till new User
            UserList.UserLists.Add(new User()
            {
                Username = "John",
                Password = "password"
            });
            UserList.UserLists.Add(new User()
            {
                Username = "Dave",
                Password = "password"
            });
            Guid JohnID = Guid.Empty;
            Guid DaveID = Guid.Empty;

            foreach (var item in UserList.UserLists)
            {
                if (item.Username == "John")
                {
                    JohnID = item.ID;
                }
                if (item.Username == "Dave")
                {
                    DaveID = item.ID;
                }
            }

            //--------Lägga till new User ---STOP

            //--------Lägga till new Mechanic och lägger in new User
            MechanicList._mechanicList.Add(new Mechanic()
            {
                Name = "John",
                DateOfBirthday = DateTime.Now,
                DateOfEmployment = DateTime.Now,
                DateOfEnd = DateTime.Now,
                UserID = JohnID
            });
            MechanicList._mechanicList.Add(new Mechanic()
            {
                Name = "Dave",
                DateOfBirthday = DateTime.Now,
                DateOfEmployment = DateTime.Now,
                DateOfEnd = DateTime.Now,
                Breaks = true,
                Engine = true,
                UserID = DaveID
            });
            //--------Lägga till new Mechanic ---STOP

            txtName.Text = "Lasse";
            txtEmployementday.Text = "20-10-24";
            txtBirthday.Text = "20-10-24";
            txtUnEnmploymentday.Text = "22-10-24";

            txtModelName.Text = "Audi";
            txtRegNr.Text = "abc123";
            txtOdoMeter.Text = "3000";
            txtRegDate.Text = "20-10-11";
            txtFuel.Text = "Diesel";

            txtDescription.Text = "blablablblablablblablablblablabl";
            #endregion

            //Viktig, denna fyller i comboboxen med objekt av Mechanic MEN visar bara Namen på mekaniker. Ordnade i XAML.
            foreach (var item in MechanicList._mechanicList)
            {
                cbBoxMechanicForChaningErrands.Items.Add(item);
                cbBoxMechanicForNewErrands.Items.Add(item);
            }
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
                cbBoxStatusErrand.Items.Add(item.ToString());
            }

            //Fyller comboboxen i Registrera ärenden -Fordon.
            foreach (var item in VehicleList.VehicleLists)
            {
                cbBoxVeichlesErrand.Items.Add(item);
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

        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            if (_choosenComboBoxMechanicObject != null)
            {

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
            UpdateVechileCheckBoc();
            MessageBox.Show("Sparad");
        }

        private void BtnSaveErrand_Click(object sender, RoutedEventArgs e)
        {
            var obj = cbBoxMechanicForNewErrands.SelectedItem as Mechanic;
            var IEnumiratedID = MechanicList._mechanicList.Where(x => x.Id == obj.Id).Select(y => y.Id);
            Guid ID = Guid.Empty;
            foreach (var item in IEnumiratedID)
            {
                ID = item;
            }

            var objVehicle = cbBoxVeichlesErrand.SelectedItem as Vehicle;
            var vID = VehicleList.VehicleLists.Where(x => x.ID == objVehicle.ID).Select(y => y.ID);
            Guid vehicleID = Guid.Empty;
            foreach (var item in vID)
            {
                vehicleID = item;
            }

            Errand newErrand = new Errand();
            newErrand.Description = txtDescription.Text;
            newErrand.VeichleID = vehicleID;
            newErrand.Problem = cbBoxProblemsErrand.SelectedItem.ToString();
            newErrand.MechanicID = ID;
            newErrand.Status = cbBoxStatusErrand.SelectedItem.ToString();

            ErrandList.ErrandsList.Add(newErrand);
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
        private void UpdateMechanicCheckBoc()
        {
            cbBoxMechanicForChaningErrands.Items.Clear();
            cbBoxMechanicForNewErrands.Items.Clear();
            foreach (var item in MechanicList._mechanicList)
            {
                cbBoxMechanicForChaningErrands.Items.Add(item);
                cbBoxMechanicForNewErrands.Items.Add(item);
            }
        }
        private void UpdateVechileCheckBoc()
        {
            cbBoxVeichlesErrand.Items.Clear();
            foreach (var item in VehicleList.VehicleLists)
            {
                cbBoxVeichlesErrand.Items.Add(item);
            }
        }

        # region Denna har med comboxboxen att göra, det som är så nice är att när man väljer ett namn från listan så lagras mekaniker objektet i "var obj" och inte bara namnet sen därifrån så kan man enkelt arbeta med att lägga till Task/Errands osv.
        void OnDropDownClosed(object sender, EventArgs e)
        {
            if (cbBoxMechanicForChaningErrands.IsDropDownOpen == false)
            {
                _choosenComboBoxMechanicObject = cbBoxMechanicForChaningErrands.SelectedItem as Mechanic;

                dgErrandOngoingAndDone.ItemsSource = ErrandList.ErrandsList.Where(x => x.ID == _choosenComboBoxMechanicObject.ErrandsID);
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
    }
}
