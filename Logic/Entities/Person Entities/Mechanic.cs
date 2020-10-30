using System;
using System.Collections.Generic;

namespace Logic.Entities.Person_Entities
{
    public class Mechanic
    {

        public Mechanic()
        {
            Id = Guid.NewGuid(); // skriver med construktor för att den ska inte ta allt för mkt plats i minnet. 
            MechanicProgressList = new List<string>();
        }

        public Guid Id { get; set; } 
        public string Name { get; set; }

        public DateTime DateOfBirthday { get; set; }

        public DateTime DateOfEmployment { get; set; }

        public DateTime? DateOfEnd { get; set; }

        private List<string> _skillLista;

        public List<User> ListofUsers { get; set; }

        public List<string> SkillLista
        {
            get
            {
                if (_skillLista == null)
                {
                    return _skillLista = new List<string>();
                }
                return _skillLista;
            }
            set
            {
                _skillLista = value;
            }

           
        }

        public List<string> MechanicProgressList { get; set; }
    }
}