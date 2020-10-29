﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logic.Entities.Person_Entities
{
    //Testar stuff..
    public class Mechanic : INotifyPropertyChanged
    {
        public Mechanic()
        {
            Id = Guid.NewGuid(); // skriver med construktor för att den ska inte ta allt för mkt plats i minnet. 
            MechanicProgressList = new List<string>();
            SkillLista = new List<string>();
        }

        public Guid Id { get; set; } 
        public string Name { get; set; }

        public DateTime DateOfBirthday { get; set; }

        public DateTime DateOfEmployment { get; set; }

        public DateTime? DateOfEnd { get; set; }

        public bool Breaks { get; set; }
        public bool Engine { get; set; }
        public bool Carbody { get; set; }
        public bool Windshield { get; set; }
        public bool Tyre { get; set; }   //fråga ? wtf ? 


        private List<string> _skillLista;

        public User IdentityUser { get; set; } // create login and password

        public List<string> SkillLista { get; set; }
        

        public List<string> MechanicProgressList { get; set; }
        public List<string> MechanicDoneList { get; set; }


        //Denna är för att varje gång vi gör några ändringar så kallas detta.
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propName = "")
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}