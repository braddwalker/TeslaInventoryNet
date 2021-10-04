namespace TeslaInventoryNet
{
    public class Location
    {
        public string Market { get; set;}
        public string Language {get; set;}
        public string Region {get; set;}
        public string Country {get; set;}

        public static Location CA = new Location() { Market = "CA", Language = "en", Region = "North America", Country = "Canada" };
        public static Location US = new Location() { Market = "US", Language = "en", Region = "North America", Country = "United States" };
    }
}