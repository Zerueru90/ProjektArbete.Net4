using Logic;
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
using System.Windows.Shapes;

namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for MechanicWindow.xaml
    /// </summary>
    public partial class MechanicWindow : Window
    {
        private Mechanic _currentMechanic;
        private CRUD _crud;
        private string[] _unwantedColumns = new string[]
        {
            "ChangeMechanicID", "ChangeModelName", "ChangeRegistrationNumber", "ChangeName", "ChangeDescription", "ChangeProblem", "ChangeStatus"
        };
        private string[] _skills = new string[]
        {
            "Breaks", "Engine", "Carbody", "Windshield", "Tyre"
        };

        public MechanicWindow(Mechanic currentMech)
        {
            InitializeComponent();

            _currentMechanic = currentMech;

            labelName.Content = currentMech.Name.ToString();

            dgErrends.ItemsSource = ErrandMechanicViewCombine.Source.Where(x => x.MechanicID == currentMech.ID);

            CheckingBoxes();
        }

        private void BtnUpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            _crud = new CRUD();
            if (dgErrends.SelectedItem != null)
            {
                var objCommonView = dgErrends.SelectedItem as CommonView;

                var test = ErrandMechanicViewCombine.Source.Where(x => x.ErrandID == objCommonView.ErrandID);
                var status = "";
                foreach (var item in test)
                {
                    status = item.Status;
                }

                if (status != "Klar")
                {
                    MechanicSkill.ChangeMechanicStatus(dgErrends.SelectedItem as CommonView, _currentMechanic, "Klar");
                }
                else
                    MessageBox.Show("Ärendet är klart och går inte att ändra.");


            }
        }
        private void BtnUpdateKompetens_Click(object sender, RoutedEventArgs e)
        {
            UpdatingBoxes();
            for (int i = 0; i < _skills.Length; i++)
            {
                _currentMechanic.NotifyPropertyChanged(_skills[i]);
            }
            MessageBox.Show("Uppdaterad");
        }

        //Finns säkert ett bättre sätt men detta funkar
        private void UpdatingBoxes()
        {
            if (checkBoxBreaks.IsChecked == true)
            {
                _currentMechanic.Breaks = true;
            }
            else
            {
                _currentMechanic.Breaks = false;
            }

            if (checkBoxEngine.IsChecked == true)
            {
                _currentMechanic.Engine = true;
            }
            else
            {
                _currentMechanic.Engine = false;
            }

            if (checkBoxCarbody.IsChecked == true)
            {
                _currentMechanic.Carbody = true;
            }
            else
            {
                _currentMechanic.Carbody = false;
            }

            if (checkBoxWindshield.IsChecked == true)
            {
                _currentMechanic.Windshield = true;
            }
            else
            {
                _currentMechanic.Windshield = false;
            }

            if (checkBoxTyre.IsChecked == true)
            {
                _currentMechanic.Tyre = true;
            }
            else
            {
                _currentMechanic.Tyre = false;
            }
        }

        private void CheckingBoxes()
        {
            if (_currentMechanic.Breaks == true)
            {
                checkBoxBreaks.IsChecked = true;
            }
            if (_currentMechanic.Engine == true)
            {
                checkBoxEngine.IsChecked = true;
            }
            if (_currentMechanic.Carbody == true)
            {
                checkBoxCarbody.IsChecked = true;
            }
            if (_currentMechanic.Windshield == true)
            {
                checkBoxWindshield.IsChecked = true;
            }
            if (_currentMechanic.Tyre == true)
            {
                checkBoxTyre.IsChecked = true;
            }
        }

        private void dgErrends_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
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
    }
}
