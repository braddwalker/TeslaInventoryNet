using Newtonsoft.Json;

namespace TeslaInventoryNet
{
    public class SearchResult
    {
        [JsonProperty("results")]
        public Vehicle[] Vehicles {get; set;}

        [JsonProperty("total_matches_found")]
        public int TotalMatchesFound {get; set;}
    }
}