using Logic;
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
    /// Interaction logic for MechanicHomePage.xaml
    /// </summary>
    public partial class MechanicHomePage : Page
    {
        public MechanicHomePage()
        {
            InitializeComponent();
        }

       

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            Mechanic mechanic = new Mechanic();
            var ID = mechanic.Id;
            txtName.Text = mechanic.Name;
            txtBirthday.Text = DateTime.Now.ToString();
            txtEmployementday.Text = DateTime.Now.ToString();
            txtUnEnmploymentday.Text = DateTime.Now.ToString();


            MechanicList._mechanicList.Add(mechanic);
                
        }
        
    }
}
