using Logic.Entities.Person_Entities;
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
        private const string path = @"DAL\MechanicJson.json";
        private static string findMap = Directory.GetCurrentDirectory();

        public static void SaveNewMechanicData(Mechanic mechanic)
        {
            string jsonString = JsonSerializer.Serialize(mechanic);
            string txtFileAddress = Path.Combine(findMap + @"\" +  path);

            using (StreamWriter write = new StreamWriter(txtFileAddress, true))
            {
                write.WriteLine(jsonString);
            }

        }


    }
}
