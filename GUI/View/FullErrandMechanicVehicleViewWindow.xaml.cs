﻿using Logic;
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

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for FullErrandMechanicVehicleViewWindow.xaml
    /// </summary>
    public partial class FullErrandMechanicVehicleViewWindow : Window
    {
        private CommonViewEMV _objCommonViewVE;
        private Vehicle _objVehicle;
        private CommonView _objCommonView;
        private Mechanic _objMechanic;
        private ICRUD _crud = new CRUD();
        private string[] _unwantedColumns = new string[]
        {
            "VehicleID", "MechanicID"
        };
        public FullErrandMechanicVehicleViewWindow(IEnumerable<CommonViewEMV> commonViewVE, Vehicle vehicle, CommonView commonView)
        {
            InitializeComponent();

            dgErrandEditDelete.ItemsSource = commonViewVE;
            _objVehicle = vehicle;
            _objCommonView = commonView;
            foreach (var item in commonViewVE)
            {
                _objCommonViewVE = item;
            }

            txtDescription.Text = _objCommonViewVE.Description;
            cbBoxProblemsErrand.SelectedItem = _objCommonViewVE.Problem;

            foreach (var item in Enum.GetValues(typeof(Enums.VehicelProblems)))
            {
                cbBoxProblemsErrand.Items.Add(item.ToString());
            }
        }
        private void FirstStepDoesErrandHaveMechanic()
        {
            var objMecID = _objCommonViewVE.MechanicID;
            var iEnumbleMec = MechanicList.MechanicLists.Where(x => x.ID == objMecID);
            Mechanic mec = null;
            foreach (var item in iEnumbleMec)
            {
                mec = item;
            }
            if (mec != null)
            {
                _objMechanic = mec;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            FirstStepDoesErrandHaveMechanic();
            if (_objCommonViewVE.Status != "Klar")
            {
                if (_objMechanic == null)
                {
                    _crud.UpdateErrand(_objCommonView, _objVehicle, txtDescription.Text, cbBoxProblemsErrand.SelectedItem.ToString());
                    ErrandMechanicViewCombine.BuildSource();
                    ErrandVehicleViewCombine.BuildSource();
                    MessageBox.Show("Uppdaterad");
                }
                else
                {
                    if (_objMechanic.SkillLista.Any(x => x == cbBoxProblemsErrand.SelectedItem.ToString()))
                    {
                        _crud.UpdateErrand(_objCommonView, _objVehicle, txtDescription.Text, cbBoxProblemsErrand.SelectedItem.ToString());
                        ErrandMechanicViewCombine.BuildSource();
                        ErrandVehicleViewCombine.BuildSource();
                        MessageBox.Show("Uppdaterad");
                    }
                    else
                        MessageBox.Show($"Den tilldelade mekanikern har inte kompetens {cbBoxProblemsErrand.SelectedItem.ToString()} ");
                }
            }
            else
                MessageBox.Show("Klargjorda ärenden går inte att ändra, om bilen måste lagas om igen så skapar du ett nytt ärende");
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            _crud.RemoveErrand(_objCommonView);
            this.Close();
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

        private void dgErrandEditDelete_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            CancelUnwantedColumnHeaderName(e);
        }
    }
}
