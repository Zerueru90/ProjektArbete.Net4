using Logic.Entities.Person_Entities;
using Newtonsoft.Json;
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
        private static string txtFileAddress = Path.Combine(@"" + findMap + @"\" + path);

        private static List<Mechanic> MechanicList = new List<Mechanic>();

        public static void SaveNewMechanicData(Mechanic mechanic)
        {
            if (File.Exists(txtFileAddress))
            {
                ReadJsonFile(mechanic);


                string jsonString = JsonConvert.SerializeObject(mechanic);
                jsonString = JsonPrettify(jsonString);
                using (StreamWriter write = new StreamWriter(txtFileAddress, true))
                {
                    write.Write(jsonString);
                    write.Close();
                }

            }
            else
            {
                string jsonString = JsonConvert.SerializeObject(mechanic);
                jsonString = JsonPrettify(jsonString);
                using (StreamWriter write = new StreamWriter(txtFileAddress, true))
                {
                    write.Write(jsonString);
                    write.Close();
                }
            }
        }

        public static string JsonPrettify(string json)
        {
            using (var stringReader = new StringReader(json))
            using (var stringWriter = new StringWriter())
            {
                var jsonReader = new JsonTextReader(stringReader);
                var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented };
                jsonWriter.WriteToken(jsonReader);
                return stringWriter.ToString();
            }
        }

        public static void ReadJsonFile(Mechanic mechanic)
        {
            MechanicList.Add(mechanic);
            string jsonString = File.ReadAllText(txtFileAddress);

            // var jsonRead = JsonSerializer.Deserialize<Mechanic>(jsonString);
            var jsonRead = JsonConvert.DeserializeObject<Mechanic>(jsonString);
                                    //.Deserialize<Mechanic>(jsonString);
            MechanicList.Add(jsonRead);

        }
    }
}