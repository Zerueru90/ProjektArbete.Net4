using Logic.Entities.Vehicles_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public static class VehicleList
    {
        private static List<Vehicle> _vehicleList;

        public static List<Vehicle> VehicleLists
        {
            get
            {
                if (_vehicleList == null)
                {
                    return _vehicleList = new List<Vehicle>();
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
