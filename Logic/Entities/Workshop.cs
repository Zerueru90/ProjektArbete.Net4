using Logic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Workshop : ObservableObject
    {
        public int Engine { get; set; }
        public int Carbody { get; set; }
        public int Windshield { get; set; }


        public int CarBreaks { get; set; }
        public int CarTyre { get; set; }

        public int TruckBreaks { get; set; }
        public int TruckTyre { get; set; }

        public int MotorcykelBreaks { get; set; }
        public int MotorcykelTyre { get; set; }

        public int BusBreaks { get; set; }
        public int BusTyre { get; set; }


        public Workshop()
        {
            CarBreaks = 8;
            TruckBreaks = 12;
            MotorcykelBreaks = 4;
            BusBreaks = 12;

            Engine = 2;
            Carbody = 2;
            Windshield = 2;

            CarTyre = 8;
            TruckTyre = 12;
            MotorcykelTyre = 4;
            BusTyre = 12;
        }


        public void UpdateWorkshopDataGrid()
        {
            string[] prop = new string[] { "CarBreaks", "TruckBreaks", "MotorcykelBreaks", "BusBreaks", "Engine", "Carbody", "Windshield", "CarTyre", "TruckTyre", "MotorcykelTyre", "BusTyre" };

            for (int i = 0; i < prop.Length; i++)
            {
                NotifyPropertyChanged(prop[i]);
            }
        }

    }
}
