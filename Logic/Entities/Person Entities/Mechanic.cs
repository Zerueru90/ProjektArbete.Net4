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
        }

        public Guid Id { get; set; } 
        public string Name { get; set; }

        public DateTime DateOfBirthday { get; set; }

        public DateTime DateOfEmployment { get; set; }

        public DateTime? DateOfEnd { get; set; }

        private List<string> _skillLista;

        public List<User> users { get; set; }

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


        //Denna är för att varje gång vi gör några ändringar så kallas detta.
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propName = "")
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}