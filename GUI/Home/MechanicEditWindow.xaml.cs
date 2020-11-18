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
    /// Interaction logic for MechanicEditWindow.xaml
    /// </summary>
    public partial class MechanicEditWindow : Window
    {
        private string[] _unwantedColumns = new string[]
        {
             "ID", "UserID", "ErrandID", "SkillLista"
        };

        public MechanicEditWindow(IEnumerable<Mechanic> mechanic)
        {
            InitializeComponent();
            //dgMechanicView.ItemsSource = MechanicList.MechanicLists; Denna funkar utan readonly
            //dgMechanicView.ItemsSource = MechanicList.MechanicLists.Where(x => x.ID == mechanic.ID);
            dgMechanicView.ItemsSource = mechanic;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
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

        private void dgMechanicView_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            CancelUnwantedColumnHeaderName(e);
        }
    }
}
