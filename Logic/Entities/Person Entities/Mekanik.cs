using System;
using System.Collections.Generic;

namespace Logic.Entities.Person_Entities
{
    public class Mekanik
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime DateOfBirthday { get; set; }

        public DateTime dateOfEnd { get; set; }

        public List<string> Skills { get; set; }

        public List<User> users { get; set; }
    }
}