using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities.Vehicles_Entities
{
    public class Car : Vehicle
    {
        public bool HasTowbar { get; set; }

        public enum CarType
        {
            Sedan,
            Herrgårdsvagn,
            Cabriolet,
            Halvkombi,
        };

    }
}
