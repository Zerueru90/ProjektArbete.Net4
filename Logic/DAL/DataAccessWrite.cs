using Logic.Entities;
using Logic.Entities.Person_Entities;
using Logic.Entities.Vehicles_Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace Logic.DAL
{
    public static class DataAccessWrite<T>
    {
        private static string findMap = $@"{Directory.GetCurrentDirectory()}\DAL";
        private static string filename = "";

        public static void SaveData(ObservableCollection<T> observableData)
        {
            DeclareFileName(observableData);
            string path = $"{filename}.json";
            string jsonFileAddress = Path.Combine(@"" + findMap + @"\" + path);

            if (File.Exists(jsonFileAddress))
            {
                File.Delete(jsonFileAddress); //Raderar "gamla" filen och skapar en ny under med nya inkommande mekaniker.

                string jsonString = JsonConvert.SerializeObject(observableData);
                jsonString = JsonPrettify(jsonString);
                using (StreamWriter write = new StreamWriter(jsonFileAddress, true))
                {
                    write.Write(jsonString);
                    write.Close();
                }
            }
            else
            {
                string jsonString = JsonConvert.SerializeObject(observableData);
                jsonString = JsonPrettify(jsonString);
                using (StreamWriter write = new StreamWriter(jsonFileAddress, true))
                {
                    write.Write(jsonString);
                    write.Close();
                }
            }
        }

        private static string JsonPrettify(string json)
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

        private static void DeclareFileName(ObservableCollection<T> observableData)
        {
            if (observableData is ObservableCollection<Mechanic>)
            {
                filename = "Mechanic";
            }
            if (observableData is ObservableCollection<Errand>)
            {
                filename = "Errand";
            }
            if (observableData is ObservableCollection<User>)
            {
                filename = "User";
            }
            if (observableData is ObservableCollection<Vehicle>)
            {
                filename = "Vehicle";
            }
        }
    }
}
