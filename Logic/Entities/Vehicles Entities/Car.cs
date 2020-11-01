using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities.Vehicles_Entities
{
    public class Car : Vehicle
    {
        private bool _hasTowbar;

        public enum CarType
        {
            Sedan,
            Herrgårdsvagn,
            Cabriolet,
            Halvkombi,
        };

        public bool GetTowbarValue()
        {
            return _hasTowbar;
        }
        public void SetTowbarValue(bool value)
        {
            _hasTowbar = value;
        }
    }
}
