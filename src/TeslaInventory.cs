using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace TeslaInventoryNet
{
    /// <summary>
    /// This class is a light wrapper around the Tesla public inventory API. It can be used to craft inventory searches and list the results.
    /// </summary>
    public class TeslaInventory
    {
        // The public Tesla inventory API
        private static readonly string TESLA_API = "https://www.tesla.com/inventory/api/v1/inventory-results";

        private readonly ILogger<TeslaInventory> logger;

        /// <summary>
        /// Delegate to process all of the search results at once
        /// </summary>
        /// <param name="results">A <c>IList<Result></c> collection</param>
        public delegate void ResultsAction(IList<Result> results);

        public TeslaInventory(ILogger<TeslaInventory> logger)
        {
            this.logger = logger;
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
        public IList<Result> Search(Location location, SearchCriteria criteria)
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
                }
            });

            logger.LogDebug($"Query: {query}");

            // Perform the API call
            var client = new RestClient(TESLA_API);
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
                throw new Exception(response.ErrorMessage);
            }
            else 
            {
                // Check for API errors first
                var error = JsonConvert.DeserializeAnonymousType(response.Content, new { Error = "", Code = 0});
                if (error.Code > 0)
                {
                    logger.LogDebug($"Error: {error.Code} - {error.Error}");
                    throw new Exception($"Error calling Tesla API - {error.Code}: {error.Error} - {JsonConvert.SerializeObject(query)}");
                }

                // Deserialize the actual results and return the relevant portion
                return JsonConvert.DeserializeAnonymousType(response.Content, new { results = new Result[0], total_matches_found = 0}).results;
            }
        }
    }
}