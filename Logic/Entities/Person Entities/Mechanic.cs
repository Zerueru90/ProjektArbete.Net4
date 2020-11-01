﻿using Logic.Entities.Vehicles_Entities;
using System;
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

        public Guid UserID { get; set; } //Detta är för när mekaniker får en User log in.

        public Guid ErrandsID { get; set; } 

        public string Name { get; set; }

        public DateTime DateOfBirthday { get; set; }

        public DateTime DateOfEmployment { get; set; }

        public DateTime? DateOfEnd { get; set; }

        #region Bool funkar bättre med WPF än List<string> som blir en collection i GUI. Med bool så får man checkbox i WPF. Man kan nog på något sätt kombinera dessa två att checka om de är sant eller falsk med listan. Men låt stå

        public bool Breaks { get; set; }
        public bool Engine { get; set; }
        public bool Carbody { get; set; }
        public bool Windshield { get; set; }
        public bool Tyre { get; set; }

        #endregion
        #region Liten prop som kollar om värdet i Guid UserID är tom eller inte. Denna jobbar bra med GUI och ger en check i GUI om det mekaniker har User access.
        public bool MechanicUser
        {
            get
            {//När programet startar så kollar den om det finns en User och retunerar sant eller falsk.

                if (UserID != Guid.Empty)
                {
                    return true;
                }
                else
                    return false;
            }
            set
            {

                NotifyPropertyChanged(); //viktig
            }
        }
        public void Testar(bool value)
        {
            MechanicUser = value;
        }
        #endregion

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