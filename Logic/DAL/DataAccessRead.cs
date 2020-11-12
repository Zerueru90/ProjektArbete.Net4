using Logic.Entities;
using Logic.Entities.Person_Entities;
using Logic.Entities.Vehicles_Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Logic.DAL
{
    public static class DataAccessRead
    {
        private static string findMap = $@"{Directory.GetCurrentDirectory()}\DAL";
        private static string[] allFiles = Directory.GetFiles(findMap);

        public static void ReadJsonFile()
        {
            for (int i = 0; i < allFiles.Length; i++)
            {
                string[] sort = allFiles[i].Split(new string[] { "\\" }, StringSplitOptions.None);
                string filename = sort[12];
                StreamRead(allFiles[i], filename);
            }
        }

        private static void StreamRead(string fileadress, string filename)
        {
            string json = "";
            using (StreamReader read = new StreamReader(fileadress, true))
            {
                json = read.ReadToEnd();
            }

            switch (filename)
            {
                case "Mechanic.json":
                    var jsonReadMechanic = JsonConvert.DeserializeObject<List<Mechanic>>(json);
                    foreach (var ReadMechanic in jsonReadMechanic)
                    {
                        MechanicList.MechanicLists.Add(ReadMechanic);
                    }
                    break;
                case "User.json":
                    var jsonReadUser = JsonConvert.DeserializeObject<List<User>>(json);
                    foreach (var ReadUser in jsonReadUser)
                    {
                        UserList.UserLists.Add(ReadUser);
                    }
                    break;
                case "Vehicle.json":
                    var jsonReadVehicle = JsonConvert.DeserializeObject<List<Vehicle>>(json);
                    foreach (var ReadVehicle in jsonReadVehicle)
                    {
                        VehicleList.VehicleLists.Add(ReadVehicle);
                    }
                    break;
                case "Errand.json":
                    var jsonReadErrand = JsonConvert.DeserializeObject<List<Errand>>(json);
                    foreach (var ReadErrand in jsonReadErrand)
                    {
                        ErrandList.ErrandsList.Add(ReadErrand);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
