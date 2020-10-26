using Logic.Entities.Person_Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Logic
{
    public static class MechanicList
    {
        private static List<Mechanic> _mechanics;

        public static List<Mechanic> AddToMechanicList
        {
            get
            {
                if (_mechanics == null)
                {
                    return _mechanics = new List<Mechanic>();
                }
                return _mechanics;
            }
            set
            {
                _mechanics = value;
            }
        }

    }
}
