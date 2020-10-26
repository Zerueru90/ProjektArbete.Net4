using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities.Vehicles_Entities
{
    public class Errands : Vehicles
    {
        public List<Errands> OngoingErrands { get; set; }
        public List<Errands> finnishedErrands { get; set; }
        public int ErrandsID { get; set; }

        public bool tryTOAdd(Vehicles vehicles)
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
            var isOktoAdd = tryTOAdd(vehicles);
            if (isOktoAdd)
            {
                OngoingErrands.Add((Errands)vehicles);
                return true;
            }
            else
                return false;
        }
       
    }
}
