using Logic.Entities.Person_Entities;
using Logic.Entities.Vehicles_Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Logic
{
    public static class ErrandMechanicViewCombine
    {
        public static ObservableCollection<CommonView> _source { get; set; }

        public static ObservableCollection<CommonView> Source
        {
            get
            {
                if (_source == null)
                {
                    return _source = new ObservableCollection<CommonView>();
                }
                return _source;
            }
            set
            {
                _source = value;
            }
        }

        public static void BuildSource()
        {
            if (Source.Count != 0)
            {
                _source.Clear();
            }
            foreach (var errandItem in ErrandList.ErrandsList)
            {
                string mechanicName = "";
                Guid mechanicId = Guid.Empty;
                string vehicleType = VehicleType(errandItem.VehicleID);

                foreach (var item in MechanicList.MechanicLists)
                 {
                     foreach (var item2 in item.ErrandID)
                     {

                         if (item2 == errandItem.ID)
                         {
                            mechanicName = item.Name;
                            mechanicId = item.ID;
                         }
                     }

                 }
                _source.Add(new CommonView()
                {
                    ErrandID = errandItem.ID,
                    MechanicID = mechanicId,
                    VehicleID = errandItem.VehicleID,
                    Model = errandItem.ModelName,
                    RegistrationNumber = errandItem.RegistrationNumber,
                    Description = errandItem.Description,
                    Problem = errandItem.Problem,
                    Status = errandItem.Status, 
                    Name = mechanicName,
                    VehicleType = vehicleType
                });
            }
        }
        private static string VehicleType(Guid id)
        {
            string vehicleType = "";
            var obj = VehicleList.VehicleLists.Where(x => x.ID == id);

            foreach (var item in obj)
            {
                vehicleType = item.VehicleType;
            }
            return vehicleType;
        }
    }
}
