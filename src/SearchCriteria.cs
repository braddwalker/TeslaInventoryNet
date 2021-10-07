namespace TeslaInventoryNet
{
    public class SearchCriteria
    {
        /// <summary>
        /// The model to search for. Valid values are: m3, my, mx, ms
        /// </summary>
        public string Model {get; set;}

        /// <summary>
        /// The condition to search for. Valid values are: new, used
        /// </summary>
        public string Condition {get; set;}

        /// <summary>
        /// The number of search results to return 
        /// </summary>
        public int Count {get; set;}

        /// <summary>
        /// For paging, the offset of the total search results to start returning data for
        /// </summary>
        public int Offset {get; set;}
    }
}