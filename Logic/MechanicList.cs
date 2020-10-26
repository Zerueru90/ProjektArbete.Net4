using Logic.Entities.Person_Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Logic
{
    public static class MechanicList
    {
        static MechanicList()
        {
            _mechanicList = new List<Mechanic>();
        }
        
        public static List<Mechanic> _mechanicList { get ; set; }
    }
}
