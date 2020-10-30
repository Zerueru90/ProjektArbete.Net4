using Logic;
using Logic.DAL;
using Logic.Entities;
using Logic.Entities.Person_Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for BosseHomePage.xaml
    /// </summary>
    public partial class BosseHomePage : Page
    {
        public BosseHomePage()
        {
            InitializeComponent();

            List<Mechanic> mechanics = new List<Mechanic>();

            foreach (var item in MechanicList._mechanicList)
            {
                mechanics.Add(new Mechanic() { Id = item.Id, Name = item.Name, DateOfBirthday = item.DateOfBirthday, DateOfEmployment = item.DateOfEmployment, DateOfEnd = item.DateOfEnd, SkillLista = item.SkillLista, ListofUsers = item.ListofUsers });
            }

            dgUserAccess.ItemsSource = mechanics;

            txtName.Text = "Lasse";
            txtEmployementday.Text = "20-10-24";
            txtBirthday.Text = "20-10-24";
            txtUnEnmploymentday.Text = "22-10-24";
        }
        private User _newUser;
        private Mechanic _mechanic;
        private CRUD _crud = new CRUD();

        private string _breakes = "Bromsar";
        private string _engine = "Motor";
        private string _carbody = "Kaross";
        private string _windshield = "Vindruta";
        private string _tyre = "Tyre";

        private void SkillCheck(CheckBox checkBox, string skill)
        {
            if (checkBox.IsChecked == true)
            {
                _mechanic.SkillLista.Add(skill);
            }
        }

        private void SkillCheck2(string skill)
        {
            if (skill == _breakes)
            {
                //Om strängen existerar så checkas boxen.
            }
            if (skill == _engine)
            {
                //Om strängen existerar så checkas boxen.
            }
            if (skill == _carbody)
            {
                //Om strängen existerar så checkas boxen.
            }
            if (skill == _windshield)
            {
                //Om strängen existerar så checkas boxen.
            }
            if (skill == _tyre)
            {
                //Om strängen existerar så checkas boxen.
            }
        }

        private IEnumerable<Mechanic> GetFromListBox(string selectedItem)
        {
            var mechanic = from mec in MechanicList._mechanicList
                           where mec.Name == selectedItem.ToString()
                           select mec;

            return mechanic;
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

                SkillCheck(checkBoxBreaks, _breakes);
                SkillCheck(checkBoxEngine, _engine);
                SkillCheck(checkBoxBody, _carbody);
                SkillCheck(checkBoxWindShield, _windshield);
                SkillCheck(checkBoxTyre, _tyre);

                _crud.AddMechanic(_mechanic);

                listBoxNewMechanic.Items.Add(_mechanic.Name);

                MessageBox.Show("Saved");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxNewMechanic.SelectedItem != null)
            {
                //Hellre vilja ha ID här men får fel meddelande för guid
                var mechanic = GetFromListBox(listBoxNewMechanic.SelectedItem.ToString());
                //_crud.RemoveMechanic(mechanic);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxNewMechanic.SelectedItem != null)
            {
                var mechanic = GetFromListBox(listBoxNewMechanic.SelectedItem.ToString());

                foreach (var item in mechanic)
                {
                    item.Name = txtName.Text;
                    item.DateOfBirthday = Convert.ToDateTime(txtBirthday.Text);
                    item.DateOfEmployment = Convert.ToDateTime(txtEmployementday.Text);
                    item.DateOfEnd = Convert.ToDateTime(txtUnEnmploymentday.Text);
                }
            }
        }

        private void listBoxNewMechanic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxNewMechanic.SelectedItem != null)
            {
                var mechanic = GetFromListBox(listBoxNewMechanic.SelectedItem.ToString());

                foreach (var item in mechanic)
                {
                    txtName.Text = item.Name;
                    txtBirthday.Text = Convert.ToString(item.DateOfBirthday);
                    txtEmployementday.Text = Convert.ToString(item.DateOfEmployment);
                    txtUnEnmploymentday.Text = Convert.ToString(item.DateOfEnd);

                    for (int i = 0; i < item.SkillLista.Count; i++)
                    {
                        SkillCheck2(item.SkillLista[i]);
                    }
                }
            }
        }

        private void BtnNewUser_Click(object sender, RoutedEventArgs e)
        {
           
            
                _newUser = new User()
                {
                    Username = txtUserName.Text,
                    Password = txtPassword.Text
                };
                _crud.AddUser(_newUser);
            
            
        }

        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
