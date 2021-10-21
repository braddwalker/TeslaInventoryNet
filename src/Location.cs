using Newtonsoft.Json;
using System.Reflection;
using System;

namespace TeslaInventoryNet
{
    public class Location
    {
        public string Market { get; set; }
        public string Language { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Location CA = new Location() { Market = "CA", Language = "en" };
        public static Location FR = new Location() { Market = "FR", Language = "fr" };
        public static Location US = new Location() { Market = "US", Language = "en" };
        public static Location UK = new Location() { Market = "GB", Language = "en" };
        public static Location ES = new Location() { Market = "ES", Language = "es" };
        public static Location DE = new Location() { Market = "DE", Language = "de" };

        public static Location Parse(string market)
        {
            foreach (var field in typeof(Location).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var location = (Location)field.GetValue(null);
                if (location.Market.Equals(market, System.StringComparison.CurrentCultureIgnoreCase))
                {
                    return location;
                }
            }

            throw new ArgumentException($"Unknown location {market}");
        }
    }
}