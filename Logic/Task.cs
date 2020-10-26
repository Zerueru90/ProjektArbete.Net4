using System;
using System.Collections.Generic;
using System.Text;
using Logic.Entities.Person_Entities;

namespace Logic
{
    public  class Progress{
        public  Guid Id { get; set; }
        public  string _toDo { get; set; }
        public int Count { get; set; }
    }

    public class Done
    {
        public Guid Id { get; set; }
        public string _toDo { get; set; }
    }

   public static class Task
    {
      
        public static List<string> ToDoList { get; set; }
         public static List<Progress> ProgressList { get; set; }
        public static List<Done> DoneList { get; set; }


        public static void AddToDoList(string task)
        {
            ToDoList.Add(task);
        }
        public static void RemoveToDoList(string task)
        {
            ToDoList.Add(task);
        }
        public static void EditToDoList(string task,int n)
        {
            ToDoList[n] = task;//o to do börjar från 0
        }

        /// <summary>
        /// Progress create
        /// n - 
        /// </summary>
        public static void AddProgressList(int ToDoN, Mechanic mechanic)
        {
            if(mechanic.MechanicProgressList.Count == 2)
            {
                Console.WriteLine("Mekanikern är upptagen med 2 äranden!");
            }
            else
            {
                mechanic.MechanicProgressList.Add(ToDoList[ToDoN]);
                Progress progress = new Progress { Id = mechanic.Id, _toDo = ToDoList[ToDoN] };

                ProgressList.Add(progress);
            }
           
        }

        public static void AddDoneList(int ProgressN)
        {
            Progress progress = ProgressList[ProgressN];
           
            Done done = new Done { Id = progress.Id, _toDo = progress._toDo };
           
        }

    }
}
