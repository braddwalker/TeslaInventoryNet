using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;

namespace TeslaInventoryNet
{
    /// <summary>
    /// This class is a light wrapper around the Tesla public inventory API. It can be used to craft inventory searches and list the results.
    /// </summary>
    public class TeslaInventory
    {
        // The public Tesla inventory API
        private static readonly string TESLA_API = "https://www.tesla.com/inventory/api/v1/inventory-results";
        
        // The URL to build image compositor urls from
        private static readonly string COMPOSITOR_URL = "https://static-assets.tesla.com/v1/compositor/";

        private static readonly int DEFAULT_TIMEOUT = 10000;

        private readonly ILogger<TeslaInventory> logger;
        private readonly JsonSerializerSettings jsonSettings = null;
        private readonly HttpClient httpClient = null;

        /// <summary>
        /// Delegate to process all of the search results at once
        /// </summary>
        /// <param name="results">A <c>IList<Result></c> collection</param>
        public delegate void ResultsAction(SearchResult results);

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <param name="httpClient">An optional <c>HttpClient</c> implementation</param>
        /// <returns></returns>
        public TeslaInventory(ILogger<TeslaInventory> logger, [Optional] HttpClient httpClient)
        {
            this.logger = logger;
            this.httpClient = httpClient ?? new HttpClient()
            {
                BaseAddress = new Uri(TESLA_API),
                DefaultRequestVersion = HttpVersion.Version20,
                DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
                Timeout = TimeSpan.FromMilliseconds(DEFAULT_TIMEOUT),
            };
            this.jsonSettings = new JsonSerializerSettings()
            {
                Error = delegate(object sender, ErrorEventArgs args)
                {
                    logger.LogWarning($"Json parse error: {args.ErrorContext.Error.Message}");
                    args.ErrorContext.Handled = true;
                },
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        /// <summary>
        /// Performs a search based on the provided filter criteria.
        /// </summary>
        /// <param name="location">The geographic location to search</param>
        /// <param name="criteria">The search criteria to use</param>
        /// <param name="action">The delegate to process results</param>
        public async void Search(Location location, SearchCriteria criteria, ResultsAction action)
        {
            action(await Search(location, criteria));
        }

        /// <summary>
        /// Performs a search based on the provided filter criteria.
        /// </summary>
        /// <param name="location">The geographic location to search</param>
        /// <param name="criteria">The search criteria to use</param>
        /// <returns>A list of search results</returns>
        public async Task<SearchResult> Search(Location location, SearchCriteria criteria)
        {
            // build the querystring
            var query = JsonConvert.SerializeObject(new {
                query = new {
                    model = criteria.Model,
                    condition = criteria.Condition,
                    market = location.Market,
                    language = location.Language
                },
                offset = criteria.Offset,
                count = criteria.Count,
            });

            // Don't get me started on this...
            if (location == Location.CA && criteria.Condition.Equals("used", StringComparison.CurrentCultureIgnoreCase))
            {
                query = JsonConvert.SerializeObject(new {
                    query = new {
                        model = criteria.Model,
                        condition = criteria.Condition,
                        market = location.Market,
                        language = location.Language
                    },
                    offset = criteria.Offset,
                    count = criteria.Count,
                    outsideOffset = 0,
                    outsideSearch = false
                });
            }

            logger.LogDebug($"Query: {query}");

            var queryParams = HttpUtility.ParseQueryString(string.Empty);
            queryParams["query"] = query;

            var response = await httpClient.GetAsync($"?{queryParams}");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                logger.LogDebug($"StatusCode: {response.StatusCode} - {response.Content}");
                throw new Exception($"{response.StatusCode} - {response.Content}");
            }
            else 
            {
                var rawString = await response.Content.ReadAsStringAsync();

                // Check for API errors first
                var error = JsonConvert.DeserializeAnonymousType(rawString, new { Error = "", Code = 0});
                if (error.Code > 0)
                {
                    throw new Exception($"Error calling Tesla API - {error.Code}: {error.Error} - {JsonConvert.SerializeObject(query)}");
                }

                // Deserialize the actual results and return the relevant portions
                // Start with a dynamic object because results can come back in more than one format
                dynamic raw = JsonConvert.DeserializeObject(rawString, jsonSettings);
                var results = new SearchResult();

                if (raw.results is JArray)
                {
                    results = JsonConvert.DeserializeObject<SearchResult>(rawString, jsonSettings);
                }
                else
                {
                    results.TotalMatchesFound = raw.total_matches_found;
                    results.Vehicles = JsonConvert.DeserializeObject<Vehicle[]>(JsonConvert.SerializeObject(raw.exact), jsonSettings) ?? Array.Empty<Vehicle>();
                }

                // Generate the custom vehicle attributes
                foreach (var vehicle in results.Vehicles)
                {
                    vehicle.DetailsUrl = BuildDetailsUrl(vehicle.Vin, vehicle.Model, location);
                    vehicle.CompositorUrls = new CompositorUrls();

                    if (!string.IsNullOrWhiteSpace(vehicle.CompositorViews.FrontView))
                    {
                        vehicle.CompositorUrls.FrontView = BuildImageUrl(vehicle.CompositorViews.FrontView, vehicle);
                    }

                    if (!string.IsNullOrWhiteSpace(vehicle.CompositorViews.InteriorView))
                    {
                        vehicle.CompositorUrls.InteriorView = BuildImageUrl(vehicle.CompositorViews.InteriorView, vehicle);
                    }

                    if (!string.IsNullOrWhiteSpace(vehicle.CompositorViews.SideView))
                    {
                        vehicle.CompositorUrls.SideView = BuildImageUrl(vehicle.CompositorViews.SideView, vehicle);
                    }
                }

                return results;
            }
        }

        /// <summary>
        /// Builds the appropriate details view URL for a vehicle. Different locations use different URLs.
        /// </summary>
        /// <param name="vin">The vehicle vin</param>
        /// <param name="model">The vehicle model</param>
        /// <param name="location">The location this vehicle exists in</param>
        /// <returns></returns>
        public static string BuildDetailsUrl(string vin, string model, Location location)
        {
            return $"https://www.tesla.com/{location.Language}_{location.Market}/{model}/order/{vin}";
        }

        private static string BuildImageUrl(string viewName, Vehicle vehicle)
        {
            return $"{COMPOSITOR_URL}?model={vehicle.Model}&view={viewName}&size=1441&bkba_opt=2&options={string.Join(',', vehicle.OptionCodeList)}";
        }
    }
}