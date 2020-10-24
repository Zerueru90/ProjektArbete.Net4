using Logic.DAL;
using Logic.Entities.Person_Entities;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for BosseHomePage.xaml
    /// </summary>
    public partial class BosseHomePage : Page
    {
        public BosseHomePage()
        {
            InitializeComponent();

            txtName.Text = "Lasse";
            txtEmployementday.Text = "20-10-24";
            txtBirthday.Text = "20-10-24";
            txtUnEnmploymentday.Text = "22-10-24";
        }

        private Mechanic _mechanic;

        private string _breakes = "Bromsar";
        private string _engine = "Motor";
        private string _carbody = "Kaross";
        private string _windshield = "Vindruta";
        private string _tyre = "Däck";

        private void SkillCheck(CheckBox checkBox, string skill)
        {
            if (checkBox.IsChecked == true)
            {
                _mechanic.SkillLista.Add(skill);
            }
        }

        private void BtnSaveNewMechanic(object sender, RoutedEventArgs e)
        {
            _mechanic = new Mechanic();
            _mechanic.Name = txtName.Text;
            _mechanic.DateOfBirthday = Convert.ToDateTime(txtBirthday.Text);
            _mechanic.DateOfEmployment = Convert.ToDateTime(txtEmployementday.Text);
            _mechanic.DateOfEnd = Convert.ToDateTime(txtUnEnmploymentday.Text);

            SkillCheck(checkBoxBreaks, _breakes);
            SkillCheck(checkBoxEngine, _engine);
            SkillCheck(checkBoxBody, _carbody);
            SkillCheck(checkBoxWindShield, _windshield);
            SkillCheck(checkBoxTyre, _tyre);

            MechanicDataAccess.SaveNewMechanicData(_mechanic);

            listBoxNewMechanic.Items.Add(MechanicDataAccess.LoadMechanics());

            MessageBox.Show("Saved");
        }

        private void BtnSaveNewUser(object sender, ContextMenuEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
