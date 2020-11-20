using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public static class WorkshopWarehouse
    {
        #region Läser och ser om allt är tillgänligt
        public static bool CheckWarehouse(string vehicleType, string problem, Workshop workshop)
        {
            bool trueOrFalse = false;
            switch (vehicleType)
            {
                case "Bil":
                    if (IsCompontentInStock(vehicleType, problem, workshop))
                    {
                        trueOrFalse = true;
                        return true;
                    }
                    else
                        return false;

                case "Motorcyckel":
                    if (IsCompontentInStock(vehicleType, problem, workshop))
                    {
                        trueOrFalse = true;
                        return true;
                    }
                    else
                        return false;

                case "Buss":
                    if (IsCompontentInStock(vehicleType, problem, workshop))
                    {
                        trueOrFalse = true;
                        return true;
                    }
                    else
                        return false;

                case "Lastbil":
                    if (IsCompontentInStock(vehicleType, problem, workshop))
                    {
                        trueOrFalse = true;
                        return true;
                    }
                    else
                        return false;
                default:
                    break;
            }
            return trueOrFalse;
        }

        private static bool IsCompontentInStock(string vehicleType, string problem, Workshop workshop)
        {
            bool trueOrFalse = false;
            switch (problem)
            {
                case "Bromsar":
                    trueOrFalse = CheckBreaksAreInStock(vehicleType, problem, workshop);
                    break;

                case "Motor":
                    if (workshop.Engine != 0)
                    {
                        workshop.Engine -= 1;
                        trueOrFalse = true;
                        return true;
                    }
                    else
                        return false;

                case "Kaross":
                    if (workshop.Carbody != 0)
                    {
                        workshop.Carbody -= 1;
                        trueOrFalse = true;
                        return true;
                    }
                    else
                        return false;

                case "Vindruta":
                    if (workshop.Windshield != 0)
                    {
                        workshop.Windshield -= 1;
                        trueOrFalse = true;
                        return true;
                    }
                    else
                        return false;

                case "Däck":
                    trueOrFalse = CheckTyresAreInStock(vehicleType, problem, workshop);
                    break;
                default:
                    break;
            }
            return trueOrFalse;
        }
        private static bool CheckBreaksAreInStock(string vehicleType, string problem, Workshop workshop)
        {
            bool trueOrFalse = false;
            switch (vehicleType)
            {
                case "Bil":
                    if (workshop.CarBreaks >= 4)
                    {
                        workshop.CarBreaks -= 4;
                        trueOrFalse = true;
                        return true;
                    }
                    else
                        return false;

                case "Motorcyckel":
                    if (workshop.MotorcykelBreaks >= 2)
                    {
                        workshop.MotorcykelBreaks -= 2;
                        trueOrFalse = true;
                        return true;
                    }
                    else
                        return false;

                case "Buss":
                    if (workshop.BusBreaks >= 6)
                    {
                        workshop.BusBreaks -= 6;
                        trueOrFalse = true;
                        return true;
                    }
                    else
                        return false;

                case "Lastbil":
                    if (workshop.TruckBreaks >= 6)
                    {
                        workshop.TruckBreaks -= 6;
                        trueOrFalse = true;
                        return true;
                    }
                    else
                        return false;
                default:
                    break;
            }
            return trueOrFalse;
        }
        private static bool CheckTyresAreInStock(string vehicleType, string problem, Workshop workshop)
        {
            bool trueOrFalse = false;
            switch (vehicleType)
            {
                case "Bil":
                    if (workshop.CarTyre >= 4)
                    {
                        workshop.CarTyre -= 4;
                        trueOrFalse = true;
                        return true;
                    }
                    else
                        return false;

                case "Motorcyckel":
                    if (workshop.MotorcykelTyre >= 2)
                    {
                        workshop.MotorcykelTyre -= 2;
                        trueOrFalse = true;
                        return true;
                    }
                    else
                        return false;

                case "Buss":
                    if (workshop.BusTyre >= 6)
                    {
                        workshop.BusTyre -= 6;
                        trueOrFalse = true;
                        return true;
                    }
                    else
                        return false;

                case "Lastbil":
                    if (workshop.TruckTyre >= 6)
                    {
                        workshop.TruckTyre -= 6;
                        trueOrFalse = true;
                        return true;
                    }
                    else
                        return false;

                default:
                    break;
            }
            return trueOrFalse;
        }
        #endregion

        #region Beställning

        public static void OrderToWarehouse(string vehicleType, string problem, int antal, Workshop workshop)
        {
            switch (vehicleType)
            {
                case "Bil":
                    CheckStock(vehicleType, problem, antal, workshop);
                    break;

                case "Motorcyckel":
                    CheckStock(vehicleType, problem, antal, workshop);
                    break;

                case "Buss":
                    CheckStock(vehicleType, problem, antal, workshop);
                    break;

                case "Lastbil":
                    CheckStock(vehicleType, problem, antal, workshop);
                    break;

                default:
                    break;
            }
        }

        private static void CheckStock(string vehicleType, string problem, int antal, Workshop workshop)
        {
            switch (problem)
            {
                case "Bromsar":
                    AddBreaksToStock(vehicleType, problem, antal, workshop);
                    break;

                case "Motor":
                    workshop.Engine += antal;
                    break;

                case "Kaross":
                    workshop.Carbody += antal;
                    break;

                case "Vindruta":
                    workshop.Windshield += antal;
                    break;

                case "Däck":
                    AddTyresToStock(vehicleType, problem, antal, workshop);
                    break;
                default:
                    break;
            }
        }

        private static void AddBreaksToStock(string vehicleType, string problem, int antal, Workshop workshop)
        {
            switch (vehicleType)
            {
                case "Bil":
                    workshop.CarBreaks += antal;
                    break;

                case "Motorcyckel":
                    workshop.MotorcykelBreaks += antal;
                    break;

                case "Buss":
                    workshop.BusBreaks += antal;
                    break;

                case "Lastbil":
                    workshop.TruckBreaks += antal;
                    break;
                default:
                    break;
            }
        }
        private static void AddTyresToStock(string vehicleType, string problem, int antal, Workshop workshop)
        {
            switch (vehicleType)
            {
                case "Bil":
                    workshop.CarTyre += antal;
                    break;

                case "Motorcyckel":
                    workshop.MotorcykelTyre += antal;
                    break;

                case "Buss":
                    workshop.BusTyre += antal;
                    break;

                case "Lastbil":
                    workshop.TruckTyre += antal;
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
