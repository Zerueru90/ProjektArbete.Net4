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
        public string Status { get; set; }
    }
}
