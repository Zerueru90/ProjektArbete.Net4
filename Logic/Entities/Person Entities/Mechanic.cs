using Logic.Entities.Vehicles_Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logic.Entities.Person_Entities
{
    //Testar stuff..
    public class Mechanic : ObservableObject
    {
        public Mechanic()
        {
            ID = Guid.NewGuid(); // skriver med construktor för att den ska inte ta allt för mkt plats i minnet.
            MechanicProgressList = new List<string>();
            SkillLista = new List<string>();
            MechanicDoneList = new List<string>();
            ErrandsID = new List<Guid>();
        }
        public Guid ID { get; set; }

        public Guid UserID { get; set; } //Detta är för när mekaniker får en User log in.

        //Ska kunna hålla 2 IDn
        public List<Guid> ErrandsID { get; set; } 

        public string Name { get; set; }

        public DateTime DateOfBirthday { get; set; }

        public DateTime DateOfEmployment { get; set; }

        public DateTime? DateOfEnd { get; set; }

        public List<string> SkillLista { get; set; }

        public List<string> MechanicProgressList { get; set; }

        public List<string> MechanicDoneList { get; set; }

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
        #endregion
    }
}