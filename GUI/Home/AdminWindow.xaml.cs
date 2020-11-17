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
using System.Windows.Shapes;

namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private ICRUD _crud = new CRUD();
        private Mechanic _choosenComboBoxMechanicObject;
        private string[] _unwantedColumns = new string[]
        {
            "SkillLista", "MechanicProgressList", "MechanicDoneList", "UserID", "ErrandID", "OngoingErrands", "finnishedErrands", "Isfinnished", "MechanicID", "VehicleID", "ChangeStatus", "ChangeVeichleID", "ChangeMechanicID", "ChangeMechanic", "ChangeDescription", "ChangeProblem", "ID", "ChangeModelName", "ChangeRegistrationNumber", "ChangeName"
        };
        public AdminWindow()
        {
            InitializeComponent();

            txtModelName.Text = "Volvo";
            txtOdoMeter.Text = "2000";
            txtRegDate.Text = DateTime.Now.ToString();

            //Dessa är för att fylla vår datagrid
            dgUserAccess.ItemsSource = MechanicList.MechanicLists;
            dgMechanicList.ItemsSource = MechanicList.MechanicLists;
            dgVeichleList.ItemsSource = VehicleList.VehicleLists;
            dgErrandList.ItemsSource = ErrandMechanicViewCombine.Source;

            #region DummyData

            txtUserName.Text = "Lasse";

            txtName.Text = "Lasse";
            txtEmployementday.Text = "20-10-24";
            txtBirthday.Text = "20-10-24";
            txtUnEnmploymentday.Text = "22-10-24";

            txtDescription.Text = "Jag har problem med min bil";
            #endregion

            //Viktig, denna fyller i comboboxen.
            foreach (var item in Enum.GetValues(typeof(Enums.VeichleType)))
            {
                cbBoxVeichleType.Items.Add(item.ToString());
            }
            foreach (var item in Enum.GetValues(typeof(Enums.VehicelProblems)))
            {
                cbBoxProblemsErrand.Items.Add(item.ToString());
            }
            foreach (var item in Enum.GetValues(typeof(Enums.FuelType)))
            {
                cbBoxFuel.Items.Add(item.ToString());
            }
            //Ser till att checkboxen är i fyllda
            UpdateMechanicCheckBox();
            UpdateVechileCheckBox();
        }

        #region Mekaniker: Skapa/Radera/Uppdatera. Första sidan.
        private void BtnSaveNewMechanic_Click(object sender, RoutedEventArgs e)
        {
            // Mekaniker namn, födelsedatum, Datumföranställning och slutdatum för anställning.
            try
            {
                RegexValidation.LettersOnly(txtName.Text);
                _crud.CreateNewMechanic(txtName.Text, Convert.ToDateTime(txtBirthday.Text), Convert.ToDateTime(txtEmployementday.Text), Convert.ToDateTime(txtUnEnmploymentday.Text));
                UpdateMechanicCheckBox();
                MessageBox.Show("Saved");
            }
            catch (FormatException)
            {
                MessageBox.Show("Du måste skriva in ett giltigt datum");
            }
            catch (ExceptionHandling.NameFormatException name)
            {
                MessageBox.Show(name.ToString());
            }
            catch (ExceptionHandling.EmptyTextBoxException text)
            {
                MessageBox.Show(text.ToString());
            }
            NullDataGrids();
        }
        private void BtnDeleteMechanic_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDataGridDoesNotEqualNull(dgMechanicList))
            {
                var obj = dgMechanicList.SelectedItem as Mechanic;
                _crud.RemoveMechanic(dgMechanicList.SelectedItem as Mechanic);
                UpdateMechanicCheckBox();
                RemoveTraceOfMechanicInErrand(obj.ID);
                //UpdateDataGrid();
                MessageBox.Show("Raderat mekaniker");
            }
            NullDataGrids();
        }
        private void BtnUpdateMechanic_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDataGridDoesNotEqualNull(dgMechanicList))
            {
                _crud.UpdateMechanic(dgMechanicList.SelectedItem as Mechanic);
                MessageBox.Show("Uppdaterad");
            }
            NullDataGrids();
        }
        private void RemoveTraceOfMechanicInErrand(Guid mecID)
        {
            var obj = ErrandList.ErrandsList.Any(x => x.MechanicID == mecID);
            if (obj == true)
            {
                var obj2 = ErrandList.ErrandsList.Where(x => x.MechanicID == mecID);
                var objSource = ErrandMechanicViewCombine.Source.FirstOrDefault(x => x.MechanicID == mecID);
                foreach (var item in obj2)
                {
                    item.MechanicID = Guid.Empty;
                }
                objSource.MechanicID = Guid.Empty;
            }
            dgErrandList.ItemsSource = null; ;
            dgErrandList.ItemsSource = ErrandMechanicViewCombine.Source;
        }
        private void NullDataGrids()
        {
            dgCommonViewList.ItemsSource = null;
            dgMechanicHistoric.ItemsSource = null;
            cbBoxAppointMechanicAnErrand.SelectedItem = null;
        }
        #endregion

        #region User: Skapa/Radera User inlogg. Andra sidan.
        private void BtnNewUser_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDataGridDoesNotEqualNull(dgUserAccess))
            {
                try
                {
                    RegexValidation.CheckForEmail(txtUserName.Text);
                    RegexValidation.VerifyPassword(txtPassword.Text);
                    _crud.CreateNewUser(dgUserAccess.SelectedItem as Mechanic, txtUserName.Text, txtPassword.Text);
                    MessageBox.Show("Sparat ny användare");
                }
                catch (ExceptionHandling.EmailFormatException email)
                {
                    MessageBox.Show(email.ToString());
                }
                catch (ExceptionHandling.PasswordFormat password)
                {
                    MessageBox.Show(password.ToString());
                }
                catch (ExceptionHandling.EmptyTextBoxException text)
                {
                    MessageBox.Show(text.ToString());
                }
            }
        }
        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDataGridDoesNotEqualNull(dgUserAccess))
            {
                //Raderar inte själva mekanikern men raderar användarnamn och lösenord som den ska
                _crud.RemoveUser(dgUserAccess.SelectedItem as Mechanic);
                MessageBox.Show("Raderat användare");
            }
        }
        #endregion

        #region Fordon: Skapa. har en metod för upprepande kod. Tredje sidan. (Antar att radera ej behövs, man ska ha koll på alla klargjorda fordon)
        private void BtnSaveVeichle_Click(object sender, RoutedEventArgs e)
        {
            if (cbBoxVeichleType.SelectedItem != null && cbBoxFuel.SelectedItem != null)
            {
                var objreg = txtRegNr.Text.ToUpper();
                var objVehicleList = VehicleList.VehicleLists.Any(x => x.RegistrationNumber == objreg);

                if (objVehicleList == false)
                {
                    try
                    {
                        RegexValidation.LettersOnly(txtModelName.Text);
                        RegexValidation.NumberOnly(txtOdoMeter.Text);
                        RegexValidation.RegistrationNumber(txtRegNr.Text);

                        switch (cbBoxVeichleType.SelectedItem.ToString())
                        {
                            case "Bil":
                                Car _newCar = new Car();
                                if (cbBoxCarType.SelectedItem == null)
                                {
                                    RegexValidation.IsNullorEmpty("");
                                }
                                _newCar.Cartype = cbBoxCarType.SelectedItem.ToString();
                                if (checkBoxCarHook.IsChecked == true)
                                {
                                    _newCar.HasTowbar = true;
                                }
                                _crud.CreateNewVehicle(_newCar, txtModelName.Text, txtRegNr.Text.ToUpper(), Convert.ToDecimal(txtOdoMeter.Text), Convert.ToDateTime(txtRegDate.Text), cbBoxFuel.SelectedItem.ToString(), cbBoxVeichleType.SelectedItem.ToString());
                                break;

                            case "Motorcyckel":
                                Motorbike _newMotorbike = new Motorbike();
                                _crud.CreateNewVehicle(_newMotorbike, txtModelName.Text, txtRegNr.Text.ToUpper(), Convert.ToDecimal(txtOdoMeter.Text), Convert.ToDateTime(txtRegDate.Text), cbBoxFuel.SelectedItem.ToString(), cbBoxVeichleType.SelectedItem.ToString());
                                break;

                            case "Lastbil":
                                Truck _newTruck = new Truck();
                                RegexValidation.MaxLoadWeight(txtMaxLoadWeight.Text);
                                _newTruck.MaxLoadWeight = Convert.ToDecimal(txtMaxLoadWeight.Text);
                                _crud.CreateNewVehicle(_newTruck, txtModelName.Text, txtRegNr.Text.ToUpper(), Convert.ToDecimal(txtOdoMeter.Text), Convert.ToDateTime(txtRegDate.Text), cbBoxFuel.SelectedItem.ToString(), cbBoxVeichleType.SelectedItem.ToString());
                                break;

                            case "Buss":
                                Bus _newBus = new Bus();
                                RegexValidation.MaxPassangers(txtMaxPassanger.Text);
                                _newBus.MaxTotalPassengers = Convert.ToInt32(txtMaxPassanger.Text);
                                _crud.CreateNewVehicle(_newBus, txtModelName.Text, txtRegNr.Text.ToUpper(), Convert.ToDecimal(txtOdoMeter.Text), Convert.ToDateTime(txtRegDate.Text), cbBoxFuel.SelectedItem.ToString(), cbBoxVeichleType.SelectedItem.ToString());
                                break;

                            default:
                                break;
                        }

                        UpdateVechileCheckBox();
                        MessageBox.Show("Sparad");
                    }
                    catch (ExceptionHandling.NumbersOnlyException numb)
                    {
                        MessageBox.Show(numb.ToString());

                    }
                    catch (ExceptionHandling.NameFormatException name)
                    {
                        MessageBox.Show(name.ToString());
                    }
                    catch (ExceptionHandling.EmptyTextBoxException empty)
                    {
                        MessageBox.Show(empty.ToString());
                    }
                    catch (ExceptionHandling.RegNumberException regnum)
                    {
                        MessageBox.Show(regnum.ToString());
                    }
                    catch (ExceptionHandling.MaxLoadWeight deci)
                    {
                        MessageBox.Show(deci.ToString());
                    }
                    catch (ExceptionHandling.MaxPassangersException max)
                    {
                        MessageBox.Show(max.ToString());
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Du måste skriva in ett giltigt datum");
                    }
                }
                else
                    MessageBox.Show($"Ett fordon med reg nr {objreg} finns redan.");
            }
            else
                MessageBox.Show("Du måste fylla i alla uppgifter");

        }
        #endregion

        #region Ärenden: Skapa/Radera/Uppdatera. Tredje sidan. (Fattas dock att ärendet GRIDEN ska visa info om fordon, namn, reg osv.)
        private void BtnSaveErrand_Click(object sender, RoutedEventArgs e)
        {
            if (cbBoxVeichlesErrand.SelectedItem != null && cbBoxProblemsErrand.SelectedItem != null)
            {
                try
                {
                    _crud.CreateNewErrand(cbBoxVeichlesErrand.SelectedItem as Vehicle, txtDescription.Text, cbBoxProblemsErrand.SelectedItem.ToString());
                    MessageBox.Show("Sparad");
                    NullDataGrids();
                }
                catch (NullReferenceException empty)
                {
                    MessageBox.Show(empty.ToString());
                }
            }
            else
                MessageBox.Show("Du måste fylla i alla uppgifter");

        }
        private void BtnDeleteErrand_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDataGridDoesNotEqualNull(dgErrandList))
            {
                _crud.RemoveErrand(dgErrandList.SelectedItem as CommonView);
            }
            NullDataGrids();
        }
        private bool CheckDataGridDoesNotEqualNull(DataGrid dataGrid)
        {
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Du måste välja något ifrån listan");
                return false;
            }
            return true;
        }
        private void BtnUpdateErrand_Click(object sender, RoutedEventArgs e)
        {
            if (cbBoxVeichlesErrand.SelectedItem != null && cbBoxProblemsErrand.SelectedItem != null)
            {
                var objVehicle = cbBoxVeichlesErrand.SelectedItem as Vehicle;

                var obj = ErrandMechanicViewCombine.Source.Where(x => x.VehicleID == objVehicle.ID);
                foreach (var item in obj)
                {
                    if (item.Status != "Klar")
                    {
                        if (item.VehicleID != Guid.Empty)
                        {
                            _crud.UpdateErrand(item, objVehicle, txtDescription.Text, cbBoxProblemsErrand.SelectedItem.ToString());
                            MessageBox.Show("Uppdaterad");
                        }
                        else
                            MessageBox.Show("Finns inget ärende att uppdatera, välj rätt reg nr.");
                    }
                    else
                        MessageBox.Show("Ärendet är klart, går inte att ändra");
                }
                NullDataGrids();
            }
            else
                MessageBox.Show("Måste välja ett reg nr och problem för att uppdatera");
        }
        #endregion

        #region Tilldelar en mekaniker ett ärende, kollar så att mekaniker inte har 2 pågående. Man kan även ändra status.
        private void BtnAppointMechanicErrand_Click(object sender, RoutedEventArgs e)
        {
            Guid guid = Guid.Empty;

            if (dgCommonViewList.SelectedItem != null && cbBoxAppointMechanicAnErrand.SelectedItem != null)
            {
                var objChosenMechanic = cbBoxAppointMechanicAnErrand.SelectedItem as Mechanic;
                var obj = dgCommonViewList.SelectedItem as CommonView;
                guid = obj.MechanicID;

                var trueorfalse = MechanicSkill.AddMechanicErrandList(cbBoxAppointMechanicAnErrand.SelectedItem as Mechanic, dgCommonViewList.SelectedItem as CommonView);

                if (trueorfalse)
                {
                    //Om Bosse väljer att ge en mekaniker ett ärende som en annan mekaniker har och är pågående, så måste den gamla mekanikers progresslista ta bort 1 ärende.
                    if (objChosenMechanic.ID != guid)
                    {
                        var objMechanic = MechanicList.MechanicLists.Where(x => x.ID == guid);
                        foreach (var oldMechanic in objMechanic)
                        {
                            MechanicSkill.RemoveFromMechanicProgressList(oldMechanic, obj.ErrandID.ToString());
                        }
                    }

                    UpdateDataGrid();
                    MessageBox.Show($"Mekanikern har blivit tilldelat ett ärende");
                }
                else
                    MessageBox.Show($"Mekanikern har redan två ärenden pågående");
            }
            else
                MessageBox.Show("Du har antingen glömt välja en mekaniker eller ett ärende");
        }
        private void BtnChangeStatusMechanicHistoric_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDataGridDoesNotEqualNull(dgMechanicHistoric))
            {
                var objCommonView = dgMechanicHistoric.SelectedItem as CommonView;
                var test = ErrandMechanicViewCombine.Source.Where(x => x.ErrandID == objCommonView.ErrandID);
                var status = "";
                foreach (var item in test)
                {
                    status = item.Status;
                }
                if (status != "Klar")
                {
                    var trueorfalse = MechanicSkill.ChangeMechanicStatus(dgMechanicHistoric.SelectedItem as CommonView, _choosenComboBoxMechanicObject, "Klar");

                    if (trueorfalse)
                    {
                        MessageBox.Show("Uppdaterad");
                    }
                    else
                        MessageBox.Show("Ingen mekaniker är tilldelat det ärendet");
                }
                else
                    MessageBox.Show("Ärendet är klart och går inte att ändra, skapa ett nytt ärende om fordonet måste repareras om igen");
            }
            else
                MessageBox.Show("Du måste välja något från Historik listan");
        }
        #endregion

        #region Uppdaterar Checkbox: kallas när man skapar något nytt i.e Mekaniker, Fordon
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

        #region OnDropDownClosed
        void cbBoxAppointMechanicAnErrand_OnDropDownClosed(object sender, EventArgs e)
        {
            dgCommonViewList.ItemsSource = null;

            if (cbBoxAppointMechanicAnErrand.SelectedItem != null)
            {
                if (cbBoxAppointMechanicAnErrand.IsDropDownOpen == false)
                {
                    UpdateDataGrid();
                }
            }
        }

        private void UpdateDataGrid()
        {
            _choosenComboBoxMechanicObject = cbBoxAppointMechanicAnErrand.SelectedItem as Mechanic;
            if (_choosenComboBoxMechanicObject != null)
                MechanicSkill.AddAndRemoveMechanicSkill(_choosenComboBoxMechanicObject);

            List<CommonView> availableErrandList = new List<CommonView>();
            List<CommonView> tempListHistoricErrand = new List<CommonView>();

            List<CommonView> compatibleErrandList = CompatibleErrands(_choosenComboBoxMechanicObject);

            //Seperar pågående och klara ärenden med tillgängliga äreden.
            foreach (var _errand in compatibleErrandList)
            {
                if (_errand.MechanicID == _choosenComboBoxMechanicObject.ID)
                {
                    if (_errand.Status == "Pågående" || _errand.Status == "Klar")
                    {
                        tempListHistoricErrand.Add(_errand);
                    }
                    else
                        availableErrandList.Add(_errand);
                }
                else if (_errand.MechanicID != _choosenComboBoxMechanicObject.ID && _errand.Status != "Klar")
                {
                    availableErrandList.Add(_errand);
                }
            }

            //Här nedan dubbelkollar vi om mekanikern redan ett pågående ärende med "gammal" kompetens så ska den endå dyka upp i historik.
            List<CommonView> ghostedErrandList = null;
            ghostedErrandList = GhostedErrands(_choosenComboBoxMechanicObject);

            foreach (var item in ghostedErrandList)
            {
                tempListHistoricErrand.Add(item);
            }

            dgCommonViewList.ItemsSource = availableErrandList;
            dgMechanicHistoric.ItemsSource = tempListHistoricErrand;
        }

        private List<CommonView> CompatibleErrands(Mechanic mechanic)
        {
            IEnumerable<CommonView> commonView = null;
            List<CommonView> tempListErrand = new List<CommonView>();

            //Gå igenom hela Errandlistan efter kompatibla kompetenser och lägg i lista.
            foreach (var item in _choosenComboBoxMechanicObject.SkillLista)
            {
                commonView = ErrandMechanicViewCombine.Source.Where(x => x.Problem == item);

                foreach (var _errand in commonView)
                {
                    tempListErrand.Add(_errand);
                }
            }

            return tempListErrand;
        }

        private List<CommonView> GhostedErrands(Mechanic mechanic)
        {
            List<CommonView> ghostedErrands = new List<CommonView>();
            List<CommonView> unCompatibleProblem = new List<CommonView>();

            foreach (var item in ErrandMechanicViewCombine.Source)
            {
                var objrev = mechanic.SkillLista.Any(x => x.Contains(item.Problem));

                if (objrev == false)
                {
                    unCompatibleProblem.Add(item);
                }
            }

            var obj = unCompatibleProblem.Where(x => x.MechanicID == mechanic.ID);

            foreach (var item in obj)
            {
                ghostedErrands.Add(item);
            }

            return ghostedErrands;
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

        #region Kolumer: Tar bort dom man inte vill ha med.

        private void CancelUnwantedColumnHeaderName(DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();

            //Tar bort kolumerna man inte vill ha med

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

        private void dgCommonView_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
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

