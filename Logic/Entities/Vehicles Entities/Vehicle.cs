using Logic.Entities.Person_Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Logic.Entities.Vehicles_Entities
{
    [System.Text.Json.Serialization.JsonConverter(typeof(VehicleConverter))]
    public abstract class Vehicle
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string VehicleType { get; set; }
        public string ModelName { get; set; }
        public string RegistrationNumber { get; set; }
        public decimal OdoMeter { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Fuel { get; set; }
    }

    public class VehicleConverter : System.Text.Json.Serialization.JsonConverter<Vehicle>
    {
        public override Vehicle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var json = reader.GetString();
            var jobj = JObject.Parse(json);
            var vehicletype = jobj["VehicleType"];

            switch (vehicletype.ToString())
            {
                case "Bil":
                    return System.Text.Json.JsonSerializer.Deserialize<Car>(json);
                    
                case "Lastbil":
                    return System.Text.Json.JsonSerializer.Deserialize<Truck>(json);
                    
                case "Buss":
                    return System.Text.Json.JsonSerializer.Deserialize<Bus>(json);
                    
                case "Motorcyckel":
                    return System.Text.Json.JsonSerializer.Deserialize<Motorbike>(json);
                default:
                    throw new Exception("Feeeeeeeeeeeeeeeeeeel");
            }
            return null;
        }

        public override void Write(Utf8JsonWriter writer, Vehicle value, JsonSerializerOptions options)
        {
            var json = "";
            switch (value.VehicleType)
            {
                case "Bil":
                     json=System.Text.Json.JsonSerializer.Serialize<Car>(value as Car);
                    break;
                case "Lastbil":
                     json=System.Text.Json.JsonSerializer.Serialize<Truck>(value as Truck);
                    break;
                case "Buss":
                     json=System.Text.Json.JsonSerializer.Serialize<Bus>(value as Bus);
                    break;
                case "Motorcyckel":
                    json=System.Text.Json.JsonSerializer.Serialize<Motorbike>(value as Motorbike);
                    break;
                default:
                    throw new Exception("Feeeeeeeeeeeeeeeeeeel");
            }
            writer.WriteStringValue(json);

        }
    }
}
