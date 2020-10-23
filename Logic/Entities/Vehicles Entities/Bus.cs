using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities.Vehicles_Entities
{
    class Bus :Vehicles
    {
        List<Bus> buses { get; set; }
        private int _maxtotalPassengers;
    }
}
