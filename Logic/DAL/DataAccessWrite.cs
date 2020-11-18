using Logic.Entities;
using Logic.Entities.Person_Entities;
using Logic.Entities.Vehicles_Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Logic.DAL
{
    public static class DataAccessWrite<T>
    {
        private static string findMap = $@"{Directory.GetCurrentDirectory()}\DAL";
        private static string filename = "";

        public static void SaveData(ObservableCollection<T> observableData)
        {
            if (observableData.Count != 0)
            {
                DeclareFileName(observableData);
                string path = $"{filename}.json";
                string jsonFileAddress = Path.Combine(@"" + findMap + @"\" + path);

                if (File.Exists(jsonFileAddress))
                {
                    File.Delete(jsonFileAddress); 

                    string jsonString = JsonSerializer.Serialize(observableData);
                    using (StreamWriter write = new StreamWriter(jsonFileAddress, true))
                    {
                        write.Write(jsonString);
                        write.Close();
                    }
                }
                else
                {
                    string jsonString = JsonSerializer.Serialize(observableData);
                    using (StreamWriter write = new StreamWriter(jsonFileAddress, true))
                    {
                        write.Write(jsonString);
                        write.Close();
                    }
                }
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
