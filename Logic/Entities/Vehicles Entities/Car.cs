using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities.Vehicles_Entities
{
    public class Car : Vehicles
    {
        List<Car> Cars = new List<Car>();
        private bool _hasTowbar;
        private string _sedan;
        private string _manorcar;
        private string _convertible;
        private string _hatchback;
    }
}
