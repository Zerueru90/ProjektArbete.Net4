using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    //Denna klass är för GUI så att man ska kunna få fram lämplig info om ett ärende. Kombinerar båda Mekaniker och Ärende klasserna.
    public class CommonView : ObservableObject
    {
        public Guid ErrandID { get; set; }
        public Guid MechanicID { get; set; }
        public Guid VehicleID { get; set; }
        public string Modell { get; set; }
        public string Regnummer { get; set; }
        public string Beskrivning { get; set; }
        public string Problem { get; set; }
        public string Namn { get; set; }
        public string Status { get; set; }

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
                if (string.IsNullOrEmpty(Modell))
                {
                    return " ";
                }
                return Modell;
            }
            set
            {
                Modell = value;
                NotifyPropertyChanged("ModelName");
            }
        }

        public string ChangeRegistrationNumber
        {
            get
            {
                if (string.IsNullOrEmpty(Regnummer))
                {
                    return " ";
                }
                return Regnummer;
            }
            set
            {
                Regnummer = value;
                NotifyPropertyChanged("RegistrationNumber");
            }
        }

        public string ChangeName
        {
            get
            {
                if (string.IsNullOrEmpty(Namn))
                {
                    return " ";
                }
                return Namn;
            }
            set
            {
                Namn = value;
                NotifyPropertyChanged("Name");
            }
        }

        public string ChangeDescription
        {
            get
            {
                if (string.IsNullOrEmpty(Beskrivning))
                {
                    return " ";
                }
                return Beskrivning;
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
    }
}
