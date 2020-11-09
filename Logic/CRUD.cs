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
    public class CRUD
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
            //Resten uppdateras direkt från GUI
            objMechanic.NotifyPropertyChanged("Breaks");
            objMechanic.NotifyPropertyChanged("Engine");
            objMechanic.NotifyPropertyChanged("Carbody");
            objMechanic.NotifyPropertyChanged("Windshield");
            objMechanic.NotifyPropertyChanged("Tyre");
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
        public void CreateNewVehicle(string cases, string modelname, string regnr, decimal odometer, DateTime regdate, string fuel, bool checkBoxCarHook, decimal maxLoadWeight, int maxPassangers)
        {
            switch (cases)
            {
                case "Bil":
                    Car _newCar = new Car();
                    if (checkBoxCarHook)
                    {
                        _newCar.SetTowbarValue(true);
                    }
                    NewVehicleDataAndSave(_newCar, modelname, regnr, odometer, regdate, fuel);
                    break;

                case "Motorcykel":
                    Motorbike _newMotorbike = new Motorbike();
                    NewVehicleDataAndSave(_newMotorbike, modelname, regnr, odometer, regdate, fuel);
                    break;

                case "Lastbil":
                    Truck _newTruck = new Truck();
                    _newTruck.SetMaxLoadWeight(maxLoadWeight);
                    NewVehicleDataAndSave(_newTruck, modelname, regnr, odometer, regdate, fuel);
                    break;

                case "Buss":
                    Bus _newBus = new Bus();
                    _newBus.SetPassengersValue(maxPassangers);
                    NewVehicleDataAndSave(_newBus, modelname, regnr, odometer, regdate, fuel);
                    break;

                default:
                    break;
            }
        }
        private void NewVehicleDataAndSave(Vehicle vehicle, string modelname, string regnr, decimal odometer, DateTime regdate, string fuel)
        {
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
        #region Tilldela Ärende till mekaniker
        public bool AddMechanicErrandList(Mechanic objMechanic, CommonView objCommonView)
        {
            if (objMechanic.MechanicProgressList.Count != 2) //Så att man inte kan tilldela mer än 2 ärenden.
            {
                //Denna ser till att GUI håller sig uppdaterad
                objCommonView.ChangeMechanicID = objMechanic.ID;
                objCommonView.ChangeName = objMechanic.Name;
                objCommonView.ChangeStatus = "Pågående";

                //Denna ser till att Datan sparas.
                var objErrands = ErrandList.ErrandsList.Where(x => x.ID == objCommonView.ErrandID);
                foreach (var item in objErrands)
                {
                    item.MechanicID = objMechanic.ID;
                    item.Status = "Pågående";
                    objMechanic.ErrandID.Add(item.ID);

                    //Funkar
                    Task.AddProgressList(objMechanic, item.ID.ToString());
                }

                return true;
            }
            else
                return false;
        }

        public bool ChangeMechanicStatus(CommonView objCommonView, Mechanic objMechanic, string newStatus)
        {
            if (objCommonView.MechanicID != Guid.Empty)
            {
                var objErrands = ErrandList.ErrandsList.Where(x => x.ID == objCommonView.ErrandID);
                foreach (var item in objErrands)
                {
                    Task.AddDoneList(objMechanic, item.ID.ToString());
                    item.Status = newStatus;
                }

                objCommonView.ChangeStatus = newStatus;
                return true;
            }
            else
                return false;
        }
        #endregion
        public void AddMekchanicSkill(Guid _id, string skill)
        {
            Mechanic mekanik = MechanicList.MechanicLists.FirstOrDefault(item => item.ID == _id);

            mekanik.SkillLista.Add(skill); 
        }

        public void RemoveMechanicSkill(Guid _id, string skill)
        {
            Mechanic mekanik = MechanicList.MechanicLists.FirstOrDefault(item => item.ID == _id);
            mekanik.SkillLista.Remove(skill); 
        }

        public void RemoveFromMechanicProgressList(Mechanic mechanic, string ErrandID)
        {
            mechanic.MechanicProgressList.Remove(ErrandID);
        }

        //show mechanic

        public void ShowMechanic(int skillNum)
        {
            switch (skillNum)
            {
                case (byte)Enums.VehicelProblems.Bromsar:
                    Console.WriteLine(Enums.VehicelProblems.Bromsar);
                    break;

                case (byte)Enums.VehicelProblems.Motor:
                    Console.WriteLine(Enums.VehicelProblems.Motor);
                    break;

                case (byte)Enums.VehicelProblems.Kaross:
                    Console.WriteLine(Enums.VehicelProblems.Kaross);
                    break;

                case (byte)Enums.VehicelProblems.Vindruta:
                    Console.WriteLine(Enums.VehicelProblems.Vindruta);
                    break;

                case (byte)Enums.VehicelProblems.Däck:
                    Console.WriteLine(Enums.VehicelProblems.Motor);
                    break;

                default:
                    Console.WriteLine("Finns ingen mekanik med detta kompetens!");
                    break;
            }



        }

        // 0 - Karen
        //1 - Gurgen

        public void ShowAllMechanic()
        {
            int x = 0;
            foreach (var item in MechanicList.MechanicLists)
            {
                Console.WriteLine($"{x++} - {item.Name}"); //id start 0 for mechanic , if id int
            }
            ShowCurrentMechanic(int.Parse(Console.ReadLine()));
        }

        public void ShowCurrentMechanic(int Id)
        {
            Mechanic mechanic = MechanicList.MechanicLists[Id];
            var listDone = mechanic.MechanicDoneList;

            Console.WriteLine($"Name - {mechanic.Name}");
            Console.WriteLine($"Name - {mechanic.ID}");
            Console.WriteLine($"Name - {mechanic.DateOfBirthday}");
            Console.WriteLine($"Name - {mechanic.DateOfEmployment}");
            Console.WriteLine($"Name - {mechanic.DateOfEnd}");

            Console.WriteLine($"Skill List");

            foreach (var item in mechanic.SkillLista)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine($"Progress List");
            foreach (var item in mechanic.MechanicProgressList)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Done list");
            foreach (var item in listDone)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("--------");

            
        }

        public void ShowProccesAndDoneTaskList()
        {
            short count = 1;
            foreach (var item in Task.ProgressList)
            {
                //1) taskname
                //2) taskname
                Console.WriteLine($"{count++}) {item}");
            }
            ShowCurrentProccesTask(int.Parse(Console.ReadLine()));
            //1) ProgressList[0]
            //2) ProgressList[1]

            count = 1;
            foreach (var item in Task.DoneList)
            {
                //1) taskname
                //2) taskname
                Console.WriteLine($"{count++}) {item}");
            }
            ShowCurrentDoneTask(int.Parse(Console.ReadLine()));

        }

        //Kalla då när man behöver veta om vilken progress task är kopplad till vilken meckanikern

        public void ShowCurrentProccesTask(int n) 
        {
            Progress progress = Task.ProgressList[n - 1];
            Mechanic mechanic = MechanicList.MechanicLists.FirstOrDefault(item => item.ID == progress.Id);

            Console.WriteLine("Mechanic name: " + mechanic.Name);
            Console.WriteLine("Current task" + progress._toDo);


            
        }

        public void ShowCurrentDoneTask(int n)
        {
            Done done = Task.DoneList[n - 1];
            Mechanic mechanic = MechanicList.MechanicLists.FirstOrDefault(item => item.ID == done.Id);

            Console.WriteLine("Mechanic name: " + mechanic.Name);
            Console.WriteLine("Current task" + done._toDo);

        }
    }
}