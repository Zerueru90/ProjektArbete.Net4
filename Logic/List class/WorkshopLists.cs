using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Logic
{
    public static class WorkshopLists
    {
        private static ObservableCollection<Workshop> _workshopList;

        public static ObservableCollection<Workshop> WorkshopList
        {
            get
            {
                if (_workshopList == null)
                {
                    return _workshopList = new ObservableCollection<Workshop>();
                }
                return _workshopList;
            }
            set
            {
                _workshopList = value;
            }
        }
    }
}
