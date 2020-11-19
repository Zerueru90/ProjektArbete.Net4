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
        public string VehicleType { get; set; }
        public string Model { get; set; }
        public string RegistrationNumber { get; set; }
        public string Description { get; set; }
        public string Problem { get; set; }
        public string Name { get; set; }
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
                if (string.IsNullOrEmpty(Model))
                {
                    return " ";
                }
                return Model;
            }
            set
            {
                Model = value;
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

        public string ChangeName
        {
            get
            {
                if (string.IsNullOrEmpty(Name))
                {
                    return " ";
                }
                return Name;
            }
            set
            {
                Name = value;
                NotifyPropertyChanged("Name");
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
    }
}
