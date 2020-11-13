﻿using GUI.Home;
using Logic;
using Logic.DAL;
using Logic.Entities;
using Logic.Entities.Person_Entities;
using Logic.Entities.Vehicles_Entities;
using Logic.Services;
using System;
using System.Collections.Generic;
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

namespace GUI.Login
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private const string _errorMsg = "Inloggningen misslyckades";

        private LoginService _loginService;
        private AdminWindow adminWindow;
        private MechanicWindow mecWindow;

        public LoginPage()
        {
            InitializeComponent();

            DataAccessRead.ReadJsonFile();

            _loginService = new LoginService();

            txtBoxUserName.Text = "Bosse@hotmail.com";
            txtBoxPassword.Password = "Meckarn123";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = this.txtBoxUserName.Text;
            string password = this.txtBoxPassword.Password;
            
            bool successful = _loginService.LoginAdmin(username, password);
            
            if (successful && username == "Bosse@hotmail.com")
            {
                adminWindow = new AdminWindow();
                adminWindow.Show();
                adminWindow.Closing += delegate
                {
                    DataAccessWrite<Mechanic>.SaveData(MechanicList.MechanicLists);
                    DataAccessWrite<User>.SaveData(UserList.UserLists);
                    DataAccessWrite<Vehicle>.SaveData(VehicleList.VehicleLists);
                    DataAccessWrite<Errand>.SaveData(ErrandList.ErrandsList);
                };
            }

            bool successful1 = _loginService.LoginMec(username, password);

            if (successful1 && username != "Bosse@hotmail.com")
            {
                mecWindow = new MechanicWindow(_loginService.GetMechanicObj());
                mecWindow.Show();
                mecWindow.Closing += delegate
                {
                    DataAccessWrite<Mechanic>.SaveData(MechanicList.MechanicLists);
                };
            }
            else
            {
            
                MessageBox.Show(_errorMsg);
                this.txtBoxUserName.Clear();
                this.txtBoxPassword.Clear();
            }
        }
    }
}