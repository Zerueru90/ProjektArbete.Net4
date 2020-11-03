using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Logic
{
    public static class UserList
    {
        private static ObservableCollection<User> _userList;

        public static ObservableCollection<User> UserLists
        {
            get
            {
                if (_userList == null)
                {
                    return _userList = new ObservableCollection<User>();
                }
                return _userList;
            }
            set
            {
                _userList = value;
            }
        }
    }
}
