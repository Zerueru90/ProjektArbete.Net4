using Logic.Entities.Vehicles_Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Logic
{
    public static class ErrandList
    {
        private static ObservableCollection<Errand> _errandsList;

        public static ObservableCollection<Errand> ErrandsList
        {
            get
            {
                if (_errandsList == null)
                {
                    return _errandsList = new ObservableCollection<Errand>();
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
