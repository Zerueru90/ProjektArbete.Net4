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
        public enum FuelType
        {
            El,
            Bensin,
            Diesel,
            Etanol
        }
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
