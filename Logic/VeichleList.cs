using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public static class VeichleList
    {
        private static List<Vehicel> _veichleList;

        public static List<Vehicel> VeichleLists
        {
            get
            {
                if (_veichleList == null)
                {
                    return _veichleList = new List<Vehicel>();
                }
                return _veichleList;
            }
            set
            {
                _veichleList = value;
            }
        }
    }
}
