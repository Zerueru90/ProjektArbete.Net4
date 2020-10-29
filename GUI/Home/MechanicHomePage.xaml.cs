﻿using Logic;
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
        
        private Mechanic _mechanic;
        private CRUD _crud = new CRUD();

        private string _breakes = "Bromsar";
        private string _engine = "Motor";
        private string _carbody = "Kaross";
        private string _windshield = "Vindruta";
        private string _tyre = "Tyre";


        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            Mechanic mechanic = new Mechanic();
            var ID = mechanic.Id;
            txtName.Text = mechanic.Name;
            txtBirthday.Text = DateTime.Now.ToString();
            txtEmployementday.Text = DateTime.Now.ToString();
            txtUnEnmploymentday.Text = DateTime.Now.ToString();
            SkillCheck(Checkboxbreaks, _breakes);
            SkillCheck(CheckboxEngien, _engine);
            SkillCheck(CheckboxBody, _carbody);
            SkillCheck(CheckboxTyre, _tyre);
            SkillCheck(CheckboxWindsheild, _windshield);
           
            _crud.AddMechanic(mechanic);
            MessageBox.Show("Mekaniker skapad");
                
        }
        private void SkillCheck(CheckBox checkBox, string skill)
        {
            if (checkBox.IsChecked == true)
            {
                _mechanic.SkillLista.Add(skill);
                SetSkill(skill);
            }
        }
        private void SetSkill(string skill)
        {
            if (skill == "Bromsar")
            {
                _mechanic.Breaks = true;
            }
            if (skill == "Motor")
            {
                _mechanic.Engine = true;
            }
            if (skill == "Kaross")
            {
                _mechanic.Carbody = true;
            }
            if (skill == "Vindruta")
            {
                _mechanic.Windshield = true;
            }
            if (skill == "Tyre")
            {
                _mechanic.Tyre = true;
            }
        }

    }
}
