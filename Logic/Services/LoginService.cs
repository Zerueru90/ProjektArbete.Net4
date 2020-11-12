using Logic.DAL;
using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Logic.Services
{
    public class LoginService
    {
        public bool Login(string username, string password)
        {
           var obj = UserList.UserLists.FirstOrDefault(user => user.Username.Equals(username) && user.Password.Equals(password));

            if (obj != null)
                return true;
            else
                return false;
        }
    }
}
