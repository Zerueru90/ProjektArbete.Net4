using Logic.Entities.Vehicles_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    //Denna klass är för GUI så att man ska kunna få fram lämplig info om ett ärende. Kombinerar båda Ärende, Mekaniker och Fordon.
    public class CommonViewEMV
    {
        public Guid ErrandID { get; set; }
        public Guid MechanicID { get; set; }
        public Guid VehicleID { get; set; }
        public string Model { get; set; }
        public string RegistrationNumber { get; set; }
        public string Description { get; set; }
        public string Problem { get; set; }
        public string MechanicName { get; set; }
        public string Status { get; set; }
        public string VehicleType { get; set; }
        public decimal OdoMeter { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Fuel { get; set; }
        public bool HasTowbar { get; set; }
        public decimal MaxLoadWeight { get; set; }
        public int MaxTotalPassengers { get; set; }

    }
}