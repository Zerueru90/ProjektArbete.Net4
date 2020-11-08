using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public static class Enums
    {
        public enum VeichleType
        {
            Bil,
            Motorcyckel,
            Lastbil,
            Buss
        }
        //public enum Cartype
        //{
        //    sedan = 1,
        //    manorcar = 2,
        //    convertible = 3,
        //    hatckback = 4,
        //}
        public enum VehicelProblems
        {
            Bromsar,
            Motor,
            Kaross,
            Vindruta,
            Däck,
        }
        public enum VehicelStatus
        {
            Pågående,
            Klar,
        }
    }
}
