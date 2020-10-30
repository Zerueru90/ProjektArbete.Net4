using Logic.Entities.Person_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities.Vehicles_Entities
{
    public class Errands : Vehicles
    {
        public List<Errands> OngoingErrands { get; set; }
        public List<Errands> finnishedErrands { get; set; }
        
        public Guid ErrandsID { get; set; } = Guid.NewGuid();
        public Guid? VeichleID { get; set; } 
        public string Description { get; set; }
        public bool Isfinnished { get; set; }
        public string Problem { get; set; }

        //public Mechanic Mechanic { get; set; }
        //public Vehicles Vehicles { get; set; }

        //Inlagd av Samson, tillfällig.
        public string Status { get; set; }
        //_-------------------
        public bool TryToAdd(Vehicles vehicles)
        {
            var counter = 0;
            if (counter <= 2)
            {
                counter++;
                return true;
            }
            else
            {
                //MessageBox.Show("Tyvärr kan en makaniker endast ha två ärenden samtidigt");
                return false;
            }
        }
        public bool AddErrands(Vehicles vehicles)
        {
            var isOktoAdd = TryToAdd(vehicles);
            if (isOktoAdd)
            {
                OngoingErrands.Add((Errands)vehicles);
                return true;
            }
            else
                return false;
        }
        //public string GetinfoOfErrand()
        //{
        //    //return $"Beskrvining: {Description}" +
        //    //    $"\nProblem: {Problem}" +
        //    //    $"\nMekaniker: {Mechanic}" +
        //    //    $"\nFordon: {Vehicles}" +
        //    //    $"\nÄrrende avslutat: {Isfinnish()}";
    
        //}
        public string Isfinnish()
        {
            if (Isfinnished == true)
            {
                return "Ja";
            }
            else
            {
                return "Nej";
            }
        }
       
    }
}
