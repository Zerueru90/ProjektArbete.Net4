using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities.Vehicles_Entities
{
    public abstract class Vehicles
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public Guid RegNrID { get; set; }
        public string ModelName { get; set; }
        public string RegistrationNumber { get; set; }
        public decimal OdoMeter { get; set; }
        public DateTime Registrated { get; set; }
        public string Fuel { get; set; }

    }
}
