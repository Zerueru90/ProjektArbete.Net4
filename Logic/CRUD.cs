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
            Mechanic mekaniker = new Mechanic();

            foreach (var item in MechanicList._mechanicList)
            {
                if (item.Id == _id)
                {
                    mekaniker = item;
                }
            }

            mekaniker.SkillLista.Add(skill); //"t.ex MotorRenoverare" som bilmekanikerns kompentes 

        }

        public void RemoveMechanicSkill(Guid _id, string skill)
        {
            Mechanic mekaniker = new Mechanic();

            foreach (var item in MechanicList._mechanicList)
            {
                if (item.Id == _id)
                {
                    mekaniker = item;
                }
            }

            mekaniker.SkillLista.Remove(skill); //"t.ex MotorRenoverare" som bilmekanikerns kompentes 

        }

        public void AddUser(User user)   //skapade uuser i mekanik listen som finns på mekanik class
        {
            Mechanic mekanik = new Mechanic();

            //foreach (var item in MechanicList._mechanicList)
            //{
            //    if (item.Id == user.MekanikerId)
            //    {
            //        mekanik = item;
            //    }
            //}
            mekanik.users.Add(user);
            
        }
        public void RemoveUser(User user)   //skapade uuser i mekanik listen som finns på mekanik class
        {
            Mechanic mekanik = new Mechanic();

            //foreach (var item in MechanicList._mechanicList)
            //{

            //    if (item.Id == user.MekanikerId)
            //    {
            //        mekanik = item;
            //    }
            //}
            mekanik.users.Remove(user);
            
        }

        //4 an är oklart, vilka ärenden ? från 4-8 hur gör man dom ? finns det något speciellt sätt 

    }
}
