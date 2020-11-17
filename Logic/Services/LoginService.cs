using Logic.DAL;
using Logic.Entities;
using Logic.Entities.Person_Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Logic.Services
{
    public class LoginService
    {
        private BosseDataAccess _db;
        private Mechanic Mechanic;

        public LoginService()
        {
            _db = new BosseDataAccess();
        }

        public bool LoginBosse(string username, string password)
        {
            List<User> users = _db.GetUsers();

            return users.Exists(user => user.Username.Equals(username) && user.Password.Equals(password));
        }

        public bool LoginMec(string username, string password)
        {
            var obj = UserList.UserLists.FirstOrDefault(user => user.Username.Equals(username) && user.Password.Equals(password));

            if (obj != null)
            {
                var obj1 = MechanicList.MechanicLists.Where(x => x.UserID == obj.ID).Select(x => x);

                foreach (var item in obj1)
                {
                    Mechanic = item;
                }
                return true;
            }
            else
                return false;
        }

        public Mechanic GetMechanicObj()
        {
            return Mechanic;
        }
    }
}
/*
    var ojbUser = from list in UserList.UserLists
                          where list.Username == username && list.Password == password
                          select list.ID;
            if (ojbUser != null)
            {
                foreach (var key in ojbUser)
                {
                    var obj1 = MechanicList.MechanicLists.Where(x => x.UserID == key).Select(x => x);
                    foreach (var item in obj1)
                    {
                        Mechanic = item;
                    }
                }
                return true;
            }
            else
                return false; 

 */
