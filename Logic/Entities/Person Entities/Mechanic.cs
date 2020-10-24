using System;
using System.Collections.Generic;

namespace Logic.Entities.Person_Entities
{
    public class Mechanic
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime DateOfBirthday { get; set; }

        public DateTime DateOfEmployment { get; set; }

        public DateTime? DateOfEnd { get; set; }

        public List<string> Skills { get; set; }

        public List<User> users { get; set; }


    }
}