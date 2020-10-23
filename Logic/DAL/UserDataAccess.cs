using Logic.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Logic.DAL
{
    public class UserDataAccess
    {
        private const string path = @"DAL\User.json";

        // EN RELATIV SÖKVÄG!
        string startupPath = Directory.GetCurrentDirectory();

        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {

            string jsonString = File.ReadAllText(path);
            List<User> users = JsonSerializer.Deserialize<List<User>>(jsonString);

            return users;
        }
        //// Från c# till Json fil!
        //string json = JsonSerializer.Serialize(bs);
        //FileStream fs = File.OpenWrite($"{startupPath}/Boksamlingar.json");
        //StreamWriter sw = new StreamWriter(fs);
        //sw.WriteLine(json);
        //    sw.Close();

        //    // Från Json-fil tillbaka till c#
        //fs = File.OpenRead($"{startupPath}/Boksamlingar.json");
        //StreamReader sr = new StreamReader(fs);
        //json = sr.ReadToEnd();
        //Boksamling upplast = JsonSerializer.Deserialize<Boksamling>(json);
        //sr.Close();
     
    }
}
