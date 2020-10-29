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
        public string Name { get; set; }

        public DateTime DateOfBirthday { get; set; }

        public DateTime DateOfEmployment { get; set; }

        public DateTime? DateOfEnd { get; set; }

        #region Alla bool är kanske tillfälligt, dom funkar bättre med WPF än List<string>. Med bool så får man checkbox i WPF. Man kan nog på något sätt kombinera dessa två att checka om de är sant eller falsk med listan. Men låt stå, WPF funkar.

        public bool Breaks { get; set; }
        public bool Engine { get; set; }
        public bool Carbody { get; set; }
        public bool Windshield { get; set; }
        public bool Tyre { get; set; }   //fråga ? wtf ?

        #endregion Alla bool är kanske tillfälligt, dom funkar bättre med WPF än List<string>. Med bool så får man checkbox i WPF. Man kan nog på något sätt kombinera dessa två att checka om de är sant eller falsk med listan. Men låt stå, WPF funkar.

        public User IdentityUser { get; set; } // create login and password

        public bool MechanicUser //Testar mig fram. om en mekaniker är en användare så blir checkboxen checkad. - Namnet är MechanicUser ändrar man här så måste man ändra på XAML
        {
            get
            {//När programet startar så kollar den om det finns en User och retunerar sant eller falsk.
                if (IdentityUser != null)
                {
                    return true;
                }
                else
                    return false;
            }
            set
            {
                NotifyPropertyChanged(); //När man sättar ÄrMekanikerAnvändare? till sant eller falsk så triggar denna.
            }
        }

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