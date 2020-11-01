using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities.Vehicles_Entities
{
    public class Car : Vehicles
    {
        private bool _hasTowbar;

        enum CarType
        {
            Sedan,
            Herrgårdsvagn,
            Cabriolet,
            Halvkombi,
        };
    }
}
