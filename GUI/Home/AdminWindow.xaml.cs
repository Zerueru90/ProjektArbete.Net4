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
            "SkillLista", "MechanicProgressList", "MechanicDoneList", "UserID", "ErrandID", "OngoingErrands", "finnishedErrands", "Isfinnished", "MechanicID", "VeichleID", "ChangeStatus", "ChangeVeichleID", "ChangeMechanicID", "ChangeMechanic", "ChangeDescription", "ChangeProblem", "ID", "ChangeModelName", "ChangeRegistrationNumber", "ChangeName"
        };
        public AdminWindow()
        {
            InitializeComponent();

            //Dessa är för att fylla vår datagrid
            dgUserAccess.ItemsSource = MechanicList.MechanicLists;
            dgMechanicList.ItemsSource = MechanicList.MechanicLists;
            dgVeichleList.ItemsSource = VehicleList.VehicleLists;
            dgErrandList.ItemsSource = ErrandMechanicViewCombine.Source;
            //dgCommonViewList.ItemsSource = ErrandMechanicViewCombine.Source;

            #region DummyData

            //DummyData.ErrandData();
            //DummyData.UserData();
            //DummyData.MecanichData();
            //if (VehicleList.VehicleLists.Count() == 0)
            //{
            //    DummyData.VehicleData();
            //}

            txtUserName.Text = "Lasse";

            txtName.Text = "Lasse";
            txtEmployementday.Text = "20-10-24";
            txtBirthday.Text = "20-10-24";
            txtUnEnmploymentday.Text = "22-10-24";

            txtDescription.Text = "blablablblablablblablablblablabl";
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
            //Ska dessa vara här?
            UpdateMechanicCheckBox();
            UpdateVechileCheckBox();
        }

        #region Mekaniker: Skapa/Radera/Uppdatera. Första sidan.
        private void BtnSaveNewMechanic_Click(object sender, RoutedEventArgs e)
        {
            if (!(txtName.Text == null) || !(txtName.Text == ""))
            {
                try
                {
                    _crud.CreateNewMechanic(txtName.Text, Convert.ToDateTime(txtBirthday.Text), Convert.ToDateTime(txtEmployementday.Text), Convert.ToDateTime(txtUnEnmploymentday.Text));
                    UpdateMechanicCheckBox();
                    MessageBox.Show("Saved");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Du måste skriva in ett giltigt datum");
                }
            }
            else
            {
                MessageBox.Show("Du måste ange ett namn");
            }
        }
        private void BtnDeleteMechanic_Click(object sender, RoutedEventArgs e)
        {
            if (dgMechanicList.SelectedItem != null)
            {
                _crud.RemoveMechanic(dgMechanicList.SelectedItem as Mechanic);
                MessageBox.Show("Raderat mekaniker");
            }
        }
        private void BtnUpdateMechanic_Click(object sender, RoutedEventArgs e)
        {
            if (dgMechanicList.SelectedItem != null)
            {
                _crud.UpdateMechanic(dgMechanicList.SelectedItem as Mechanic);
                MessageBox.Show("Uppdaterad");
            }
        }
        #endregion

        #region User: Skapa/Radera User inlogg. Andra sidan.
        private void BtnNewUser_Click(object sender, RoutedEventArgs e)
        {
            var isMatch = RegexValidation.VerifyEmail(txtUserName.Text) && RegexValidation.VerifyPassword(txtPassword.Text);
            if (isMatch)
            {

                _crud.CreateNewUser(dgUserAccess.SelectedItem as Mechanic, txtUserName.Text, txtPassword.Text);
                MessageBox.Show("Sparat ny användare");
            }
        }
        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (dgUserAccess.SelectedItem != null)
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
            string txt = "0";
            _crud.CreateNewVehicle(cbBoxVeichleType.SelectedItem.ToString(), txtModelName.Text, txtRegNr.Text.ToUpper(), Convert.ToDecimal(txtOdoMeter.Text), Convert.ToDateTime(txtRegDate.Text), cbBoxFuel.SelectedItem.ToString(), Convert.ToBoolean(checkBoxCarHook.IsChecked), Convert.ToDecimal(txt), Convert.ToInt32(txt));

            UpdateVechileCheckBox();
            MessageBox.Show("Sparad");
        }
        #endregion

        #region Ärenden: Skapa/Radera/Uppdatera. Tredje sidan. (Fattas dock att ärendet GRIDEN ska visa info om fordon, namn, reg osv.)
        private void BtnSaveErrand_Click(object sender, RoutedEventArgs e)
        {
            _crud.CreateNewErrand(cbBoxVeichlesErrand.SelectedItem as Vehicle, txtDescription.Text, cbBoxProblemsErrand.SelectedItem.ToString());
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
                _crud.UpdateErrand(dgErrandList.SelectedItem as CommonView, cbBoxVeichlesErrand.SelectedItem as Vehicle, txtDescription.Text, cbBoxProblemsErrand.SelectedItem.ToString());
                MessageBox.Show("Uppdaterad");
            }
        }
        #endregion

        #region Tilldelar en mekaniker ett ärende, kollar så att mekaniker inte har 2 pågående. Man kan även ändra status.
        private void BtnAppointMechanicErrand_Click(object sender, RoutedEventArgs e)
        {
            if (dgCommonViewList != null)
            {
                var trueorfalse = MechanicSkill.AddMechanicErrandList(cbBoxAppointMechanicAnErrand.SelectedItem as Mechanic, dgCommonViewList.SelectedItem as CommonView);

                if (trueorfalse)
                {
                    MessageBox.Show($"Mekanikern har blivit tilldelat ett ärende");
                }
                else
                    MessageBox.Show($"Mekanikern har redan två ärenden pågående");
            }
            else
                MessageBox.Show("Du måste välja något från den övre listan");
        }
        private void BtnChangesStatusOnly_Click(object sender, RoutedEventArgs e)
        {
            if (_choosenComboBoxMechanicObject != null)
            {
                var objCommonView = dgCommonViewList.SelectedItem as CommonView;
                var test = ErrandMechanicViewCombine.Source.Where(x => x.ErrandID == objCommonView.ErrandID);
                var status = "";
                foreach (var item in test)
                {
                    status = item.Status;
                }
                if (status != "Klar")
                {
                    var trueorfalse = MechanicSkill.ChangeMechanicStatus(dgCommonViewList.SelectedItem as CommonView, cbBoxAppointMechanicAnErrand.SelectedItem as Mechanic, "Klar");

                    if (trueorfalse)
                    {
                        MessageBox.Show("Uppdaterad");
                        dgCommonViewList.ItemsSource = null;
                    }
                    else
                        MessageBox.Show("Ingen mekaniker är tilldelat det ärendet");
                }
                else
                    MessageBox.Show("Ärendet är klart och går inte att ändra, skapa ett nytt ärende om fordonet måste repareras om igen");
            }
            else
                MessageBox.Show("Du måste välja något från den övre listan");

        }

        private void BtnChangeStatusMechanicHistoric_Click(object sender, RoutedEventArgs e)
        {
            if (_choosenComboBoxMechanicObject != null)
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
                    var trueorfalse = MechanicSkill.ChangeMechanicStatus(dgMechanicHistoric.SelectedItem as CommonView, cbBoxAppointMechanicAnErrand.SelectedItem as Mechanic, "Klar");

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
                    _choosenComboBoxMechanicObject = cbBoxAppointMechanicAnErrand.SelectedItem as Mechanic;
                    MechanicSkill.AddAndRemoveMechanicSkill(_choosenComboBoxMechanicObject);
                    List<CommonView> availableErrandList = new List<CommonView>();
                    List<CommonView> tempListHistoricErrand = new List<CommonView>();


                    List<CommonView> compatibleErrandList = null;
                    compatibleErrandList = CompatibleErrands(_choosenComboBoxMechanicObject);

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
                        else
                            availableErrandList.Add(_errand);
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
            }
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

