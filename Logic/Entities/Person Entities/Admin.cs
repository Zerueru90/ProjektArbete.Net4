using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities.Person_Entities
{
    public class Admin
    {

        
        
            public List<Mechanic> MechanicList { get; set; } //skapar list för mekanikernas system så att man kan adda, ta bort osv

            public Admin()
            {
                MechanicList = new List<Mechanic>(); // Konstruktorn funkar när man bygger admin, som har list,   
            }

            public void AddMechanic(Mechanic NewMechanic)
            {
                MechanicList.Add(NewMechanic);
            }

            public void RemoveMechanic(Mechanic NewMechanic)
            {
                MechanicList.Remove(NewMechanic);
            }

            public void AddMekchanicSkill(Guid _id, string skill)
            {
                Mechanic mekaniker = new Mechanic();

                foreach (var item in MechanicList)
                {
                    if (item.Id == _id)
                    {
                        mekaniker = item;
                    }
                }

                mekaniker._skillLista.Add(skill); //"t.ex MotorRenoverare" som bilmekanikerns kompentes 

            }

            public void RemoveMechanicSkill(Guid _id, string skill)
            {
                Mechanic mekaniker = new Mechanic();

                foreach (var item in MechanicList)
                {
                    if (item.Id == _id)
                    {
                        mekaniker = item;
                    }
                }

                mekaniker._skillLista.Remove(skill); //"t.ex MotorRenoverare" som bilmekanikerns kompentes 

            }

            public void AddUser(User user)   //skapade uuser i mekanik listen som finns på mekanik class
            {
                Mechanic mekanik = new Mechanic();

                foreach (var item in MechanicList)
                {

                    if (item.Id == user.MekanikerId)
                    {
                        mekanik = item;
                    }
                }
                mekanik.users.Add(user);

            }
            public void RemoveUser(User user)   //skapade uuser i mekanik listen som finns på mekanik class
            {
                Mechanic mekanik = new Mechanic();

                foreach (var item in MechanicList)
                {

                    if (item.Id == user.MekanikerId)
                    {
                        mekanik = item;
                    }
                }
                mekanik.users.Remove(user);

            }

            //4 an är oklart, vilka ärenden ? från 4-8 hur gör man dom ? finns det något speciellt sätt 


        }
    }


