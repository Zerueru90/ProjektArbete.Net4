using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities.Vehicles_Entities
{
    public class Truck : Vehicle
    {
        private decimal _maxloadWeight;

        public decimal GetMaxLoadWeight()
        {
            return _maxloadWeight;
        }
        public void SetMaxLoadWeight(decimal value)
        {
            _maxloadWeight = value;
        }
    }
}
