using Logic.Entities.Vehicles_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.DAL
{
    public class Database
    {
        List<Car> ListofCars { get; set; }
        List<Motobike> ListOfMotobikes { get; set; }
        List<Truck> ListofTrucks { get; set; }
        List<Bus> ListofBusses { get; set; }
    }
}
