using Newtonsoft.Json;

namespace TeslaInventoryNet
{
    /// <summary>
    /// Defines a single inventory result
    /// </summary>
    public class Result
    {
        [JsonProperty("AUTOPILOT")]
        public string[] Autopilot { get; set; }
        public string City { get; set; }
        public long InventoryPrice { get; set; }

        public bool IsDemo { get; set; }
        public string Model { get; set; }
        public string StateProvince { get; set; }
        public long Odometer { get; set; }
        public string OdometerType { get; set; }

        [JsonProperty("OptionCodeData")]
        public OptionCode[] Options { get; set; }
        public string TrimName { get; set; }
        public string Vin { get; set; }

        public int Year { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class OptionCode
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
    }
}