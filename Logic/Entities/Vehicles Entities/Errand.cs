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
        public Guid ErrandsID { get; set; } = Guid.NewGuid();
        public Guid VeichleID { get; set; } //När man sparar ett Ärende så måste man ha ett fordon och en MEKANIKER
        public Guid MechanicID { get; set; }
        public string Mechanic { get; set; }
        public string Description { get; set; }
        public string Problem { get; set; }
        public string Status { get; set; }

        #region Detta måste göras (finns säkert ett bättre sätt) så att WPFn håller sig uppdaterad på direkten. 
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


        //public List<Errand> OngoingErrands { get; set; }
        //public List<Errand> finnishedErrands { get; set; }
        //public bool Isfinnished { get; set; }

        //public bool TryToAdd(Vehicle vehicles)
        //{
        //    var counter = 0;
        //    if (counter <= 2)
        //    {
        //        counter++;
        //        return true;
        //    }
        //    else
        //    {
        //        //MessageBox.Show("Tyvärr kan en makaniker endast ha två ärenden samtidigt");
        //        return false;
        //    }
        //}
        //public bool AddErrands(Vehicle vehicles)
        //{
        //    var isOktoAdd = TryToAdd(vehicles);
        //    if (isOktoAdd)
        //    {
        //        OngoingErrands.Add((Errand)vehicles);
        //        return true;
        //    }
        //    else
        //        return false;
        //}
        ////public string GetinfoOfErrand()
        ////{
        ////    //return $"Beskrvining: {Description}" +
        ////    //    $"\nProblem: {Problem}" +
        ////    //    $"\nMekaniker: {Mechanic}" +
        ////    //    $"\nFordon: {Vehicles}" +
        ////    //    $"\nÄrrende avslutat: {Isfinnish()}";

        ////}
        //public string Isfinnish()
        //{
        //    if (Isfinnished == true)
        //    {
        //        return "Ja";
        //    }
        //    else
        //    {
        //        return "Nej";
        //    }
        //}

    }
}
