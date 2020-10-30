using Logic.Entities;
using Logic.Entities.Person_Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
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

        #region Tillfällig för att logga in enkelt.
        public static Mechanic Login(string username)
        {

            var obj = from mec in _mechanicList
                      where mec.Name == username
                      select mec;

            Mechanic mechanic = null;

            foreach (var item in obj)
            {
                mechanic = item;
            }

            return mechanic;
        }
        #endregion
    }
}
