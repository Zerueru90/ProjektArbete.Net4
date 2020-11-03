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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for MechanichomePage.xaml
    /// </summary>
    public partial class MechanichomePage : Page
    {

        public Mechanic CurrentMechanic { get; set; }

        public MechanichomePage(Mechanic currentMech)
        {
            InitializeComponent();

            CurrentMechanic = currentMech;

            dgErrends.ItemsSource = ErrandList.ErrandsList.Where(x => x.MechanicID == CurrentMechanic.Id);

            foreach (var item in Enum.GetValues(typeof(Enums.VehicelStatus)))
            {
                comboBoxErrands.Items.Add(item.ToString());
            }
        }
        private void saveBtnClick(object sender, RoutedEventArgs e)
        {

        }

        private void dgErrends_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }
    }
}
