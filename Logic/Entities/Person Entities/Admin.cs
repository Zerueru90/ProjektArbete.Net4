using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities.Person_Entities
{
    class Admin
    {

        
        
            public List<Mekanik> MekanikSystem { get; set; } //skapar list för mekanikernas system så att man kan adda, ta bort osv

            public Admin()
            {
                MekanikSystem = new List<Mekanik>(); // Konstruktorn funkar när man bygger admin, som har list,   
            }

            public void AddMekanik(Mekanik NewMekaniker)
            {
                MekanikSystem.Add(NewMekaniker);
            }

            public void RemoveMekanik(Mekanik NewMekaniker)
            {
                MekanikSystem.Remove(NewMekaniker);
            }

            public void AddMekanikSkill(int _id, string skill)
            {
                Mekanik mekaniker = new Mekanik();

                foreach (var item in MekanikSystem)
                {
                    if (item.Id == _id)
                    {
                        mekaniker = item;
                    }
                }

                mekaniker.Skills.Add(skill); //"t.ex MotorRenoverare" som bilmekanikerns kompentes 

            }

            public void RemoveMekanikSkill(int _id, string skill)
            {
                Mekanik mekaniker = new Mekanik();

                foreach (var item in MekanikSystem)
                {
                    if (item.Id == _id)
                    {
                        mekaniker = item;
                    }
                }

                mekaniker.Skills.Remove(skill); //"t.ex MotorRenoverare" som bilmekanikerns kompentes 

            }

            public void AddUser(User user)   //skapade uuser i mekanik listen som finns på mekanik class
            {
                Mekanik mekanik = new Mekanik();

                foreach (var item in MekanikSystem)
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
                Mekanik mekanik = new Mekanik();

                foreach (var item in MekanikSystem)
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


