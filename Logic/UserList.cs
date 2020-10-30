using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public static class UserList
    {
        private static List<User> _userList;

        public static List<User> UserLists
        {
            get
            {
                if (_userList == null)
                {
                    return _userList = new List<User>();
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
