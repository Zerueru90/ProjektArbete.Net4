using Logic.Entities.Person_Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Logic.Entities.Vehicles_Entities
{
    public class Errand : ObservableObject
    {
        public Errand()
        {
            ID = Guid.NewGuid(); // För att varje gång ett nytt ärende skapas så ska en ny Guid skapas, FAST NÄR MAN UPPDATERAR SÅ SKA DENNA INTE GÖRAS, VILKET DEN INTE GÖR DÅ CTOR BARA GÅR IGENOM MED = new Errand();
        }

        public Guid ID { get; set; }
        public Guid VeichleID { get; set; } //När man sparar ett Ärende så måste man ha ett fordon och en MEKANIKER
        public Guid MechanicID { get; set; }
        public string ModelName { get; set; }
        public string RegistrationNumber { get; set; }

        public string Description { get; set; }
        public string Problem { get; set; }
        public string Mechanic { get; set; } //Denna finns så att man kan se vilken mekaniker det är som har jobbet, dock kaaanske det finns ett sätt att skapa en column och sätta värdet som Mekaniker och sen hämta namnet från MechanicList och jämnföra med MechanicID. Dock så räcker denna för tillfället, mycket mer kod annars.
        public string Status { get; set; }

        #region Detta måste göras så att WPFn håller sig uppdaterad på direkten. 
        
        public Guid ChangeVeichleID
        {
            get
            {
                if (VeichleID != Guid.Empty)
                {
                    return Guid.Empty;
                }
                return VeichleID;
            }
            set
            {
                VeichleID = value;
                NotifyPropertyChanged("VeichleID");
            }
        }

        public Guid ChangeMechanicID
        {
            get
            {
                if (MechanicID != Guid.Empty)
                {
                    return Guid.Empty;
                }
                return MechanicID;
            }
            set
            {
                MechanicID = value;
                NotifyPropertyChanged("MechanicID");
            }
        }

        public string ChangeModelName
        {
            get
            {
                if (string.IsNullOrEmpty(ModelName))
                {
                    return " ";
                }
                return ModelName;
            }
            set
            {
                ModelName = value;
                NotifyPropertyChanged("ModelName");
            }
        }

        public string ChangeRegistrationNumber
        {
            get
            {
                if (string.IsNullOrEmpty(RegistrationNumber))
                {
                    return " ";
                }
                return RegistrationNumber;
            }
            set
            {
                RegistrationNumber = value;
                NotifyPropertyChanged("RegistrationNumber");
            }
        }

        public string ChangeMechanic
        {
            get
            {
                if (string.IsNullOrEmpty(Mechanic))
                {
                    return " ";
                }
                return Mechanic;
            }
            set
            {
                Mechanic = value;
                NotifyPropertyChanged("Mechanic");
            }
        }

        public string ChangeDescription
        {
            get
            {
                if (string.IsNullOrEmpty(Description))
                {
                    return " ";
                }
                return Description;
            }
            set
            {
                Problem = value;
                NotifyPropertyChanged("Description");
            }
        }

        public string ChangeProblem
        {
            get
            {
                if (string.IsNullOrEmpty(Problem))
                {
                    return " ";
                }
                return Problem;
            }
            set
            {
                Problem = value;
                NotifyPropertyChanged("Problem");
            }
        }

        public string ChangeStatus 
        { 
            get
            {
                if (string.IsNullOrEmpty(Status))
                {
                    return " ";
                }
                return Status;
            }
            set
            {
                Status = value;
                NotifyPropertyChanged("Status");
            }
        }
        #endregion
    }
}
