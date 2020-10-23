using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities.Vehicles_Entities
{
    public class RegNumber
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string Varde { get; set; }
        public Guid fordonsID { get; set; }
        public FordonsTyp Fordonstyp { get; set; }
    }
    public enum FordonsTyp
    {
        Car,
        Motobike,
        Truck,
        Bus,
    }
}
