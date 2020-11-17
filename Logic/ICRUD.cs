using Logic.Entities.Person_Entities;
using Logic.Entities.Vehicles_Entities;
using System;

namespace Logic
{
    public interface ICRUD
    {
        void CreateNewErrand(Vehicle objVehicle, string description, string problem);
        void CreateNewMechanic(string name, DateTime birtday, DateTime employmentday, DateTime employmentend);
        void CreateNewUser(Mechanic objMechanic, string username, string password);
        void CreateNewVehicle(Vehicle vehicle, string modelname, string regnr, decimal odometer, DateTime regdate, string fuel, string vehicletype);
        void RemoveErrand(CommonView errand);
        void RemoveMechanic(Mechanic objMechanic);
        void RemoveUser(Mechanic objMechanic);
        void UpdateErrand(CommonView objCommonView, Vehicle objVehicle, string description, string problem);
        void UpdateMechanic(Mechanic objMechanic);
    }
}