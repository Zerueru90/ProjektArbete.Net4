﻿using Logic.Entities.Person_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities.Vehicles_Entities
{
    public abstract class Vehicle 
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string ModelName { get; set; }
        public string RegistrationNumber { get; set; }
        public decimal OdoMeter { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Fuel { get; set; }
    }
}
