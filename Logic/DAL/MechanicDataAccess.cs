using Logic.Entities.Person_Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Logic.DAL
{
    public static class MechanicDataAccess
    {
        
        private static string findMap = Directory.GetCurrentDirectory();
        private const string path = @"DAL\MechanicJson.json";
        private static string txtFileAddress = Path.Combine(findMap + @"\" + path);


        public static void SaveNewMechanicData(Mechanic mechanic)
        {
            string jsonString = JsonConvert.SerializeObject(mechanic);

            using (StreamWriter write = new StreamWriter(txtFileAddress, true))
            {
                write.WriteLine(jsonString);
            }
        }

        public static List<Mechanic> LoadMechanics()
        {
            //string jsonString = File.ReadAllText(txtFileAddress);
            //List<Mechanic> users = JsonConvert.DeserializeObject<List<Mechanic>>(jsonString);

            //using (StreamReader read = new StreamReader(txtFileAddress, true))
            //{
            //    read.ReadLine(jsonString);
            //}


            //return users;
        }
    }
}