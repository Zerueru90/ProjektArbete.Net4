using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
    //Denna ska sparas i User.json.
    //Om Bosse väljer att ge en mekaniker admin privilegium, så går detta igenom.
    public class User
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Password { get; set; }

        public enum Priviliges
        {
            Admin = 1,
            User = 2,
        }
        //public Guid MekanikerId { get; set; }
    }
}
