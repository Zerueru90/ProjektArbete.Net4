﻿using Logic.Entities.Vehicles_Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logic.Entities.Person_Entities
{
    public class Mechanic : ObservableObject
    {
        public Mechanic()
        {
            ID = Guid.NewGuid(); 
            SkillLista = new List<string>();
            MechanicDoneList = new List<Guid>();
            MechanicProgressList = new List<Guid>();
            ListContainingInProgressAndDoneErrendIDs = new List<Guid>();
        }
        public Guid ID { get; set; }

        public Guid UserID { get; set; } 

        public List<Guid> MechanicProgressList { get; set; } 

        public string Name { get; set; }

        public DateTime DateOfBirthday { get; set; }

        public DateTime DateOfEmployment { get; set; }

        public DateTime? DateOfEnd { get; set; }

        public List<string> SkillLista { get; set; }

        public List<Guid> MechanicDoneList { get; set; }
        public List<Guid> ListContainingInProgressAndDoneErrendIDs { get; set; }

        #region Bättre för GUI

        public bool Breaks { get; set; }
        public bool Engine { get; set; }
        public bool Carbody { get; set; }
        public bool Windshield { get; set; }
        public bool Tyre { get; set; }

        public bool MechanicUser
        {
            get
            {

                if (UserID != Guid.Empty)
                {
                    return true;
                }
                else
                    return false;
            }
            set
            {
                NotifyPropertyChanged(); 
            }
        }
        #endregion
    }
}