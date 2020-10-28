using Logic.Entities.Person_Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Text;

namespace Logic
{
    public static class MechanicList
    {
        static MechanicList()
        {
            _mechanicList = new ObservableCollection<Mechanic>();
        }
        
        public static ObservableCollection<Mechanic> _mechanicList { get ; set; }
    }
}
