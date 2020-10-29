using Logic.Entities.Vehicles_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public static class ErrandList
    {
        private static List<Errands> _errandsList;

        public static List<Errands> ErrandsList
        {
            get
            {
                if (_errandsList == null)
                {
                    return _errandsList = new List<Errands>();
                }
                return _errandsList;
            }
            set
            {
                _errandsList = value;
            }
        }
    }
}
