using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Linq;

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

        private readonly ILogger<TeslaInventory> logger;
        private IRestClient client = null;

        /// <summary>
        /// Delegate to process all of the search results at once
        /// </summary>
        /// <param name="results">A <c>IList<Result></c> collection</param>
        public delegate void ResultsAction(SearchResult results);

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <param name="client">An optional <c>IRestClient</c> implementation</param>
        /// <returns></returns>
        public TeslaInventory(ILogger<TeslaInventory> logger, [Optional] IRestClient client)
        {
            this.logger = logger;
            this.client = client ?? new RestClient();
        }

        /// <summary>
        /// Performs a search based on the provided filter criteria.
        /// </summary>
        /// <param name="location">The geographic location to search</param>
        /// <param name="criteria">The search criteria to use</param>
        /// <param name="action">The delegate to process results</param>
        public void Search(Location location, SearchCriteria criteria, ResultsAction action)
        {
            action(Search(location, criteria));
        }

        /// <summary>
        /// Performs a search based on the provided filter criteria.
        /// </summary>
        /// <param name="location">The geographic location to search</param>
        /// <param name="criteria">The search criteria to use</param>
        /// <returns>A list of search results</returns>
        public SearchResult Search(Location location, SearchCriteria criteria)
        {
            // build the querystring
            var query = JsonConvert.SerializeObject(new {
                query = new {
                    model = criteria.Model,
                    condition = criteria.Condition,
                    country = location.Country,
                    market = location.Market,
                    language = location.Language,
                    region = location.Region
                },
                offset = criteria.Offset,
                count = criteria.Count
            });

            logger.LogDebug($"Query: {query}");

            // Perform the API call
            client.BaseUrl = new Uri(TESLA_API);
            var request = new RestRequest();
            request.AddParameter("query", query);
            var response = client.Execute(request);

            if (response == null)
            {
                throw new Exception("Error calling Tesla API - null response");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                logger.LogDebug($"StatusCode: {response.StatusCode} - {response.ErrorMessage}");
                throw new Exception($"{response.StatusCode} - {response.ErrorMessage}");
            }
            else 
            {
                //logger.LogDebug(response.Content);

                // Check for API errors first
                var error = JsonConvert.DeserializeAnonymousType(response.Content, new { Error = "", Code = 0});
                if (error.Code > 0)
                {
                    throw new Exception($"Error calling Tesla API - {error.Code}: {error.Error} - {JsonConvert.SerializeObject(query)}");
                }

                // Deserialize the actual results and return the relevant portion
                var results = JsonConvert.DeserializeObject<SearchResult>(response.Content);

                // generate the image URLs
                foreach (var vehicle in results.Vehicles)
                {
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

        private string BuildImageUrl(string viewName, Vehicle vehicle)
        {
            return $"{COMPOSITOR_URL}?model={vehicle.Model}&view={viewName}&size=1441&bkba_opt=2&options={string.Join(',', vehicle.OptionCodeData.Select(x => x.Code))}";
        }
    }
}