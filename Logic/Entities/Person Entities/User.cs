using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
    //Om Bosse väljer att ge en mekaniker admin privilegium, så går detta igenom.
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int MekanikerId { get; set; }
    }
}
