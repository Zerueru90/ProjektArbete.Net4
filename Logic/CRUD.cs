using Logic.Entities;
using Logic.Entities.Person_Entities;
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
           
            MechanicList._mechanicList.Add(NewMechanic);
        }

        public void RemoveMechanic(Mechanic mechanic)
        {

            MechanicList._mechanicList.Remove(mechanic);

        }

        public void AddMekchanicSkill(Guid _id, string skill)
        {
            Mechanic mekanik = MechanicList._mechanicList.FirstOrDefault(item => item.Id == _id);

            mekanik.SkillLista.Add(skill); //"t.ex MotorRenoverare" som bilmekanikerns kompentes 

        }

        public void RemoveMechanicSkill(Guid _id, string skill)
        {
            Mechanic mekanik = MechanicList._mechanicList.FirstOrDefault(item => item.Id == _id);
            mekanik.SkillLista.Remove(skill); //"t.ex MotorRenoverare" som bilmekanikerns kompentes 

        }

        public void AddUser(User user,Guid Id)   //skapade uuser i mekanik listen som finns på mekanik class
        {
            Mechanic mekanik = MechanicList._mechanicList.FirstOrDefault(item => item.Id == Id);
            mekanik.IdentityUser = user;

        }
        public void RemoveUser(User user,Guid Id)   //skapade uuser i mekanik listen som finns på mekanik class
        {
            Mechanic mekanik = MechanicList._mechanicList.FirstOrDefault(item => item.Id == Id);
            mekanik.IdentityUser = null;
        }


        //show mechanic 

        public void ShowMechanic(int skillNum)
        {
            switch (skillNum)
            {
                case (byte)Vehicelparts.Bromsar:
                    Console.WriteLine(Vehicelparts.Bromsar);
                    break;
                case (byte)Vehicelparts.Motor:
                    Console.WriteLine(Vehicelparts.Motor);
                    break;
                case (byte)Vehicelparts.Kaross:
                    Console.WriteLine(Vehicelparts.Kaross);
                    break;
                case (byte)Vehicelparts.Vindruta:
                    Console.WriteLine(Vehicelparts.Vindruta);
                    break;
                case (byte)Vehicelparts.Däck:
                    Console.WriteLine(Vehicelparts.Motor);
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
            foreach (var item in MechanicList._mechanicList)
            {
                Console.WriteLine($"{x++} - {item.Name}"); 
            }
            ShowCurrentMechanic(int.Parse(Console.ReadLine()));
        }

        public void ShowCurrentMechanic(int Id)
        {
            Mechanic mechanic = MechanicList._mechanicList[Id];
            Console.WriteLine($"Name - {mechanic.Name}");
            Console.WriteLine($"Name - {mechanic.Id}");
            Console.WriteLine($"Name - {mechanic.DateOfBirthday}");
            Console.WriteLine($"Name - {mechanic.DateOfEmployment}");
            Console.WriteLine($"Name - {mechanic.DateOfEmployment}");





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

            Console.WriteLine("--------");

            //karucel done list hajord dasin
            //ev avelcacnel ayd listum mer uzac tvyalnere
            //konkret mechanici donelistum avelacnel progresslistic todos
            //1 2 3 4 5 6 7 8
        }

    }
}
