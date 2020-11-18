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
                string MechanicName = "";
                Guid MechanicId = Guid.Empty;
                
                 foreach (var item in MechanicList.MechanicLists)
                 {
                     foreach (var item2 in item.ErrandID)
                     {

                         if (item2 == errandItem.ID)
                         {
                            MechanicName = item.Name;
                            MechanicId = item.ID;
                         }
                     }

                 }
                _source.Add(new CommonView()
                {
                    ErrandID = errandItem.ID,
                    MechanicID = MechanicId,
                    VehicleID = errandItem.VeichleID,
                    Modell = errandItem.ModelName,
                    Regnummer = errandItem.RegistrationNumber,
                    Beskrivning = errandItem.Description,
                    Problem = errandItem.Problem,
                    Status = errandItem.Status, 
                    Namn = MechanicName
                });
            }
        }
        
    }
}
