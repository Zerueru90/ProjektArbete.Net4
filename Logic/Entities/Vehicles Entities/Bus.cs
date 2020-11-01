using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities.Vehicles_Entities
{
    public class Bus : Vehicle
    {
        private int _maxtotalPassengers;

        public int GetPassengersValue()
        {
            return _maxtotalPassengers;
        }
        public void SetPassengersValue(int value)
        {
            _maxtotalPassengers = value;
        }
    }
}
