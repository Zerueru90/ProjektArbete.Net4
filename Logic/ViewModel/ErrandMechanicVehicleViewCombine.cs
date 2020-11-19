using Logic.Entities.Vehicles_Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Logic
{
    public class ErrandMechanicVehicleViewCombine
    {
        public static ObservableCollection<CommonViewEMV> _source { get; set; }

        public static ObservableCollection<CommonViewEMV> Source
        {
            get
            {
                if (_source == null)
                {
                    return _source = new ObservableCollection<CommonViewEMV>();
                }
                return _source;
            }
            set
            {
                _source = value;
            }
        }

        public static void BuildSource()
        {
            if (Source.Count != 0)
            {
                _source.Clear();
            }
            foreach (var errandItem in ErrandList.ErrandsList)
            {
                //string MechanicName = "";
                //Guid MechanicId = Guid.Empty;
                foreach (var item in MechanicList.MechanicLists)
                {
                    foreach (var item2 in item.ListContainingInProgressAndDoneErrendIDs)
                    {

                        if (item2 == errandItem.ID)
                        {
                            //MechanicName = item.Name;
                            //MechanicId = item.ID;

                            foreach (var item3 in VehicleList.VehicleLists)
                            {
                                if (item3.ID == errandItem.VehicleID)
                                {
                                    SortTypeOfVehicle(item3, item3.VehicleType, errandItem, item.Name, item.ID);
                                }
                            }
                        }
                    }
                }
                if (errandItem.Status == null)
                {
                    foreach (var item3 in VehicleList.VehicleLists)
                    {
                        if (item3.ID == errandItem.VehicleID)
                        {
                            SortTypeOfVehicle(item3, item3.VehicleType, errandItem, "", Guid.Empty);
                        }
                    }
                }
            }
        }

        private static void SortTypeOfVehicle(Vehicle item,string vehicleType, Errand errandItem, string MechanicName, Guid MechanicId)
        {
            Vehicle vehicle = item;
            switch (vehicleType)
            {
                case "Bil":
                    var objCar = vehicle as Car;

                    _source.Add(new CommonViewEMV()
                    {
                        ErrandID = errandItem.ID,
                        MechanicID = MechanicId,
                        VehicleID = errandItem.VehicleID,
                        MechanicName = MechanicName,
                        Model = errandItem.ModelName,
                        RegistrationNumber = errandItem.RegistrationNumber,
                        Description = errandItem.Description,
                        Problem = errandItem.Problem,
                        Status = errandItem.Status,


                        VehicleType = objCar.VehicleType,
                        OdoMeter = objCar.OdoMeter,
                        RegistrationDate = objCar.RegistrationDate,
                        Fuel = objCar.Fuel,
                        HasTowbar = objCar.HasTowbar

                    });
                    break;
                case "Motorcyckel":
                    var objMC = vehicle as Motorbike;

                    _source.Add(new CommonViewEMV()
                    {
                        ErrandID = errandItem.ID,
                        MechanicID = MechanicId,
                        VehicleID = errandItem.VehicleID,
                        MechanicName = MechanicName,
                        Model = errandItem.ModelName,
                        RegistrationNumber = errandItem.RegistrationNumber,
                        Description = errandItem.Description,
                        Problem = errandItem.Problem,
                        Status = errandItem.Status,


                        VehicleType = objMC.VehicleType,
                        OdoMeter = objMC.OdoMeter,
                        RegistrationDate = objMC.RegistrationDate,
                        Fuel = objMC.Fuel

                    });
                    break;
                case "Buss":
                    var objBus = vehicle as Bus;

                    _source.Add(new CommonViewEMV()
                    {
                        ErrandID = errandItem.ID,
                        MechanicID = MechanicId,
                        VehicleID = errandItem.VehicleID,
                        MechanicName = MechanicName,
                        Model = errandItem.ModelName,
                        RegistrationNumber = errandItem.RegistrationNumber,
                        Description = errandItem.Description,
                        Problem = errandItem.Problem,
                        Status = errandItem.Status,


                        VehicleType = objBus.VehicleType,
                        OdoMeter = objBus.OdoMeter,
                        RegistrationDate = objBus.RegistrationDate,
                        Fuel = objBus.Fuel,
                        MaxTotalPassengers = objBus.MaxTotalPassengers

                    });
                    break;
                case "Lastbil":
                    var objTruck = vehicle as Truck;

                    _source.Add(new CommonViewEMV()
                    {
                        ErrandID = errandItem.ID,
                        MechanicID = MechanicId,
                        VehicleID = errandItem.VehicleID,
                        MechanicName = MechanicName,
                        Model = errandItem.ModelName,
                        RegistrationNumber = errandItem.RegistrationNumber,
                        Description = errandItem.Description,
                        Problem = errandItem.Problem,
                        Status = errandItem.Status,


                        VehicleType = objTruck.VehicleType,
                        OdoMeter = objTruck.OdoMeter,
                        RegistrationDate = objTruck.RegistrationDate,
                        Fuel = objTruck.Fuel,
                        MaxLoadWeight = objTruck.MaxLoadWeight

                    });
                    break;
                default:
                    break;
            }

        }
    }
}
