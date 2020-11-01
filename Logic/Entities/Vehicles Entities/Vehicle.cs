﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities.Vehicles_Entities
{
    public class Vehicle
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string ModelName { get; set; }
        public string RegistrationNumber { get; set; }
        public decimal OdoMeter { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Fuel { get; set; }

        public enum VeichleType
        {
            Bil,
            Motorcyckel,
            Lastbil,
            Buss
        }
    }
}
