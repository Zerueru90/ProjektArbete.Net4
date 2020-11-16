using Logic.Entities;
using Logic.Entities.Person_Entities;
using Logic.Entities.Vehicles_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Logic
{
    public class CRUD : ICRUD
    {
        #region Mekaniker: Skapa/Radera/Uppdatera.
        public void CreateNewMechanic(string name, DateTime birtday, DateTime employmentday, DateTime employmentend)
        {
            Mechanic _mechanic = new Mechanic
            {
                Name = name,
                DateOfBirthday = birtday,
                DateOfEmployment = employmentday,
                DateOfEnd = employmentend
            };

            MechanicList.MechanicLists.Add(_mechanic);
        }

        public void RemoveMechanic(Mechanic objMechanic)
        {
            MechanicList.MechanicLists.Remove(objMechanic);
        }

        public void UpdateMechanic(Mechanic objMechanic)
        {
            //Ser till att GUI blir uppdaterad och ändrar även datan så att det sparas korrekt.
            objMechanic.NotifyPropertyChanged("Name");
            objMechanic.NotifyPropertyChanged("DateOfBirthday");
            objMechanic.NotifyPropertyChanged("DateOfEmployment");
            objMechanic.NotifyPropertyChanged("DateOfEnd");
            objMechanic.NotifyPropertyChanged("Breaks");
            objMechanic.NotifyPropertyChanged("Engine");
            objMechanic.NotifyPropertyChanged("Carbody");
            objMechanic.NotifyPropertyChanged("Windshield");
            objMechanic.NotifyPropertyChanged("Tyre");

            MechanicSkill.AddAndRemoveMechanicSkill(objMechanic);
        }
        #endregion
        #region User: Skapa/Radera User inlogg. 
        public void CreateNewUser(Mechanic objMechanic, string username, string password)
        {
            User _newUser = new User()
            {
                Username = username,
                Password = password
            };
            objMechanic.UserID = _newUser.ID; //Viktigt att detta görs, prop MechanicUser settar auto till true
            objMechanic.MechanicUser = true;//Här ska den vanligtvis setta men den triggar bara PropertyChanged så att WPFn ändras

            UserList.UserLists.Add(_newUser);
        }
        public void RemoveUser(Mechanic objMechanic)
        {
            var obj = UserList.UserLists.FirstOrDefault(x => x.ID == objMechanic.UserID);
            UserList.UserLists.Remove(obj);

            objMechanic.UserID = Guid.Empty;//Viktigt att detta görs, prop MechanicUser settar auto till false
            objMechanic.MechanicUser = false; //Här ska den vanligtvis setta men den triggar bara PropertyChanged så att WPFn ändras, så gör man denna till falsk så ändras det checkboxen direkt.
        }
        #endregion
        #region Fordon: Skapa.
        public void CreateNewVehicle(string vehicletype, string modelname, string regnr, decimal odometer, DateTime regdate, string fuel, bool checkBoxCarHook, decimal maxLoadWeight, int maxPassangers)
        {
            switch (vehicletype)
            {
                case "Bil":
                    Car _newCar = new Car();
                    if (checkBoxCarHook)
                    {
                        _newCar.HasTowbar = true;
                    }
                    NewVehicleDataAndSave(_newCar, modelname, regnr, odometer, regdate, fuel, vehicletype);
                    break;

                case "Motorcyckel":
                    Motorbike _newMotorbike = new Motorbike();
                    NewVehicleDataAndSave(_newMotorbike, modelname, regnr, odometer, regdate, fuel, vehicletype);
                    break;

                case "Lastbil":
                    Truck _newTruck = new Truck();
                    _newTruck.MaxLoadWeight = maxLoadWeight;
                    NewVehicleDataAndSave(_newTruck, modelname, regnr, odometer, regdate, fuel, vehicletype);
                    break;

                case "Buss":
                    Bus _newBus = new Bus();
                    _newBus.MaxTotalPassengers = maxPassangers;
                    NewVehicleDataAndSave(_newBus, modelname, regnr, odometer, regdate, fuel, vehicletype);
                    break;

                default:
                    break;
            }
        }
        private void NewVehicleDataAndSave(Vehicle vehicle, string modelname, string regnr, decimal odometer, DateTime regdate, string fuel, string vehicletype)
        {
            vehicle.VehicleType = vehicletype;
            vehicle.ModelName = modelname;
            vehicle.RegistrationNumber = regnr;
            vehicle.OdoMeter = odometer;
            vehicle.RegistrationDate = regdate;
            vehicle.Fuel = fuel;
            VehicleList.VehicleLists.Add(vehicle);
        }
        #endregion
        #region Ärenden: Skapa/Radera/Uppdatera.
        public void CreateNewErrand(Vehicle objVehicle, string description, string problem)
        {
            Errand newErrand = new Errand();
            newErrand.Description = description;
            newErrand.Problem = problem;
            newErrand.VeichleID = objVehicle.ID;
            newErrand.ModelName = objVehicle.ModelName;
            newErrand.RegistrationNumber = objVehicle.RegistrationNumber;

            ErrandList.ErrandsList.Add(newErrand);
            ErrandMechanicViewCombine.BuildSource();
        }

        public void RemoveErrand(Errand errand)
        {
            var obj = ErrandList.ErrandsList.FirstOrDefault(x => x.ID == errand.ID);

            if (obj.MechanicID != Guid.Empty)
            {
                //Anledningen för denna är att om en Mekaniker redan är tilldelad ett Ärende som ska raderas så måste Mechanic.ErrandsID nollställas.
                Guid Key = Guid.Empty;
                Mechanic mec = null;
                int count = 0;
                foreach (var item in MechanicList.MechanicLists)
                {
                    foreach (var item2 in item.ErrandID)
                    {
                        count++;
                        if (item2 == obj.ID)
                        {
                            mec = item;
                        }
                    }
                    count = 0;
                }

                mec.ErrandID[mec.ErrandID.FindIndex(x => x.Equals(count))] = Key;
            }

            ErrandList.ErrandsList.Remove(obj);
        }

        public void UpdateErrand(CommonView objCommonView, Vehicle objVehicle, string description, string problem)
        {
            //Denna ser till att GUI håller sig uppdaterads.
            objCommonView.ChangeDescription = description;
            objCommonView.ChangeProblem = problem;
            objCommonView.ChangeModelName = objVehicle.ModelName;
            objCommonView.ChangeRegistrationNumber = objVehicle.RegistrationNumber;

            //Denna ser till att Datan sparas.
            var objErrand = ErrandList.ErrandsList.Where(x => x.ID == objCommonView.ErrandID);
            foreach (var item in objErrand)
            {
                item.Description = description;
                item.VeichleID = objVehicle.ID;
                item.Problem = problem;
                item.ModelName = objVehicle.ModelName;
                item.RegistrationNumber = objVehicle.RegistrationNumber;
            }
        }
        #endregion
    }
}