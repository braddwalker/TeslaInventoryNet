using Newtonsoft.Json;

namespace TeslaInventoryNet
{
    /// <summary>
    /// These are not part of the underlying data model, but rather they get computed
    /// once search results get returned from the API.
    /// </summary>
    public class CompositorUrls
    {
        [JsonProperty("frontView")]
        public string FrontView { get; set; }
        
        [JsonProperty("sideView")]
        public string SideView { get; set; }
        
        [JsonProperty("interiorView")]
        public string InteriorView { get; set; }
    }

    public class Vehicle : BaseVehicle
    {
        public CompositorUrls CompositorUrls { get; set; }

        public string DetailsUrl { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}