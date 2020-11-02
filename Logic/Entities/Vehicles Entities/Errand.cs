using Logic.Entities.Person_Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Logic.Entities.Vehicles_Entities
{
    public class Errand : INotifyPropertyChanged
    {
        public Guid ErrandsID { get; set; } = Guid.NewGuid();
        public Guid VeichleID { get; set; } //När man sparar ett Ärende så måste man ha ett fordon och en MEKANIKER
        public Guid MechanicID { get; set; }
        public string Mechanic { get; set; }
        public string Description { get; set; }
        public string Problem { get; set; }
        public string Status { get; set; }

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

        //Denna är för att varje gång vi gör några ändringar så kallas detta.
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propName = "")
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
