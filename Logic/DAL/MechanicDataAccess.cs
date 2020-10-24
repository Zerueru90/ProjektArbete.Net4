using Logic.Entities.Person_Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            string jsonString = JsonSerializer.Serialize(mechanic);
            using (StreamWriter write = new StreamWriter(txtFileAddress, true))
            {
                write.Write(jsonString);
            }

        }

        public static Mechanic LoadMechanics()
        {
            Mechanic mechanic;
            var fs = File.OpenRead(txtFileAddress);
            string json = "";
            using (StreamReader read = new StreamReader(fs))
            {
                json = read.ReadToEnd();
                mechanic = JsonSerializer.Deserialize<Mechanic>(json);
            }
            return mechanic;
        }
    }
}