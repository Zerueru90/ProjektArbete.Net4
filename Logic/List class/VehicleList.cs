using Logic.Entities.Vehicles_Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Logic
{
    public static class VehicleList
    {
        private static ObservableCollection<Vehicle> _vehicleList;

        public static ObservableCollection<Vehicle> VehicleLists
        {
            get
            {
                if (_vehicleList == null)
                {
                    return _vehicleList = new ObservableCollection<Vehicle>();
                }
                return _vehicleList;
            }
            set
            {
                _vehicleList = value;
            }
        }
    }
}
