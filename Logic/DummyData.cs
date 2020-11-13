using Logic.Entities;
using Logic.Entities.Person_Entities;
using Logic.Entities.Vehicles_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public static class DummyData
    {

        private static Guid JohnID = Guid.Empty;
        private static Guid DaveID = Guid.Empty;

        public static void ErrandData()
        {
            ErrandList.ErrandsList.Add(new Errand()
            {
                Description = "blablabla",
                Problem = "Tyre",
                Status = "Available"
            });

            Errand errands = null;
            foreach (var item in ErrandList.ErrandsList)
            {
                errands = item;
            }
        }

        public static void UserData()
        {
            UserList.UserLists.Add(new User()
            {
                Username = "John",
                Password = "password"
            });
            UserList.UserLists.Add(new User()
            {
                Username = "Dave",
                Password = "password"
            });


            foreach (var item in UserList.UserLists)
            {
                if (item.Username == "John")
                {
                    JohnID = item.ID;
                }
                if (item.Username == "Dave")
                {
                    DaveID = item.ID;
                }
            }
        }

        public static void MecanichData()
        {
            MechanicList.MechanicLists.Add(new Mechanic()
            {
                Name = "John",
                DateOfBirthday = DateTime.Now,
                DateOfEmployment = DateTime.Now,
                DateOfEnd = DateTime.Now,
            });
            MechanicList.MechanicLists.Add(new Mechanic()
            {
                Name = "Dave",
                DateOfBirthday = DateTime.Now,
                DateOfEmployment = DateTime.Now,
                DateOfEnd = DateTime.Now
            });
        }

        public static void VehicleData()
        {
            VehicleList.VehicleLists.Add(new Car()
            {
                ModelName = "Audi",
                RegistrationNumber = "AUD123",
                OdoMeter = 2500,
                RegistrationDate = DateTime.Now,
                Fuel = "Diesel"
            });
            VehicleList.VehicleLists.Add(new Car()
            {
                ModelName = "Volvo",
                RegistrationNumber = "VOL321",
                OdoMeter = 4500,
                RegistrationDate = DateTime.Now,
                Fuel = "Bensin"
            });
            VehicleList.VehicleLists.Add(new Car()
            {
                ModelName = "BMW",
                RegistrationNumber = "BMW123",
                OdoMeter = 2500,
                RegistrationDate = DateTime.Now,
                Fuel = "Diesel"
            });
            VehicleList.VehicleLists.Add(new Car()
            {
                ModelName = "Ferrari",
                RegistrationNumber = "AAA001",
                OdoMeter = 4500,
                RegistrationDate = DateTime.Now,
                Fuel = "Bensin"
            });
            VehicleList.VehicleLists.Add(new Car()
            {
                ModelName = "Kia",
                RegistrationNumber = "KIA627",
                OdoMeter = 2500,
                RegistrationDate = DateTime.Now,
                Fuel = "Diesel"
            });
            VehicleList.VehicleLists.Add(new Car()
            {
                ModelName = "Saab",
                RegistrationNumber = "SAA951",
                OdoMeter = 4500,
                RegistrationDate = DateTime.Now,
                Fuel = "Bensin"
            });
        }
    }
}
