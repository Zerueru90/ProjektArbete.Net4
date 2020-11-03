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
        public void AddMechanic(Mechanic NewMechanic)
        {
            MechanicList.MechanicLists.Add(NewMechanic);
        }

        public void RemoveMechanic(Mechanic mechanic)
        {
            MechanicList.MechanicLists.Remove(mechanic);
        }

        public void AddMekchanicSkill(Guid _id, string skill)
        {
            Mechanic mekanik = MechanicList.MechanicLists.FirstOrDefault(item => item.Id == _id);

            mekanik.SkillLista.Add(skill); //"t.ex MotorRenoverare" som bilmekanikerns kompentes
        }

        public void RemoveMechanicSkill(Guid _id, string skill)
        {
            Mechanic mekanik = MechanicList.MechanicLists.FirstOrDefault(item => item.Id == _id);
            mekanik.SkillLista.Remove(skill); //"t.ex MotorRenoverare" som bilmekanikerns kompentes
        }

        public void AddUser(User user) 
        {
            UserList.UserLists.Add(user); //Mer kod här istället för .Xaml bosse. Men tillfälligt.
        }
        public void RemoveUser(Guid UserId)   
        {
            var obj = UserList.UserLists.FirstOrDefault(x => x.ID == UserId);
            UserList.UserLists.Remove(obj);
        }

        public void RemoveErrand(Errand errand)
        {
            var obj = ErrandList.ErrandsList.FirstOrDefault(x => x.ErrandsID == errand.ErrandsID);

            //Anledningen för denna är att om en Mekaniker redan är tilldelad ett Ärende som ska raderas så måste Mechanic.ErrandsID nollställas.
            var objMechanicClassErrandsID = MechanicList.MechanicLists.FirstOrDefault(x => x.ErrandsID == errand.ErrandsID);
            if (objMechanicClassErrandsID.ErrandsID != Guid.Empty)
            {
                objMechanicClassErrandsID.ErrandsID = Guid.Empty;
            }

            ErrandList.ErrandsList.Remove(obj);
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
            Console.WriteLine($"Name - {mechanic.Id}");
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
            Mechanic mechanic = MechanicList.MechanicLists.FirstOrDefault(item => item.Id == progress.Id);

            Console.WriteLine("Mechanic name: " + mechanic.Name);
            Console.WriteLine("Current task" + progress._toDo);


            
        }

        public void ShowCurrentDoneTask(int n)
        {
            Done done = Task.DoneList[n - 1];
            Mechanic mechanic = MechanicList.MechanicLists.FirstOrDefault(item => item.Id == done.Id);

            Console.WriteLine("Mechanic name: " + mechanic.Name);
            Console.WriteLine("Current task" + done._toDo);

        }
    }
}