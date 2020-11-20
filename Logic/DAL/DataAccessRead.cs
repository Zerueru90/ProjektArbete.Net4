using Logic.Entities;
using Logic.Entities.Person_Entities;
using Logic.Entities.Vehicles_Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
//Ändra ordning på "allfiles" så att först Mek, User, Vechile, Errends.
namespace Logic.DAL
{
    public static class DataAccessRead
    {
        private static string findMap = $@"{Directory.GetCurrentDirectory()}\DAL";
        private static string[] allFiles = Directory.GetFiles(findMap);
        private static string[] jsonfiles = { "Mechanic.json", "User.json", "Errand.json", "Vehicle.json" };

        public static async Task ReadJsonFile()
        {
            Array.Reverse(allFiles);
            string filename = "";
            for (int i = 0; i < allFiles.Length; i++)
            {
                string[] sort = allFiles[i].Split(new string[] { "\\" }, StringSplitOptions.None);
                for (int j = 0; j < sort.Length; j++)
                {
                    if (CheckFile(sort[j]))
                    {
                        filename = sort[j];
                    }
                }

                await StreamRead<Vehicle>(allFiles[i], filename);
            }
        }

        private static bool CheckFile(string file)
        {
            switch (file)
            {
                case "Mechanic.json":
                    return true;
                case "User.json":
                    return true;
                case "Errand.json":
                    return true;
                case "Vehicle.json":
                    return true;
                default:
                    break;
            }
            return false;
        }

        private static async Task StreamRead<T>(string fileadress, string filename)
        {
            string json = "";
            using (StreamReader read = new StreamReader(fileadress, true))
            {
                json = await read.ReadToEndAsync();
            }

            switch (filename)
            {
                case "Mechanic.json":
                    var jsonReadMechanic = JsonSerializer.Deserialize<List<Mechanic>>(json);
                    foreach (var ReadMechanic in jsonReadMechanic)
                    {
                        MechanicList.MechanicLists.Add(ReadMechanic);
                    }
                    break;
                case "User.json":
                    var jsonReadUser = JsonSerializer.Deserialize<List<User>>(json);
                    foreach (var ReadUser in jsonReadUser)
                    {
                        UserList.UserLists.Add(ReadUser);
                    }
                    break;
                case "Vehicle.json":
                    var jsonReadVehicle = JsonSerializer.Deserialize<List<Vehicle>>(json);
                    foreach (var ReadVehicle in jsonReadVehicle)
                    {
                        VehicleList.VehicleLists.Add(ReadVehicle);
                    }
                    break;
                case "Errand.json":
                    var jsonReadErrand = JsonSerializer.Deserialize<List<Errand>>(json);
                    foreach (var ReadErrand in jsonReadErrand)
                    {
                        ErrandList.ErrandsList.Add(ReadErrand);
                    }
                    ErrandMechanicViewCombine.BuildSource();
                    ErrandMechanicVehicleViewCombine.BuildSource();
                    break;
                default:
                    break;
            }
        }
    }
}
