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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for MechanichomePage.xaml
    /// </summary>
    public partial class MechanichomePage : Page
    {
        private Mechanic _currentMechanic;
        public MechanichomePage(Mechanic currentMech)
        {
            InitializeComponent();

            _currentMechanic = currentMech;

            labelName.Content = currentMech.Name.ToString();
            dgErrends.ItemsSource = ErrandList.ErrandsList.Where(x => x.MechanicID == currentMech.Id);

            foreach (var item in Enum.GetValues(typeof(Enums.VehicelStatus)))
            {
                comboBoxErrands.Items.Add(item.ToString());
            }
        }
        private void BtnUpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            Errand errand = dgErrends.SelectedItem as Errand;

            errand.ChangeStatus = comboBoxErrands.SelectedItem.ToString();

        }

        private void dgErrends_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();

            //Cancel the column you don't want to generate
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
    }
}
