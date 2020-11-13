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
        public bool LoginAdmin(string username, string password)
        {
           var obj = UserList.UserLists.FirstOrDefault(user => user.Username.Equals(username) && user.Password.Equals(password));

            if (obj != null)
                return true;
            else
                return false;
        }



        private Mechanic Mechanic;
        public bool LoginMec(string username, string password)
        {
            var obj = UserList.UserLists.FirstOrDefault(user => user.Username.Equals(username) && user.Password.Equals(password));

            var obj1 = MechanicList.MechanicLists.Where(x => x.UserID == obj.ID).Select(x => x);
            

            foreach (var item in obj1)
            {
                Mechanic = item;
            }

            if (obj != null)
                return true;
            else
                return false;
        }

        #region Tillfällig för att logga in enkelt.
        public Mechanic GetMechanicObj()
        {
            return Mechanic;
        }
        #endregion
    }
}
